using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class BlobMovementRelative : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float mouseSensitivity = 3f;

    private CharacterController controller;
    private float verticalRotation = 0f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Mouse input rotates the slime (Y axis only)
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        transform.Rotate(Vector3.up * mouseX);

        // Get input and move slime relative to its own forward
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 move = transform.right * h + transform.forward * v;
        controller.SimpleMove(move * moveSpeed);
    }
}

