using System.Collections.Generic;
using UnityEngine;

namespace Character
{
    [RequireComponent(typeof(CharacterMotor))]
    public class CharacterAnimator : MonoBehaviour
    {
        [SerializeField] private Animator animator;

        private CharacterMotor motor;
        private CharacterSprint sprint;

        private bool lastMoving;
        private bool lastRunning;
        private bool lastJumping;

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
            //animator.SetBool("IsMoving", motor.IsMoving);
            //Debug.Log("Move");
            if (motor.IsMoving != lastMoving)
            {
                animator.SetBool("IsMoving", motor.IsMoving);
                lastMoving = motor.IsMoving;
                Debug.Log("Move");
            }

        }

        private void UpdateJump()
        {
            if (motor.IsGrounded != lastJumping)
            {
                lastJumping = motor.IsGrounded;
                Debug.Log("Jump");
            }
        }

        private void UpdateSprint()
        {
            if(sprint.IsSprinting != lastRunning) { 
                if (sprint == null) return;

                animator.SetBool("IsRunning", sprint.IsSprinting);
                lastRunning = sprint.IsSprinting;
                Debug.Log("Sprint");
            }
        }
    }
}