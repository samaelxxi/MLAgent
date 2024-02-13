using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Door : MonoBehaviour
{
    float _initialY;

    const float HEIGHT = 2;

    private void Awake()
    {
        _initialY = gameObject.transform.localPosition.y;
    }

    public void OpenDoor()
    {
        gameObject.transform.DOLocalMoveY(_initialY + HEIGHT, 1);
    }

    public void Reset()
    {
        gameObject.transform.DOLocalMoveY(_initialY, 0);
    }
}
