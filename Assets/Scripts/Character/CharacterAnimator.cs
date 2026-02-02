using UnityEngine;

namespace Character
{
    [RequireComponent(typeof(CharacterMotor))]
    public class CharacterAnimator : MonoBehaviour
    {
        [SerializeField] private Animator animator;

        private CharacterMotor motor;
        private CharacterSprint sprint;

        void Awake()
        {
            motor = GetComponent<CharacterMotor>();
            sprint = GetComponent<CharacterSprint>();
        }

        void Update()
        {
            if (animator == null || motor == null)
                return;

            UpdateMovement();
            UpdateJump();
            UpdateSprint();
        }

        private void UpdateMovement()
        {
            animator.SetBool("IsMoving", motor.IsMoving);
        }

        private void UpdateJump()
        {
            animator.SetBool("IsJumping", !motor.IsGrounded);
        }

        private void UpdateSprint()
        {
            if (sprint == null) return;

            animator.SetBool("IsRunning", sprint.IsSprinting);
        }
    }
}