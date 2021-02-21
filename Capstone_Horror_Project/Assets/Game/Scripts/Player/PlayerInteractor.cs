using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class PlayerInteractor : MonoBehaviour
{
    private PlayerInput input = null;

    [SerializeField] private Transform holdPosition = null;
    public Transform HoldPosition { get { return holdPosition; } }
    private HoldableObj heldObject = null;
    [SerializeField] private float dropDistance = 2f;

    private void Awake()
    {
        input = GetComponent<PlayerInput>();
    }

    private void OnEnable()
    {
        input.LMBClicked += OnClicked;
        input.RMBClicked += OnRMB;
    }

    private void OnClicked()
    {
        RaycastHit hit;
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

        if (Physics.Raycast(ray, out hit, dropDistance))
        {
            iInteract interactable = hit.transform.gameObject.GetComponent<iInteract>();
            if (interactable != null)
            {
                interactable?.Interact();
                InteractDelegate(interactable);
            }
        }
    }

    private void OnRMB()
    {
        RaycastHit hit;
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

        if (Physics.Raycast(ray, out hit, dropDistance))
        {
            heldObject?.Drop(hit.point + hit.normal.normalized, transform.rotation.eulerAngles);
        }
        else
        {
            heldObject?.Drop(Camera.main.transform.position + (Camera.main.transform.forward * dropDistance), transform.rotation.eulerAngles);
        }
        
        heldObject = null;
    }

    private void InteractDelegate(iInteract target)
    {
        if (target is HoldableObj)
        {
            if (heldObject == null)
            {
                (target as HoldableObj).PickUp(this);
                heldObject = (target as HoldableObj);
            }
        }
    }
}
