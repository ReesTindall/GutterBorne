using UnityEngine;
using UnityEngine.Audio;

public class BlobJump : MonoBehaviour
{

    public float dampingDrag = 0.1f;
    public float jumpForce = 5f;

    private Rigidbody rb;
    private bool jump;
    Vector3 input; 
    bool jumpTweenActive = false;

    public AudioClip jumpSound; 
    private AudioSource audioSource;
    public AudioMixerGroup mixerGroup;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = true;
        rb.drag = dampingDrag;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // if (Input.GetButtonDown("Jump") && IsGrounded()){
        if (Input.GetButtonDown("Jump")){
            jump = true;
        }
    }
    void FixedUpdate()
    {

        if (jump)
        {
            jumpTweenActive = true;
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
            if (jumpSound != null && audioSource != null)
            {
                Debug.Log("jump sound");
                audioSource.outputAudioMixerGroup = mixerGroup;
                audioSource.PlayOneShot(jumpSound);
            }
            jump = false;
        }

        // TWEENING jump:
        if (jumpTweenActive){
            GetComponent<Slime_Scale>().jumpTweenOn = true;
            jumpTweenActive = false;
        }

    }

    // private bool IsGrounded()
    // {
    //     return Physics.Raycast(transform.position, Vector3.down, 1.1f);
    // }
}