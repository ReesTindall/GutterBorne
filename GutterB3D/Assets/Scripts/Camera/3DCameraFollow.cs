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

    [Header("Camera Motion Settings")]
    public float smoothSpeed = 10f; // How quickly the camera interpolates to its target position

    [Header("View Settings")]
    public float viewAngleDegrees = 30f; // Angle the camera looks down at the player (degrees above horizontal)
    public float baseDistance = 6f; // Base distance the camera stays away from the player at starting size
    public float heightOffset = 0.5f; // how far above player center to look at



    [Header("Distance Scaling")]
    public float distanceScaleFactor = 1.5f; // How much further away the camera gets as the player grows

    [Header("Collision Settings")]
    public LayerMask environmentMask;         // set objects that the camera shoulve phase thru to "SolidWorld" layer
    public float collisionBuffer = 0.3f;       // so camera not right against the boundary
    public float sphereCastRadius = 0.3f;      // Cam's virtual size for collision check

    void Start()
    {
        // Automatically find the player GameObject using the "Player" tag
        target = GameObject.FindWithTag("Player").transform;
    }

    void FixedUpdate()
    {
        // --- DYNAMIC CAMERA DISTANCE CALCULATION ---

        // default size is 1,1,1
        // divide magnitude current sizeby mag of default size to get scale factor
        float scale = target.localScale.magnitude / Mathf.Sqrt(3f);
        
        // calculate new camera distance
        float scaledDistance = baseDistance * scale * distanceScaleFactor;

        // Calculate cartesian base offset using desired polar coords (angle and distance)
        float angleRad = viewAngleDegrees * Mathf.Deg2Rad;
        float yOffset = Mathf.Sin(angleRad) * scaledDistance;
        float zOffset = Mathf.Cos(angleRad) * scaledDistance;

        Vector3 localOffset = new Vector3(0, yOffset, -zOffset);

        // Only apply the slime’s Y rotation to rotate the camera behind it
        Quaternion rotationY = Quaternion.Euler(0, target.eulerAngles.y, 0);
        Vector3 worldOffset = rotationY * localOffset;

        // This is the final target position of the camera in world space and where it will look
        Vector3 desiredCameraPos = target.position + worldOffset;
        Vector3 lookTarget = target.position + Vector3.up * heightOffset * scale;

        // --- COLLISION CHECK TO AVOID CLIPPING THROUGH WALLS ---

        // Direction from the target to the camera
        Vector3 direction = (desiredCameraPos - lookTarget).normalized;
        
        // Distance from the slime to the desired camera position
        float maxDistance = (desiredCameraPos - lookTarget).magnitude;

        // SphereCast from the slime's head toward the desired camera position
        // If it hits something in the SolidWorld layer:
        if (Physics.SphereCast(lookTarget, sphereCastRadius, direction, out RaycastHit hit, maxDistance, environmentMask))
        {
            // Move camera just before the wall
            desiredCameraPos = hit.point - direction * collisionBuffer;
        }

        // --- SMOOTH CAMERA MOVEMENT AND ORIENTATION ---

        Vector3 smoothPosition = Vector3.Lerp(transform.position, desiredCameraPos, smoothSpeed * Time.deltaTime);
        transform.position = smoothPosition;

        // Always look at the slime's head area
        transform.LookAt(lookTarget);
    }
}

