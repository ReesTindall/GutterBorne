using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class glowRotate : MonoBehaviour
{
    [SerializeField] GameObject glow;
    void Update () {
	    //rotate 
	    glow.transform.Rotate(0,1,0, Space.Self);
	}
}
