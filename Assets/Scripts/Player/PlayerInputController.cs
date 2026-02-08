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
        [SerializeField] private InputActionReference lookDirectAction;
        [SerializeField] private InputActionReference lookRateAction;
        [SerializeField] private InputActionReference toggleLook;
        [SerializeField] private InputActionReference climb;

        [Header("Cam")]
        [SerializeField] private float cameraSpeedRate = 30f;

        [SerializeField] private CharacterRoot character;

        void OnEnable()
        {
            climb.action.started += OnClimb;
            move.action.Enable();
            sprint.action.Enable();
            jump.action.Enable();
            lookDirectAction.action.Enable();
            lookRateAction.action.Enable();

            jump.action.started += OnJumpStarted;
            toggleLook.action.started += OnToggleLook;
        }

        void OnDisable()
        {
            jump.action.started -= OnJumpStarted;
            toggleLook.action.started -= OnToggleLook;

            
            move.action.Disable();
            sprint.action.Disable();
            jump.action.Disable();
            lookDirectAction.action.Disable();
            lookRateAction.action.Disable();
            climb.action.started -= OnClimb;
        }

        void Update()
        {
            HandleMovement();
            HandleSprint();
            HandleCamera();
        }

        private void HandleMovement()
        {
            Vector2 input = move.action.ReadValue<Vector2>();
            Transform cam = Camera.main.transform;
            Vector3 dir = cam.forward * input.y + cam.right * input.x;
            dir.y = 0f;

            if (dir.sqrMagnitude > 1f)
                dir.Normalize();


            character.MovementInput(dir, 1f); 
        }

        private void HandleSprint()
        {
            bool sprinting = sprint.action.ReadValue<float>() > 0.5f;
            character.HandleSprint(sprinting);
        }

        private void HandleCamera()
        {
            Vector2 look = lookRateAction.action.ReadValue<Vector2>();

            CameraEvents.OnYaw?.Invoke(look.x * cameraSpeedRate * Time.deltaTime);
            CameraEvents.OnPitch?.Invoke(-look.y * cameraSpeedRate * Time.deltaTime);
        }

        private void OnToggleLook(InputAction.CallbackContext ctx)
        {
            CameraEvents.OnToggleLook?.Invoke(true);
        }

        private void OnJumpStarted(InputAction.CallbackContext ctx)
        {
            character.HandleJump();
        }
        private void OnClimb(InputAction.CallbackContext ctx)
        {
            InputEvents.OnClimbPressed?.Invoke();
        }
    }
}