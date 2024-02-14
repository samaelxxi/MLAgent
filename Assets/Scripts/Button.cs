using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class Button : MonoBehaviour
{
    [SerializeField] UnityEvent _onButtonPressed;
    [SerializeField] Renderer _buttonRenderer;


    public bool IsPressed { get; private set; }

    public event System.Action OnButtonPressed;

    Tween _pressTween;

    public void PressButton()
    {
        if (IsPressed)
            return;
        
        IsPressed = true;
        _pressTween = _buttonRenderer.material.DOColor(Color.green, 1);
        _onButtonPressed.Invoke();
        OnButtonPressed?.Invoke();
    }

    public void Reset()
    {
        _pressTween.Kill();
        _buttonRenderer.material.color = Color.red;
        IsPressed = false;
    }

    void OnTriggerEnter(Collider other)
    {
        PressButton();
    }
}
