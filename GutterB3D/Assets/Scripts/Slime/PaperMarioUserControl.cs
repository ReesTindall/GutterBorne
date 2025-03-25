using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Characters.PaperMarioStyle
{
    [RequireComponent(typeof(PaperMarioCharacter))]
    public class PaperMarioUserControl : MonoBehaviour
    {
        private PaperMarioCharacter m_Character;
        private bool m_Jump;

        void Start()
        {
            m_Character = GetComponent<PaperMarioCharacter>();
        }

        void Update()
        {
            // Check for jump input.
            if (!m_Jump)
            {
                m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
            }
        }

        void FixedUpdate()
        {
            // Read input on both horizontal axes.
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            float v = CrossPlatformInputManager.GetAxis("Vertical");
            Vector3 moveInput = new Vector3(h, 0, v);

            // Pass movement and jump command to the character.
            m_Character.Move(moveInput, m_Jump);
            m_Jump = false;
        }
    }
}