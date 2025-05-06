// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class glowRotate : MonoBehaviour
// {
//     [SerializeField] GameObject glow;
//     void Update () {
// 	    //rotate 
// 	    glow.transform.Rotate(0,1,0, Space.Self);
// 	}
// }

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class glowRotate : MonoBehaviour
{
    [SerializeField] GameObject glow;
    [SerializeField] float rotationSpeed = 30f; // degrees per second

    void Update()
    {
        // Rotate slower using Time.deltaTime and speed
        glow.transform.Rotate(0, rotationSpeed * Time.deltaTime, 0, Space.Self);
    }
}
