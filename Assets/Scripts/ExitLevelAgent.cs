using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class ExitLevelAgent : Agent
{
    [SerializeField] Level _level;
    [SerializeField] float _speed = 5;

    Rigidbody _rigidbody;

    float _jumpTime;


    private void Awake() 
    {
        _rigidbody = GetComponent<Rigidbody>();
        _level.OnButtonPressed += () => AddReward(0.5f);
    }

    public override void OnEpisodeBegin()
    {
        _level.InitLevel();
        _rigidbody.velocity = Vector3.zero;

        Vector3 spawnPosition = _level.GetRandomPosition();
        while (Vector3.Distance(spawnPosition, _level.ButtonPos) < 1)
            spawnPosition = _level.GetRandomPosition();
        transform.position = spawnPosition;
    }

    void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag("Exit")) 
        {
            AddReward(1);
            EndEpisode();
        }
        else if (other.gameObject.CompareTag("Wall")) 
        {
            AddReward(-0.01f);  // punish for hitting the wall
        }
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(_rigidbody.velocity);
        sensor.AddObservation(_level.ButtonPos);
        sensor.AddObservation(_level.ExitPos);
        sensor.AddObservation(_level.IsButtonPressed);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        if (transform.localPosition.y < -5)
        {   // fell off the level
            SetReward(-1);
            EndEpisode();
            return;
        }

        float moveX = actions.ContinuousActions[0];
        float moveZ = actions.ContinuousActions[1];
        bool jump = actions.DiscreteActions[0] == 1;

        _rigidbody.velocity = new Vector3(moveX * _speed, _rigidbody.velocity.y, moveZ * _speed);

        if (jump && CanJump())
        {
            _rigidbody.AddForce(Vector3.up*10, ForceMode.Impulse);
            AddReward(-0.02f);  // punish to prevent jumping all the time
            _jumpTime = 0.2f;
        }

        AddReward(-1f / MaxStep);  // punish for taking too long
        _jumpTime -= Time.fixedDeltaTime;
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActionsOut = actionsOut.ContinuousActions;
        var discreteActionsOut = actionsOut.DiscreteActions;

        continuousActionsOut[0] = Input.GetAxis("Horizontal");
        continuousActionsOut[1] = Input.GetAxis("Vertical");
        discreteActionsOut[0] = Input.GetKey(KeyCode.Space) ? 1 : 0;
    }

    bool CanJump()
    {
        return _jumpTime < 0 && Mathf.Abs(_rigidbody.velocity.y) < 0.1f && 
                Physics.Raycast(transform.position, Vector3.down, 1f);
    }
}
