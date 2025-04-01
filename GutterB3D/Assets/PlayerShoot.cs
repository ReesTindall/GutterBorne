using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PlayerShoot : MonoBehaviour{

      //public Animator animator;
      public Transform fireBase;
      public Transform firePoint;
      public GameObject projectilePrefab;
      public float projectileSpeed = 10f;
      public float attackRate = 2f;
      private float nextAttackTime = 0f;

      void Start(){
           //animator = gameObject.GetComponentInChildren<Animator>();
      }

      void Update(){
           if (Time.time >= nextAttackTime){
                  //if (Input.GetKeyDown(KeyCode.Space))
                 if (Input.GetAxis("Attack") > 0){
                        playerFire();
                        nextAttackTime = Time.time + 1f / attackRate;
                  }
            }
      }

      void playerFire(){
            //animator.SetTrigger ("Fire");
            Vector3 fwd = (firePoint.position - fireBase.position).normalized;
            //Spawn a bullet that inherits rotation from the instantating object:
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, transform.rotation);
            projectile.GetComponent<Rigidbody>().AddForce(fwd * projectileSpeed, ForceMode.Impulse);
      }
}