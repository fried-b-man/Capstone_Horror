using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonObj : MonoBehaviour, iInteract
{
    public event Action Interacted = delegate { };
    public event Action<bool> Toggled = delegate { };

    public bool State { get; private set; } = false;

    [SerializeField] GameObject childLight = null;

    public void Interact()
    {
        Interacted.Invoke();

        State = !State;
        Toggle();
        Toggled.Invoke(State);

        Debug.Log("Clicked Button");
    }

    private void Toggle()
    {
        childLight.SetActive(State);
    }
}
