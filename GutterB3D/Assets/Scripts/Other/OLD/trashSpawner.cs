using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class TrashSpawner : MonoBehaviour {

      //Object variables
      public GameObject trashPrefab;
      public Transform[] spawnPoints;
      private int rangeEnd;
      private Transform spawnPoint;

      //Timing variables
      public float spawnRangeStart = 0.5f;
      public float spawnRangeEnd = 1.2f;
      private float timeToSpawn;
      private float spawnTimer = 0f;

      void Start(){
       //assign the length of the array to the end of the random range
             rangeEnd = spawnPoints.Length - 1 ;
       }

      void FixedUpdate(){
            timeToSpawn = Random.Range(spawnRangeStart, spawnRangeEnd);
            spawnTimer += 0.01f;
            if (spawnTimer >= timeToSpawn){
                  spawnTrash();
                  spawnTimer =0f;
            }
      }

      void spawnTrash(){
            int SPnum = Random.Range(0, rangeEnd);
            spawnPoint = spawnPoints[SPnum];
            Instantiate(trashPrefab, spawnPoint.position, Quaternion.identity);
            Debug.Log("SPAWNING");
      }
}
