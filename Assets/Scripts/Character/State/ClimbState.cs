using Core.Interfaces;

namespace State
{
    using UnityEngine;

    public class ClimbState : ICharacterState
    {
        private ICharacterContext root;

        public ClimbState(ICharacterContext root)
        {
            this.root = root;
        }

        public void Enter()
        {
            root.ClearMovementInput();
            root.ResetVerticalVelocity();
        }

        public void Exit() { }

        public void Update()
        {
            if (!root.Climb.IsHanging)
            {
                root.SwitchToGrounded();
                return;
            }
        }
        public void HandleMove(Vector3 dir, float magnitude) { }

        public void HandleSprint(bool sprint) { }

        public void HandleJump() { }
    }
}
