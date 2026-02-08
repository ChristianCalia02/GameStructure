using UnityEngine;
using UnityEngine.InputSystem;
using Core.Interfaces;

namespace Player
{
    public class MovementInputHandler
    {
        private InputActionReference move;
        private ICharacterContext character;

        public MovementInputHandler(InputActionReference moveAction, ICharacterContext ctx)
        {
            move = moveAction;
            character = ctx;
        }

        public void Enable() => move.action.Enable();
        public void Disable() => move.action.Disable();

        public void UpdateInput()
        {
            Vector2 input = move.action.ReadValue<Vector2>();
            Transform cam = Camera.main ? Camera.main.transform : null;
            if (cam == null) return;

            Vector3 dir = cam.forward * input.y + cam.right * input.x;
            dir.y = 0f;
            if (dir.sqrMagnitude > 1f) dir.Normalize();

            character.UpdateMovementInput(dir, 1f);
        }
    }
}