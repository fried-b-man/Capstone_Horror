using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class PlayerInteractor : MonoBehaviour
{
    private PlayerInput input = null;

    private void Awake()
    {
        input = GetComponent<PlayerInput>();
    }

    private void OnEnable()
    {
        input.Clicked += OnClicked;
    }

    private void OnClicked()
    {
        RaycastHit hit;

        //following guide, must test further
        int layerMask = 1 << 8;
        layerMask = ~layerMask;

        if (Physics.Raycast(transform.position, Camera.main.transform.forward, out hit, 5f, layerMask))
        {
            iInteract interactable = hit.transform.gameObject.GetComponent<iInteract>();
            interactable.Interact();
        }
    }
}
