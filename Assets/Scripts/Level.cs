using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] Button _button;
    [SerializeField] BoxCollider _buttonSpawnArea;
    [SerializeField] Door _door;

    void Awake()
    {
        SpawnButton();
    }

    void InitLevel()
    {
        _button.Reset();
        _door.Reset();
        SpawnButton();
    }

    void SpawnButton()
    {
        Vector3 spawnPosition = new(
            Random.Range(_buttonSpawnArea.bounds.min.x, _buttonSpawnArea.bounds.max.x),
            _button.transform.position.y,
            Random.Range(_buttonSpawnArea.bounds.min.z, _buttonSpawnArea.bounds.max.z)
        );
        _button.transform.position = spawnPosition;
    }

    [ContextMenu("Reset")]
    void ResetLevel()
    {
        InitLevel();
    }
}
