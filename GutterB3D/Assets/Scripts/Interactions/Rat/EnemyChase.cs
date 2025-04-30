// using UnityEngine;

// public class EnemyChase : MonoBehaviour
// {
//     public Transform groundCheck;
//     public float groundOffset = 0.1f;    // how high above the floor
//     public float chaseSpeed = 4f;
//     public float chaseRange = 10f;
//     public float attackRange = 1.5f;
//     public float attackCooldown = 1f;

//     private Transform player;
//     private float lastAttackTime;

//     void Start()
//     {
//         // Grab the player by tag
//         GameObject ply = GameObject.FindGameObjectWithTag("Player");
//         if (ply != null) player = ply.transform;
//     }

//     void Update()
//     {
//         RaycastHit hit;
//         if (Physics.Raycast(groundCheck.position, Vector3.down, out hit, 1f))
//         {
//             Vector3 pos = transform.position;
//             pos.y = hit.point.y + groundOffset;
//             transform.position = pos;
//         }
//         if (player == null) return;

//         float dist = Vector3.Distance(transform.position, player.position);
//         if (dist <= chaseRange)
//         {
//             // Rotate toward player
//             Vector3 dir = (player.position - transform.position).normalized;
//             Quaternion look = Quaternion.LookRotation(dir);
//             transform.rotation = Quaternion.Slerp(transform.rotation, look, Time.deltaTime * chaseSpeed);

//             // Move forward
//             transform.position = Vector3.MoveTowards(transform.position, player.position, chaseSpeed * Time.deltaTime);
//         }

//         // Attack if close enough and cooldown passed
//         if (dist <= attackRange && Time.time - lastAttackTime >= attackCooldown)
//         {
//             Attack();
//             lastAttackTime = Time.time;
//         }
//     }

//     void Attack()
//     {
//         // TODO: Hook into your slime’s health system.
//         Debug.Log("Rat attacks slime!");
//         // Example: player.GetComponent<SlimeHealth>()?.TakeDamage(1);
//     }
// }