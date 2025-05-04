// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class CameraControl3DLERP : MonoBehaviour
// {
//     private Transform target; // Drag player here
//     public float smoothSpeed = 10f;
//     public Vector3 baseOffset; // Your normal camera offset at player scale = 1
//     public float distanceFactor = 2f; // Tweak this for how much further to go when player scales up

//     void Start () {
//         target = GameObject.FindWithTag("Player").transform;
//     }

//     void FixedUpdate()
//     {
//         // Get scale multiplier (e.g., 1 for normal size, 2 if player doubles in size)
//         float scaleMultiplier = target.localScale.magnitude / Mathf.Sqrt(3f); // Normalize scale to 1 when (1,1,1)

//         // Adjust offset based on scale
//         Vector3 dynamicOffset = baseOffset * scaleMultiplier * distanceFactor;

// 		// Clamp Y so camera never goes below 0.2
// 		if (dynamicOffset.y < 0.2f)
// 			dynamicOffset.y = 0.2f;
		
// 		// Clamp Z so camera never goes above -1
// 		if (dynamicOffset.z > -1f)
// 			dynamicOffset.z = -1f;

//         // Calculate position
//         Vector3 desiredPosition = target.position + dynamicOffset;
//         Vector3 smoothPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
//         transform.position = smoothPosition;

//         //transform.LookAt (target);
//     }
// }
using UnityEngine;

public class CameraControlAngleFollow : MonoBehaviour
{
    private Transform target;
    public float smoothSpeed = 10f;

    [Header("View Settings")]
    public float viewAngleDegrees = 30f;
    public float baseDistance = 6f;
    public float heightOffset = 1f;

    [Header("Distance Scaling")]
    public float distanceScaleFactor = 1.5f;

    [Header("Collision Settings")]
    public LayerMask environmentMask;         // Set this to include "Default" or your wall layers
    public float collisionBuffer = 0.3f;       // Prevent camera from being right against the wall
    public float sphereCastRadius = 0.3f;      // Camera's virtual size for collision check

    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
    }

    void FixedUpdate()
    {
        float scale = target.localScale.magnitude / Mathf.Sqrt(3f);
        float scaledDistance = baseDistance * scale * distanceScaleFactor;

        // Calculate base offset using desired angle and distance
        float angleRad = viewAngleDegrees * Mathf.Deg2Rad;
        float yOffset = Mathf.Sin(angleRad) * scaledDistance;
        float zOffset = Mathf.Cos(angleRad) * scaledDistance;

        Vector3 localOffset = new Vector3(0, yOffset, -zOffset);

        // ✅ FIX: Only use the slime's Y rotation to prevent over-rotation
        Quaternion rotationY = Quaternion.Euler(0, target.eulerAngles.y, 0);
        Vector3 worldOffset = rotationY * localOffset;

        Vector3 desiredCameraPos = target.position + worldOffset;
        Vector3 lookTarget = target.position + Vector3.up * heightOffset * scale;

        // SphereCast to check for collision between slime and desired camera position
        Vector3 direction = (desiredCameraPos - lookTarget).normalized;
        float maxDistance = (desiredCameraPos - lookTarget).magnitude;

        if (Physics.SphereCast(lookTarget, sphereCastRadius, direction, out RaycastHit hit, maxDistance, environmentMask))
        {
            // Move camera just before the wall
            desiredCameraPos = hit.point - direction * collisionBuffer;
        }

        // Smooth camera movement
        Vector3 smoothPosition = Vector3.Lerp(transform.position, desiredCameraPos, smoothSpeed * Time.deltaTime);
        transform.position = smoothPosition;

        // Always look at the slime's head area
        transform.LookAt(lookTarget);
    }
}

