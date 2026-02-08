using UnityEngine;

namespace Core.Interfaces
{
    public interface ICharacterContext
    {
        bool IsGrounded { get; }
        bool IsMoving { get; }
        bool IsSprinting { get; }

        IClimbState Climb { get; }

        void UpdateMovementLogic();

        void SwitchToClimb();
        void SwitchToGrounded();

        void ClearMovementInput();
        void ResetVerticalVelocity();
        void UpdateMovementInput(Vector3 dir, float mag);
        void SetSprint(bool sprint);
        void TryJump();
    }
}