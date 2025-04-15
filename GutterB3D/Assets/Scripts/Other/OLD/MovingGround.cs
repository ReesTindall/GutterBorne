using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveGround : MonoBehaviour {
    public float moveSpeed = 10f;
    
    public float resetPositionZ = -40f;
    
    public float startPositionZ = 40f;

    void Update() {
        
        transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);

        
        if (transform.position.z <= resetPositionZ) {
            Vector3 newPos = transform.position;
            newPos.z = startPositionZ;
            transform.position = newPos;
        }
    }
}