using Core.Events;
using UnityEngine;

namespace Character
{
    [RequireComponent(typeof(CharacterMotor))]
    public class CharacterJump : MonoBehaviour
    {
        private CharacterMotor motor;
        private Animator animator;

        void Awake()
        {
            motor = GetComponent<CharacterMotor>();
            animator = GetComponent<Animator>();
        }

        void OnEnable()
        {
            GameEvents.OnCharacterJump += TryJump;
        }

        void OnDisable()
        {
            GameEvents.OnCharacterJump -= TryJump;
        }

        private void TryJump()
        {
            if (!motor.IsGrounded) return;

            motor.Jump();

            if (animator != null)
                animator.SetTrigger("Jump");
        }
    }
}