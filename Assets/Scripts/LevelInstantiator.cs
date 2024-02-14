using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInstantiator : MonoBehaviour
{
    [SerializeField] GameObject _levelPrefab;
    [SerializeField] int _levelCount = 25;

    private void Start()
    {
        int cols = Mathf.CeilToInt(Mathf.Sqrt(_levelCount));

        int colIdx = 0;
        int rowIdx = 0;
        int levels = 0;
        while (levels < _levelCount)
        {
            Instantiate(_levelPrefab, new Vector3(rowIdx * 15, 0, colIdx * 10), Quaternion.identity);
            levels++;
            colIdx++;
            if (colIdx >= cols)
            {
                colIdx = 0;
                rowIdx++;
            }
        }
    }
}
