using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]
public class BlobMovementPhysics : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float mouseSensitivity = 3f;

    [Header("Jump Settings")]
    public float jumpForce = 7f;
    public float groundCheckDistance = 0.1f;
    public LayerMask groundMask;

    [Header("Knockback Settings")]
    public float knockbackDamp = 5f;

    [Header("Audio")]
    public AudioClip jumpSound;
    public AudioMixerGroup mixerGroup;

    private Rigidbody rb;
    private AudioSource audioSource;
    private bool isGrounded;
    private bool jumpRequest = false;
    private Vector3 moveInput;
    private Vector3 externalVelocity;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        rb.useGravity = true;

        audioSource = GetComponent<AudioSource>();
        audioSource.outputAudioMixerGroup = mixerGroup;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }


    void Update()
    {
        if (GameHandler_PauseMenu.GameisPaused) return;

        // Restore cursor lock if lost (click to regain control)
        if (Cursor.lockState != CursorLockMode.Locked && Input.GetMouseButtonDown(0))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        // Rotate slime with raw mouse input
        float mouseX = Input.GetAxisRaw("Mouse X") * mouseSensitivity;
        transform.Rotate(Vector3.up * mouseX);
        // if (GameHandler_PauseMenu.GameisPaused) return; 
        // // Rotate slime with mouse
        // float mouseX = Input.GetAxisRaw("Mouse X") * mouseSensitivity;
        // transform.Rotate(Vector3.up * mouseX);

        // Get movement input
        float v = Input.GetAxisRaw("Vertical");
        moveInput = transform.forward * v;

        // Jump input
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            jumpRequest = true;
        }

        // Move tween trigger
        Slime_Scale slimeScale = GetComponent<Slime_Scale>();
        bool isMoving = moveInput.magnitude > 0.01f || externalVelocity.sqrMagnitude > 0.01f;
        slimeScale.moveTweenOn = isMoving;
    }

    void FixedUpdate()
    {
        if (GameHandler_PauseMenu.GameisPaused) return; 
        // Apply movement
        Vector3 velocity = (moveInput * moveSpeed) + externalVelocity;
        Vector3 targetPos = rb.position + velocity * Time.fixedDeltaTime;
        rb.MovePosition(targetPos);

        // Jump
        if (jumpRequest)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
            jumpRequest = false;

            // Jump tween
            GetComponent<Slime_Scale>().jumpTweenOn = true;

            // Sound
            if (jumpSound != null)
            {
                audioSource.PlayOneShot(jumpSound);
                Debug.Log("Jump sound played");
            }
        }

        // Dampen knockback
        externalVelocity = Vector3.Lerp(externalVelocity, Vector3.zero, knockbackDamp * Time.fixedDeltaTime);
    }

    public void AddKnockback(Vector3 dir, float force)
    {
        externalVelocity = dir.normalized * force;
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, groundCheckDistance + 0.1f, groundMask);
    }
}
