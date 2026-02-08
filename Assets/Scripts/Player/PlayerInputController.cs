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

        private MovementInputHandler movementHandler;
        private CameraInputHandler cameraHandler;
        private ActionInputHandler actionHandler;

        void Awake()
        {
            movementHandler = new MovementInputHandler(move, character);
            cameraHandler = new CameraInputHandler(lookRateAction, toggleLook, cameraSpeedRate);
            actionHandler = new ActionInputHandler(jump, sprint, climb, character);
        }

        void OnEnable()
        {
            movementHandler.Enable();
            cameraHandler.Enable();
            actionHandler.Enable();
        }

        void OnDisable()
        {
            movementHandler.Disable();
            cameraHandler.Disable();
            actionHandler.Disable();
        }

        void Update()
        {
            movementHandler.UpdateInput();
            cameraHandler.UpdateInput();
            actionHandler.UpdateInput();
        }
    }
}