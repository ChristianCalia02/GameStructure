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
    }
}