using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour
{
    private PlayerInput input = null;
    private float xRotation = 0f;
    private Rigidbody rb = null;

    [SerializeField] private float moveSpeed = 1f;

    private void Awake()
    {
        input = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnEnable()
    {
        input.Moused += OnMoused;
        input.Moved += OnMoved;
    }

    private void OnMoved(Vector3 movement)
    {
        rb.MovePosition(rb.position + (movement * moveSpeed * Time.deltaTime));
    }

    private void OnMoused(Vector2 mousePosition)
    {
        transform.Rotate(Vector3.up * mousePosition.x);

        xRotation -= mousePosition.y;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);  //can never look upside down, inside out

        Camera.main.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

}
