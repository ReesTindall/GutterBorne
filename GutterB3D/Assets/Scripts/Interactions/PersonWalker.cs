using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PersonWalker : MonoBehaviour
{
    public float walkSpeed = 2f;                // Speed multiplier
    public float animSpeed = 1f;                // Animation playback speed
    public Vector3 walkDirection = Vector3.back; // Default direction

    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        anim.speed = animSpeed;
    }

    void Update()
    {
        // Move in world space using custom direction
        transform.Translate(walkDirection.normalized * walkSpeed * Time.deltaTime, Space.World);
    }
}
