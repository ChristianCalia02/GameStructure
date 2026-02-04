using Core.Events;
using UnityEngine;
using UnityEngine.InputSystem;
using static PlasticPipe.PlasticProtocol.Messages.Serialization.ItemHandlerMessagesSerialization;

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
        //[SerializeField] private float cameraSpeedDirect = 0.25f;
        [SerializeField] private float cameraSpeedRate = 30f;

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

            GameEvents.OnCharacterMove?.Invoke(dir, 1f);
        }

        private void HandleSprint()
        {
            bool sprinting = sprint.action.ReadValue<float>() > 0.5f;
            GameEvents.OnCharacterSprint?.Invoke(sprinting);
        }

        private void HandleCamera()
        {
            Vector2 look = lookRateAction.action.ReadValue<Vector2>();

            GameEvents.OnCameraYaw?.Invoke(look.x * cameraSpeedRate * Time.deltaTime);
            GameEvents.OnCameraPitch?.Invoke(-look.y * cameraSpeedRate * Time.deltaTime);
        }

        private void OnToggleLook(InputAction.CallbackContext ctx)
        {
            GameEvents.OnCameraToggle?.Invoke(true);
        }

        private void OnJumpStarted(InputAction.CallbackContext ctx)
        {
            GameEvents.OnCharacterJump?.Invoke();
        }
        private void OnClimb(InputAction.CallbackContext ctx)
        {
            GameEvents.OnCharacterClimb?.Invoke();
        }
    }
}