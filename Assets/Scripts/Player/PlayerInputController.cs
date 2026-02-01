using Core.Events;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

namespace Player
{
    public class PlayerInputController : MonoBehaviour
    {
        [SerializeField] private InputActionReference move;
        [SerializeField] private InputActionReference look;
        [SerializeField] private float lookSpeed = 120f;

        void OnEnable()
        {
            move.action.Enable();
            look.action.Enable();
        }

        void OnDisable()
        {
            move.action.Disable();
            look.action.Disable();
        }

        void Update()
        {
            Vector2 input = move.action.ReadValue<Vector2>();
            Vector2 lookInput = look.action.ReadValue<Vector2>();

            Camera cam = Camera.main;
            if (cam != null)
            {
                // calculate the direction based on XZ of the camera
                Vector3 camForward = cam.transform.forward;
                camForward.y = 0f;
                camForward.Normalize();

                Vector3 camRight = cam.transform.right;
                camRight.y = 0f;
                camRight.Normalize();

                Vector3 moveDir = camForward * input.y + camRight * input.x;

                // shere direction with CharacterMotor
                GameEvents.OnCharacterMove?.Invoke(moveDir);
            }

            // camera events
            GameEvents.OnCameraYaw?.Invoke(lookInput.x * lookSpeed * Time.deltaTime);
            GameEvents.OnCameraPitch?.Invoke(-lookInput.y * lookSpeed * Time.deltaTime);
        }
    }
}