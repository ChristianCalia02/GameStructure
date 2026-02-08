using UnityEngine;
using Core.Interfaces;

namespace Character
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(CharacterAnimator))]
    public class CharacterRoot : MonoBehaviour, ICharacterContext
    {
        private CharacterController controller;
        private CharacterMotor motor;
        private CharacterStateMachine stateMachine;

        public IClimbState Climb { get; private set; }

        private bool wasMovingThisFrame;

        [Header("Movement Settings")]
        [SerializeField] private float walkSpeed = 3f;
        [SerializeField] private float runSpeed = 6f;
        [SerializeField] private float acceleration = 10f;
        [SerializeField] private float rotationSpeed = 10f;

        [Header("Gravity Settings")]
        [SerializeField] private float jumpHeight = 2f;
        [SerializeField] private float gravityMultiplier = 1f;

        void Awake()
        {
            controller = GetComponent<CharacterController>();
            Climb = GetComponent<IClimbState>();

            motor = new CharacterMotor(controller, walkSpeed, runSpeed, acceleration, rotationSpeed, jumpHeight, gravityMultiplier, this);

            stateMachine = new CharacterStateMachine();
            stateMachine.SetState(new State.GroundedState(this));
        }

        void Update()
        {
            stateMachine.Update();
            wasMovingThisFrame = motor.WasMovingThisFrame;
        }

        // INTERFACCIA
        public bool IsGrounded => controller.isGrounded;
        public bool IsMoving => wasMovingThisFrame;
        public bool IsSprinting => motor.IsSprinting && wasMovingThisFrame;

        public void UpdateMovementLogic() => motor.Update();
        public void UpdateMovementInput(Vector3 dir, float mag) => motor.Move(dir, mag);
        public void SetSprint(bool sprint) => motor.SetSprint(sprint);
        public void TryJump() => motor.Jump();
        public void SwitchToClimb() => stateMachine.SetState(new State.ClimbState(this));
        public void SwitchToGrounded() => stateMachine.SetState(new State.GroundedState(this));
        public void ClearMovementInput() => motor.ClearInput();
        public void ResetVerticalVelocity() => motor.ResetVerticalVelocity();
    }
}