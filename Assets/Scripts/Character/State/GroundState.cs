using Core.Events;
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

        public void Enter()
        {
            CharacterEvents.OnMove += OnMove;
            CharacterEvents.OnSprint += OnSprint;
            CharacterEvents.OnJump += OnJump;
        }

        public void Exit()
        {
            CharacterEvents.OnMove -= OnMove;
            CharacterEvents.OnSprint -= OnSprint;
            CharacterEvents.OnJump -= OnJump;
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

        private void OnMove(Vector3 dir, float mag)
        {
            root.UpdateMovementInput(dir, mag);
        }

        private void OnSprint(bool sprint)
        {
            root.SetSprint(sprint);
        }

        private void OnJump()
        {
            root.TryJump();
        }
    }
}