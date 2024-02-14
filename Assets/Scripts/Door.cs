using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Door : MonoBehaviour
{
    float _initialY;

    const float HEIGHT = 2;

    Tween _openTween;

    private void Awake()
    {
        _initialY = gameObject.transform.localPosition.y;
    }

    public void OpenDoor()
    {
        _openTween = gameObject.transform.DOLocalMoveY(_initialY + HEIGHT, 1);
    }

    public void Reset()
    {
        _openTween.Kill();
        gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, _initialY, gameObject.transform.localPosition.z);
    }
}
