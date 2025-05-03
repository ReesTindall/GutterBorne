// using UnityEngine;

// namespace UnityStandardAssets.Characters.PaperMarioStyle
// {
//     [RequireComponent(typeof(Rigidbody))]
//     [RequireComponent(typeof(CapsuleCollider))]
//     [RequireComponent(typeof(Animator))]
//     public class PaperMarioCharacter : MonoBehaviour
//     {
//         [SerializeField] float m_JumpPower = 12f;
//         [Range(1f, 4f)]
//         [SerializeField] float m_GravityMultiplier = 2f;
//         [SerializeField] float m_MoveSpeedMultiplier = 1f;
//         [SerializeField] float m_AnimSpeedMultiplier = 1f;
//         [SerializeField] float m_GroundCheckDistance = 0.1f;

//         Rigidbody m_Rigidbody;
//         Animator m_Animator;
//         bool m_IsGrounded;
//         float m_OrigGroundCheckDistance;
//         CapsuleCollider m_Capsule;
//         float m_CapsuleHeight;
//         Vector3 m_CapsuleCenter;

//         void Start()
//         {
//             m_Animator = GetComponent<Animator>();
//             m_Rigidbody = GetComponent<Rigidbody>();
//             m_Capsule = GetComponent<CapsuleCollider>();
//             m_CapsuleHeight = m_Capsule.height;
//             m_CapsuleCenter = m_Capsule.center;
            
//             // Lock rotation so physics won’t spin your character.
//             m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
//             m_OrigGroundCheckDistance = m_GroundCheckDistance;
//         }

//         /// <summary>
//         /// Moves the character according to a full horizontal vector (x and z) and processes jump input.
//         /// </summary>
//         /// <param name="move">Movement vector on the XZ plane (use values between -1 and 1)</param>
//         /// <param name="jump">True if jump is requested</param>
//         public void Move(Vector3 move, bool jump)
//         {
//             // Clamp the movement vector to ensure its magnitude is at most 1.
//             if (move.magnitude > 1f)
//                 move.Normalize();

//             // Multiply by our movement speed.
//             move *= m_MoveSpeedMultiplier;

//             // Check if we’re on the ground.
//             CheckGroundStatus();

//             // If grounded, update horizontal velocity (keeping the current vertical velocity).
//             if (m_IsGrounded)
//             {
//                 Vector3 targetVelocity = new Vector3(move.x, m_Rigidbody.velocity.y, move.z);
//                 m_Rigidbody.velocity = targetVelocity;
//             }

//             // Jump if requested and grounded.
//             if (jump && m_IsGrounded)
//             {
//                 m_Rigidbody.velocity = new Vector3(m_Rigidbody.velocity.x, m_JumpPower, m_Rigidbody.velocity.z);
//                 m_IsGrounded = false;
//             }

//             // Update animator parameters.
//             // Make sure your Animator Controller has parameters: "Speed" (float), "OnGround" (bool) and "Jump" (float).
//             m_Animator.SetFloat("Speed", move.magnitude);
//             m_Animator.SetBool("OnGround", m_IsGrounded);
//             if (!m_IsGrounded)
//                 m_Animator.SetFloat("Jump", m_Rigidbody.velocity.y);

//             // Always face the camera.
//             // In this case, we set the forward vector to be opposite the camera's forward (while flattening the y-axis).
//             if (Camera.main != null)
//             {
//                 Vector3 lookDir = -Camera.main.transform.forward;
//                 lookDir.y = 0;
//                 if (lookDir.sqrMagnitude > 0.001f)
//                 {
//                     transform.rotation = Quaternion.LookRotation(lookDir);
//                 }
//             }
//         }

//         // Uses a raycast downward to check if the character is grounded.
//         void CheckGroundStatus()
//         {
//             RaycastHit hitInfo;
// #if UNITY_EDITOR
//             Debug.DrawLine(transform.position + Vector3.up * 0.1f,
//                            transform.position + Vector3.up * 0.1f + Vector3.down * m_GroundCheckDistance);
// #endif
//             if (Physics.Raycast(transform.position + Vector3.up * 0.1f, Vector3.down, out hitInfo, m_GroundCheckDistance))
//             {
//                 m_IsGrounded = true;
//                 m_Animator.applyRootMotion = true;
//             }
//             else
//             {
//                 m_IsGrounded = false;
//                 m_Animator.applyRootMotion = false;
//             }
//         }
//     }
// }