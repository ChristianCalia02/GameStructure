using UnityEngine;
using Core.Interfaces;

namespace Character
{
    public class CharacterMotor
    {
        private readonly CharacterController controller;
        private readonly CharacterMovement movement = new CharacterMovement();
        private readonly CharacterGravity gravity;
        private readonly CharacterRotation rotation;
        private readonly CharacterSprint sprint;
        private readonly ICharacterContext context;

        public bool WasMovingThisFrame { get; private set; }
        public bool IsSprinting => sprint.IsSprinting;

        public CharacterMotor(CharacterController controller, float walkSpeed, float runSpeed, float acceleration, float rotationSpeed, float jumpHeight, float gravityMultiplier, ICharacterContext ctx)
        {
            this.controller = controller;
            gravity = new CharacterGravity(jumpHeight, gravityMultiplier);
            rotation = new CharacterRotation(rotationSpeed);
            sprint = new CharacterSprint(walkSpeed, runSpeed, acceleration);
            context = ctx;
        }

        public Vector3 HorizontalMove => movement.HorizontalMove;

        public void Move(Vector3 direction, float magnitude) => movement.Move(direction, magnitude);
        public void SetSprint(bool sprinting) => sprint.SetSprint(sprinting);

        public void Jump()
        {
            if (context.IsGrounded)
                gravity.Jump();
        }

        public void ClearInput() => movement.Reset();
        public void ResetVerticalVelocity() => gravity.Reset();

        public void Update()
        {
            sprint.Update();
            movement.SetSpeed(sprint.CurrentSpeed);

            gravity.UpdateGravity(context.IsGrounded);

            Vector3 horizontal = movement.HorizontalMove;

            // Aggiorna il flag prima di resettare
            WasMovingThisFrame = horizontal.sqrMagnitude > 0.001f;

            Vector3 move = horizontal + Vector3.up * gravity.VerticalVelocity;
            controller.Move(move * Time.deltaTime);

            rotation.RotateTowards(controller.transform, horizontal);

            movement.Reset();
        }
    }
}