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
            CharacterEvents.OnJump += TryJump;
        }

        void OnDisable()
        {
            CharacterEvents.OnJump -= TryJump;
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