using Core.Events;
using UnityEngine;

namespace Character
{
    [RequireComponent(typeof(Animator))]
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

        void OnEnable()
        {
            GameEvents.OnClimbStarted += HandleClimbStarted;
            GameEvents.OnClimbStopped += HandleClimbStopped;
        }

        void OnDisable()
        {
            GameEvents.OnClimbStarted -= HandleClimbStarted;
            GameEvents.OnClimbStopped -= HandleClimbStopped;
        }

        private void HandleClimbStarted()
        {
            animator.SetBool("IsHanging", true);
            animator.Update(0f);
        }

        private void HandleClimbStopped()
        {
            animator.SetBool("IsHanging", false);
            animator.Update(0f);
        }

        void Update()
        {
            if (motor == null) return;

            animator.SetBool("IsGrounded", motor.IsGrounded);

            // movement
            animator.SetBool("IsMoving", motor.IsMoving);

            // sprint
            if (sprint != null)
                animator.SetBool("IsRunning", sprint.IsSprinting);
        }
    }
}