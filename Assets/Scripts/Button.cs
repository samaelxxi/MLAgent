using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class Button : MonoBehaviour
{
    [SerializeField] UnityEvent _onButtonPressed;
    [SerializeField] Renderer _buttonRenderer;

    public void PressButton()
    {
        _onButtonPressed.Invoke();
        _buttonRenderer.material.DOColor(Color.green, 1);
    }

    [ContextMenu("Press Button")]
    public void PressButtonContextMenu()
    {
        PressButton();
    }

    public void Reset()
    {
        _buttonRenderer.material.DOColor(Color.red, 0);
    }
}
