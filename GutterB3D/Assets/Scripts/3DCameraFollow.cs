// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class CameraControl3DLERP : MonoBehaviour {

// 	public Transform target; // drag the intended target object into the Inspector slot
// 	public float smoothSpeed = 10f;
// 	public Vector3 offset; // set the offset in the editor


// 	// Update is called once per frame
// 	void FixedUpdate () {
// 		Vector3 desiredPosition = target.position + offset;
// 		Vector3 smoothPosition = Vector3.Lerp (transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
// 		transform.position = smoothPosition;

// 		transform.LookAt (target);
// 	}
// }

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl3DLERP : MonoBehaviour
{
    public Transform target; // Drag player here
    public float smoothSpeed = 10f;
    public Vector3 baseOffset; // Your normal camera offset at player scale = 1
    public float distanceFactor = 2f; // Tweak this for how much further to go when player scales up

    void FixedUpdate()
    {
        // Get scale multiplier (e.g., 1 for normal size, 2 if player doubles in size)
        float scaleMultiplier = target.localScale.magnitude / Mathf.Sqrt(3f); // Normalize scale to 1 when (1,1,1)

        // Adjust offset based on scale
        Vector3 dynamicOffset = baseOffset * scaleMultiplier * distanceFactor;

		// Clamp Y so camera never goes below 0.1
		if (dynamicOffset.y < 0.1f)
			dynamicOffset.y = 0.1f;
		
		// Clamp Z so camera never goes below -1.5
		if (dynamicOffset.y < -1.5f)
			dynamicOffset.y = -1.5f;

        // Calculate position
        Vector3 desiredPosition = target.position + dynamicOffset;
        Vector3 smoothPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothPosition;

    }
}
