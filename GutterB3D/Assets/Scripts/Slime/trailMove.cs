using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeTrailMovement : MonoBehaviour
{
    public float fakeSpeed = 0.1f;
    private Vector3 originalLocalPos;

    void Start()
    {
        originalLocalPos = transform.localPosition;
    }

    void Update()
    {
        // Slight oscillation to force TrailRenderer to update
        transform.localPosition = originalLocalPos + new Vector3(0, 0, Mathf.Sin(Time.time * 10) * fakeSpeed);
    }
}
