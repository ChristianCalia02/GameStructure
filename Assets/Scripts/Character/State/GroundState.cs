using Core.Interfaces;

namespace State
{
    public class GroundedState : ICharacterState
    {
        private ICharacterContext root;

        public GroundedState(ICharacterContext root)
        {
            this.root = root;
        }

        public void Enter()
        {
            
        }

        public void Exit()
        {
        }

        public void Update()
        {
            if (root.Climb != null && root.Climb.IsHanging)
            {
                root.SwitchToClimb();
                return;
            }

            root.UpdateMovementLogic();
        }
    }
}
