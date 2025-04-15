// using System.Collections.Generic;
// using System.Collections;
// using UnityEngine;

// public class PlayerProjectile : MonoBehaviour{

//       public int damage = 1;
//       //public GameObject hitVFX; //uncomment VFX #1
//       public AudioSource hitSFX; //uncomment SFX #1
//       public float SelfDestructTime = 2.0f; //Time after being fired to disappear
//       public float SelfDestructSFX = 0.5f; //Time after impact for playing sound effect
//       public GameObject projectileArt;

//       void Start(){
//            selfDestruct();
//       }

//       //if bullet hits a collider, play explosion animation, then destroy the effect and the bullet
//       void OnTriggerEnter(Collider other){
//          if (other.gameObject.layer == LayerMask.NameToLayer("Enemies")) {
//                //gameHandlerObj.playerGetHit(damage);
//                //other.gameObject.GetComponent<EnemyMeleeDamage>().TakeDamage(damage);
//           }
//          if (other.gameObject.tag != "Player") {
//                //GameObject animEffect = Instantiate (hitVFX, transform.position, Quaternion.identity); //uncomment VFX #2
//                hitSFX.pitch = Random.Range(0.9f, 1.2f);
//                hitSFX.PlayOneShot(hitSFX.clip); //uncomment SFX #2
//                gameObject.GetComponent<Collider>().enabled = false;
//                projectileArt.SetActive(false);
//                StartCoroutine(selfDestructHit());
//           }
          
//       }

//       IEnumerator selfDestructHit(){
//             yield return new WaitForSeconds(SelfDestructSFX);
//             Destroy (gameObject);
//       }

//       IEnumerator selfDestruct(){
//             yield return new WaitForSeconds(SelfDestructTime);
//             Destroy (gameObject);
//       }
// }