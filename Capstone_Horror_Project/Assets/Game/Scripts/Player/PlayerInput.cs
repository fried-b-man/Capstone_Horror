using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public event Action<Vector3> Moved = delegate { };
    public event Action<Vector2> Moused = delegate { };
    public event Action LMBClicked = delegate { };
    public event Action RMBClicked = delegate { };

    [SerializeField] private float mouseSensitivity = 100f;

    private void Update()
    {
        CheckMove();
        CheckMouse();
    }

    private void CheckMove()
    {
        Vector3 playerMove = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            playerMove += transform.forward;
        }

        if (Input.GetKey(KeyCode.S))
        {
            playerMove -= transform.forward;
        }

        if (Input.GetKey(KeyCode.D))
        {
            playerMove += transform.right;
        }

        if (Input.GetKey(KeyCode.A))
        {
            playerMove -= transform.right;
        }
        
        Moved.Invoke(playerMove.normalized);
    }

    private void CheckMouse()
    {
        Vector2 playerMouse = Vector2.zero;

        playerMouse.x = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        playerMouse.y = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        Moused.Invoke(playerMouse);

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            LMBClicked.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            RMBClicked.Invoke();
        }
    }
}
