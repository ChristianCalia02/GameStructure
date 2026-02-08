using Core.Events;
using Core.Interfaces;
using UnityEngine;

namespace Character
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(Animator))]
    public class CharacterRoot : MonoBehaviour, ICharacterContext
    {
        private CharacterController controller;
        private Animator animator;

        private CharacterMovement movement;
        private CharacterGravity gravity;
        private CharacterRotation rotation;
        private CharacterSprint sprint;

        // climbing 
        private IClimbState climb;

        // State Machine
        private CharacterStateMachine stateMachine;
        private ICharacterState groundedState;
        private ICharacterState climbState;

        [Header("Movement Settings")]
        [SerializeField] private float walkSpeed = 3f;
        [SerializeField] private float runSpeed = 6f;
        [SerializeField] private float acceleration = 10f;
        [SerializeField] private float rotationSpeed = 10f;

        [Header("Gravity Settings")]
        [SerializeField] private float jumpHeight = 2f;
        [SerializeField] private float gravityMultiplier = 1f;

        [Header("Animation")]
        [SerializeField] private Animator characterAnimator;


        // ICharacterContext Properties
        public bool IsGrounded => controller.isGrounded;
        public bool IsMoving => movement.IsMoving;
        public bool IsSprinting => sprint.IsSprinting;
        public IClimbState Climb => climb;


        void Awake()
        {
            controller = GetComponent<CharacterController>();
            animator = characterAnimator ?? GetComponent<Animator>();
            climb = GetComponent<IClimbState>();

            // Initialize pure logic modules
            movement = new CharacterMovement();
            gravity = new CharacterGravity(jumpHeight, gravityMultiplier);
            rotation = new CharacterRotation(rotationSpeed);
            sprint = new CharacterSprint(walkSpeed, runSpeed, acceleration);

            // State Machine
            stateMachine = new CharacterStateMachine();

            groundedState = new State.GroundedState(this);
            climbState = new State.ClimbState(this);
        }

        void Start()
        {
            stateMachine.SetState(groundedState);
        }

        void OnEnable()
        {
            CharacterEvents.OnMove += movement.Move;
            CharacterEvents.OnSprint += sprint.SetSprint;
            CharacterEvents.OnJump += HandleJump;
        }

        void OnDisable()
        {
            CharacterEvents.OnMove -= movement.Move;
            CharacterEvents.OnSprint -= sprint.SetSprint;
            CharacterEvents.OnJump -= HandleJump;
        }

        void Update()
        {
            //movement.Reset();
            stateMachine.Update();
            UpdateAnimator();
        }


        public void UpdateMovementLogic()
        {
            sprint.Update();
            movement.SetSpeed(sprint.CurrentSpeed);

            gravity.UpdateGravity(IsGrounded);

            Vector3 move = movement.HorizontalMove +
                           Vector3.up * gravity.VerticalVelocity;

            controller.Move(move * Time.deltaTime);

            rotation.RotateTowards(transform, movement.HorizontalMove);

            movement.Reset();
        }


        // Jump
        private void HandleJump()
        {
            if (Climb != null && Climb.IsHanging)
                return;

            if (!IsGrounded)
                return;

            gravity.Jump();
            animator?.SetTrigger("Jump");
        }


        // Animator
        private void UpdateAnimator()
        {
            if (animator == null) return;

            animator.SetBool("IsGrounded", IsGrounded);
            animator.SetBool("IsMoving", IsMoving);
            animator.SetBool("IsRunning", IsSprinting);
            animator.SetBool("IsHanging", Climb != null && Climb.IsHanging);
        }


        // State Switching (ICharacterContext)
        public void SwitchToClimb()
        {
            stateMachine.SetState(climbState);
        }

        public void SwitchToGrounded()
        {
            stateMachine.SetState(groundedState);
        }
        public void ResetVerticalVelocity()
        {
            gravity.Reset();
        }
        public void ClearMovementInput()
        {
            movement.Reset();
        }
    }
}