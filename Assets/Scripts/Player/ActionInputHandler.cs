using Core.Events;
using UnityEngine.InputSystem;
using Core.Interfaces;

namespace Player {
    public class ActionInputHandler
    {
        private InputActionReference jump;
        private InputActionReference sprint;
        private InputActionReference climb;

        private ICharacterContext character;

        public ActionInputHandler(InputActionReference jump, InputActionReference sprint, InputActionReference climb, ICharacterContext ctx)
        {
            this.jump = jump;
            this.sprint = sprint;
            this.climb = climb;
            character = ctx;
        }

        public void Enable()
        {
            jump.action.Enable();
            sprint.action.Enable();
            climb.action.started += OnClimb;
            jump.action.started += OnJump;
        }

        public void Disable()
        {
            jump.action.Disable();
            sprint.action.Disable();
            climb.action.started -= OnClimb;
            jump.action.started -= OnJump;
        }

        public void UpdateInput()
        {
            bool sprinting = sprint.action.ReadValue<float>() > 0.5f;
            character.SetSprint(sprinting);
        }

        private void OnJump(InputAction.CallbackContext ctx) => character.TryJump();
        private void OnClimb(InputAction.CallbackContext ctx) => InputEvents.OnClimbPressed?.Invoke();
    }

}