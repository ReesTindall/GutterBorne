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



// using UnityEngine;
// using UnityEngine.Audio;

// [RequireComponent(typeof(Rigidbody), typeof(AudioSource))]
// public class BlobMovementCombined : MonoBehaviour
// {
//     [Header("Movement Settings")]
//     public float moveSpeed = 1f;
//     public float rotationSpeed = 720f;
//     public float dampingDrag = 0.1f;
//     public float jumpForce = 5f;
//     public float mouseSensitivity = 3f;

//     [Header("Audio")]
//     public AudioClip jumpSound;
//     public AudioMixerGroup mixerGroup;

//     private Rigidbody rb;
//     private AudioSource audioSource;
//     private bool jump = false;
//     private bool jumpTweenActive = false;
//     private Vector3 input;

//     void Start()
//     {
//         rb = GetComponent<Rigidbody>();
//         rb.useGravity = true;
//         rb.drag = dampingDrag;

//         audioSource = GetComponent<AudioSource>();
//         Cursor.lockState = CursorLockMode.Locked;
//     }

//     void Update()
//     {
//         // Rotate with mouse (Y-axis only)
//         float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
//         transform.Rotate(Vector3.up * mouseX);

//         // Get W/S input only (no A/D)
//         float v = Input.GetAxis("Vertical");
//         input = new Vector3(0, 0, v);

//         // Jump input
//         if (Input.GetButtonDown("Jump"))
//         {
//             jump = true;
//         }
//     }

//     void FixedUpdate()
//     {
//         // Movement
//         if (input.magnitude > 0.1f)
//         {
//             Vector3 moveDir = transform.forward * input.z;

//             // Face movement direction
//             Quaternion targetRotation = Quaternion.LookRotation(moveDir);
//             transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);

//             // Apply velocity
//             Vector3 newVelocity = moveDir.normalized * moveSpeed;
//             newVelocity.y = rb.velocity.y;
//             rb.velocity = newVelocity;
//         }

//         // Jump
//         if (jump && IsGrounded())
//         {
//             jumpTweenActive = true;
//             rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);

//             if (jumpSound != null && audioSource != null)
//             {
//                 audioSource.outputAudioMixerGroup = mixerGroup;
//                 audioSource.PlayOneShot(jumpSound);
//             }

//             jump = false;
//         }

//         // Tweening
//         var scaleScript = GetComponent<Slime_Scale>();
//         if (jumpTweenActive)
//         {
//             if (scaleScript != null) scaleScript.jumpTweenOn = true;
//             jumpTweenActive = false;
//         }
//         else if (input.magnitude > 0.1f)
//         {
//             if (scaleScript != null) scaleScript.moveTweenOn = true;
//         }
//         else
//         {
//             if (scaleScript != null) scaleScript.moveTweenOn = false;
//         }
//     }

//     private bool IsGrounded()
//     {
//         return Physics.Raycast(transform.position, Vector3.down, 1.1f);
//     }
// }
