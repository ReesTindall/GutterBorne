using UnityEngine;

public class BlobMovementWorldSpace : MonoBehaviour
{
    public float moveSpeed = 1f;
    float startSpeed = 1f;
    public float rotationSpeed = 720f;
    public float dampingDrag = 2f;
    public float jumpForce = 5f;

    private Rigidbody rb;
    private bool jump;
    Vector3 input; 
    bool jumpTweenActive = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = true;
        rb.drag = dampingDrag;
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        input = new Vector3(h, 0, v);

        if (Input.GetButtonDown("Jump") && IsGrounded())
            jump = true;
    }

    void FixedUpdate()
    {
    
        if (input.magnitude > 0.1f)
        {
            Vector3 moveDir = input.normalized;
            Quaternion targetRotation = Quaternion.LookRotation(moveDir);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
            Vector3 newVelocity = moveDir * moveSpeed;
            newVelocity.y = rb.velocity.y;
            rb.velocity = newVelocity;
        }
        else
        {
            //rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }

        if (jump)
        {
            jumpTweenActive = true;
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
            jump = false;
        }

        // TWEENING jump:
        if (jumpTweenActive){
            GetComponent<Slime_Scale>().jumpTweenOn = true;
            jumpTweenActive = false;
        }

        // TWEENING walk:
        else if (input.magnitude > 0.1f) {
            GetComponent<Slime_Scale>().moveTweenOn = true;
        } else {
            GetComponent<Slime_Scale>().moveTweenOn = false;
        }
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1.1f);
    }
}