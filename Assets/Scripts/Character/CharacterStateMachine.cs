using Core.Interfaces;
using UnityEngine;

namespace Character
{
    public class CharacterStateMachine
    {
        private ICharacterState currentState;

        public void SetState(ICharacterState newState)
        {
            currentState?.Exit();
            currentState = newState;
            currentState?.Enter();
        }

        public void Update()
        {
            currentState?.Update();
        }

        public void HandleMove(Vector3 dir, float magnitude)
        {
            currentState?.HandleMove(dir, magnitude);
        }

        public void HandleSprint(bool sprint)
        {
            currentState?.HandleSprint(sprint);
        }

        public void HandleJump()
        {
            currentState?.HandleJump();
        }
    }
}
