using Core.Interfaces;

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
    }
}
