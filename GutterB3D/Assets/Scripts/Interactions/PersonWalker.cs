using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PersonWalker : MonoBehaviour
{
    public float walkSpeed = 2f;    // How fast they move forward
    public float animSpeed = 1f;    // How fast the animation plays

    private Animator anim;

    void Start()
    {
        float animSpeed = walkSpeed /2;
        anim = GetComponent<Animator>();
        anim.speed = animSpeed;
    }

    void Update()
    {
        // Move forward constantly (along local Z axis)
        transform.Translate(Vector3.forward * walkSpeed * Time.deltaTime);
    }
}
