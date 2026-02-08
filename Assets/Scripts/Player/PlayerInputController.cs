using Core.Events;
using UnityEngine;
using UnityEngine.InputSystem;
using Character;

namespace Player
{
    public class PlayerInputController : MonoBehaviour
    {
        [Header("Actions")]
        [SerializeField] private InputActionReference move;
        [SerializeField] private InputActionReference sprint;
        [SerializeField] private InputActionReference jump;
        [SerializeField] private InputActionReference lookRateAction;
        [SerializeField] private InputActionReference toggleLook;
        [SerializeField] private InputActionReference climb;

        [Header("Cam Settings")]
        [SerializeField] private float cameraSpeedRate = 30f;

        [SerializeField] private CharacterRoot character;

        void OnEnable()
        {
            // Events InputAction
            climb.action.started += OnClimb;
            toggleLook.action.started += OnToggleLook;
            jump.action.started += OnJumpStarted;

            // Enable action 
            move.action.Enable();
            sprint.action.Enable();
            jump.action.Enable();
            lookRateAction.action.Enable();
        }

        void OnDisable()
        {
            // Remove events
            climb.action.started -= OnClimb;
            toggleLook.action.started -= OnToggleLook;
            jump.action.started -= OnJumpStarted;

            // Disable action 
            move.action.Disable();
            sprint.action.Disable();
            jump.action.Disable();
            lookRateAction.action.Disable();
        }

        void Update()
        {
            HandleMovement();
            HandleSprint();
            HandleCamera();
        }

        // ===================== Direct Input =====================
        private void HandleMovement()
        {
            Vector2 input = move.action.ReadValue<Vector2>();
            Transform cam = Camera.main ? Camera.main.transform : transform;

            Vector3 dir = cam.forward * input.y + cam.right * input.x;
            dir.y = 0f;

            if (dir.sqrMagnitude > 1f) dir.Normalize();

            character.UpdateMovementInput(dir, 1f); // Directly to CharacterRoot
        }

        private void HandleSprint()
        {
            bool sprinting = sprint.action.ReadValue<float>() > 0.5f;
            character.SetSprint(sprinting); // Directly to CharacterRoot
        }

        private void HandleCamera()
        {
            Vector2 look = lookRateAction.action.ReadValue<Vector2>();
            CameraEvents.OnYaw?.Invoke(look.x * cameraSpeedRate * Time.deltaTime);
            CameraEvents.OnPitch?.Invoke(-look.y * cameraSpeedRate * Time.deltaTime);
        }

        private void OnJumpStarted(InputAction.CallbackContext ctx)
        {
            character.TryJump(); // Directly to CharacterRoot
        }

        // ===================== Global events =====================
        private void OnClimb(InputAction.CallbackContext ctx)
        {
            InputEvents.OnClimbPressed?.Invoke(); // Events for (UI, ClimbController)
        }

        private void OnToggleLook(InputAction.CallbackContext ctx)
        {
            CameraEvents.OnToggleLook?.Invoke(true); // Events for CameraBase
        }
    }
}