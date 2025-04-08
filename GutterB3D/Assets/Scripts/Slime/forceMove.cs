using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class forceMove : MonoBehaviour
{
    void Update()
    {
        transform.position += transform.forward * Time.deltaTime;
    }
}
