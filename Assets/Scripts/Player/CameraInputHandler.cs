using Core.Events;
using UnityEngine.InputSystem;
using UnityEngine;

namespace Player
{
    public class CameraInputHandler
    {
        private InputActionReference lookRate;
        private InputActionReference toggleLook;
        private float cameraSpeedRate;

        public CameraInputHandler(InputActionReference lookRate, InputActionReference toggleLook, float speed)
        {
            this.lookRate = lookRate;
            this.toggleLook = toggleLook;
            this.cameraSpeedRate = speed;
        }

        public void Enable()
        {
            lookRate.action.Enable();
            toggleLook.action.started += OnToggleLook;
        }

        public void Disable()
        {
            lookRate.action.Disable();
            toggleLook.action.started -= OnToggleLook;
        }

        public void UpdateInput()
        {
            Vector2 look = lookRate.action.ReadValue<Vector2>();
            CameraEvents.OnYaw?.Invoke(look.x * cameraSpeedRate * Time.deltaTime);
            CameraEvents.OnPitch?.Invoke(-look.y * cameraSpeedRate * Time.deltaTime);
        }

        private void OnToggleLook(InputAction.CallbackContext ctx)
        {
            CameraEvents.OnToggleLook?.Invoke(true);
        }
    }
}