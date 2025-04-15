using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFood : MonoBehaviour {
    public float moveSpeed = 2f;
    public bool isAbsorbed = false;  

    void Update() {
        if (!isAbsorbed) {
            transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);
        }
    }
}