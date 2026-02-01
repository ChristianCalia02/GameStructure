using UnityEngine;
using UnityEngine.InputSystem;
using Core.Events;

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
            Vector2 m = move.action.ReadValue<Vector2>();
            Vector2 l = look.action.ReadValue<Vector2>();

            Camera cam = Camera.main;

            if (cam != null)
            {
                GameEvents.OnCharacterMove?.Invoke(
                    cam.transform.forward * m.y +
                    cam.transform.right * m.x
                );
            }

            GameEvents.OnCameraYaw?.Invoke(l.x * lookSpeed * Time.deltaTime);
            GameEvents.OnCameraPitch?.Invoke(-l.y * lookSpeed * Time.deltaTime);
        }
    }
}