using Core.Interfaces;
using UnityEngine;

namespace State
{
    public class GroundedState : ICharacterState
    {
        private ICharacterContext root;

        public GroundedState(ICharacterContext root)
        {
            this.root = root;
        }

        public void Enter() { }

        public void Exit() { }

        public void Update()
        {
            if (root.Climb != null && root.Climb.IsHanging)
            {
                root.SwitchToClimb();
                return;
            }

            root.UpdateMovementLogic();
        }

        

        public void HandleMove(Vector3 dir, float magnitude)
        {
            root.UpdateMovementInput(dir, magnitude);
        }

        public void HandleSprint(bool sprint)
        {
            root.SetSprint(sprint);
        }

        public void HandleJump()
        {
            root.TryJump();
        }
    }
}