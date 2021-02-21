using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class HoldableObj : MonoBehaviour, iInteract
{
    public event Action Interacted = delegate { };

    private Collider myCollider = null;

    public SocketType Socket { get; private set; } = SocketType.Painting;

    [TextArea][SerializeField] private string pickupText = "";
    public string PickupText { get { return pickupText; } }

    private void Awake()
    {
        myCollider = GetComponent<Collider>();
    }

    public void Interact()
    {
        Interacted.Invoke();
    }

    public void PickUp(PlayerInteractor player)
    {
        //disable collision + interact volumes
        myCollider.enabled = false;
        
        //move to player hand area
        transform.position = player.HoldPosition.position;
        transform.rotation = player.HoldPosition.rotation;
        transform.parent = player.transform;
    }

    public void Drop(Vector3 location, Vector3 rotation)
    {
        //enabl collision + interact volumes
        myCollider.enabled = true;

        //move to location
        transform.position = location;

        RaycastHit hit;
        Ray ray = new Ray(transform.position, (Vector3.down));

        if (Physics.Raycast(ray, out hit, 10f))
        {
            Vector3 postion = new Vector3(hit.point.x, hit.point.y + myCollider.bounds.extents.y, hit.point.z);
            transform.position = postion;
        }

        transform.rotation = Quaternion.Euler(new Vector3(rotation.x, 0, rotation.z));
        transform.parent = null;
    }

    public void Drop(GameObject socketObj)
    {
        Debug.Log("Being placed into a socketObject");

        //move to socket area
        //disable collision (if enabled?)
    }
}
