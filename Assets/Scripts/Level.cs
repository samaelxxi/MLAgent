using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] Button _button;
    [SerializeField] BoxCollider _buttonSpawnArea;
    [SerializeField] Door _door;


    public Vector2 ButtonPos => new(_button.transform.localPosition.x, _button.transform.localPosition.z);
    public Vector2 ExitPos => new(_door.transform.localPosition.x, _door.transform.localPosition.z);
    public bool IsButtonPressed => _button.IsPressed;

    public event Action OnButtonPressed { add => _button.OnButtonPressed += value; remove => _button.OnButtonPressed -= value;}


    void Awake()
    {
        SpawnButton();
    }

    public void InitLevel()
    {
        _button.Reset();
        _door.Reset();
        SpawnButton();
    }

    void SpawnButton()
    {
        Vector3 spawnPosition = GetRandomPosition();
        spawnPosition.y = _button.transform.position.y;
        _button.transform.position = spawnPosition;
    }

    public Vector3 GetRandomPosition()
    {
        Vector3 spawnPosition = new(
            UnityEngine.Random.Range(_buttonSpawnArea.bounds.min.x, _buttonSpawnArea.bounds.max.x),
            _buttonSpawnArea.bounds.max.y,
            UnityEngine.Random.Range(_buttonSpawnArea.bounds.min.z, _buttonSpawnArea.bounds.max.z)
        );
        return spawnPosition;
    }
}
