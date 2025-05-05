using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stinklines_Rotation : MonoBehaviour
{
    private Transform cameraMain;

    // Start is called before the first frame update
    void Start()
    {
        cameraMain = GameObject.FindWithTag("MainCamera").transform;
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = new Vector3( 
            cameraMain.position.x,
            transform.position.y,
            cameraMain.position.z
        );
        transform.LookAt(targetPosition, Vector3.up);
    }
}
