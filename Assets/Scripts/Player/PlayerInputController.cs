using Core.Events;
using UnityEngine;
using UnityEngine.InputSystem;

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

        [Header("Cam")]
        [SerializeField] private float cameraSpeedDirect = 0.25f;
        [SerializeField] private float cameraSpeedRate = 30f;

        void OnEnable()
        {
            move.action.Enable();
            sprint.action.Enable();
            jump.action.Enable();
            lookDirectAction.action.Enable();
            lookRateAction.action.Enable();

            jump.action.started += OnJumpStarted;
        }

        void OnDisable()
        {
            jump.action.started -= OnJumpStarted;

            move.action.Disable();
            sprint.action.Disable();
            jump.action.Disable();
            lookDirectAction.action.Disable();
            lookRateAction.action.Disable();
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

        private void OnJumpStarted(InputAction.CallbackContext ctx)
        {
            GameEvents.OnCharacterJump?.Invoke();
        }
    }
}