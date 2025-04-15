using UnityEngine;

public class SimpleGiant : MonoBehaviour
{
    public float moveSpeed = 2f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Vector3 nextPos = rb.position + Vector3.forward * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(nextPos);
    }
}
