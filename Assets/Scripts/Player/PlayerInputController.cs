using Core.Events;
using UnityEngine;
using UnityEngine.InputSystem;


namespace Player
{
    public class PlayerInputController : MonoBehaviour
    {
        [Header("Actions")]
        [SerializeField] 
        private InputActionReference move;
        [SerializeField] 
        private InputActionReference sprint;
        [SerializeField]
        private InputActionReference lookDirectAction;
        [SerializeField]
        private InputActionReference lookRateAction;

        [Header("Cam")]
        [SerializeField]
        [Range(0f, 5f)]
        private float cameraSpeedDirect = 0.25f;
        [SerializeField]
        private bool invertCameraYawDirect = false;
        [SerializeField]
        private bool invertCameraPitchDirect = false;

        [SerializeField]
        [Range(0f, 360f)]
        private float cameraSpeedRate = 30.0f;
        [SerializeField]
        private bool invertCameraYawRate = false;
        [SerializeField]
        private bool invertCameraPitchRate = false;


        void OnEnable()
        {
            sprint.action.Enable();
            move.action.Enable();
            lookDirectAction.action.Enable();
            lookRateAction.action.Enable();
        }

        void OnDisable()
        {
            sprint.action.Disable();
            move.action.Disable();
            lookDirectAction.action.Disable();
            lookRateAction.action.Disable();
        }

        void Update()
        {
            Vector2 inputMove = move.action.ReadValue<Vector2>();
            Transform camTransform = Camera.main.transform;

            Vector3 moveDir = camTransform.forward * inputMove.y + camTransform.right * inputMove.x;
            moveDir.y = 0f;
            if (moveDir.sqrMagnitude > 1f)
                moveDir.Normalize();

            GameEvents.OnCharacterMove?.Invoke(moveDir, 1f);

            bool isSprinting = sprint.action.ReadValue<float>() > 0.5f;
            GameEvents.OnCharacterSprint?.Invoke(isSprinting);

            Vector2 inputLook = lookRateAction.action.ReadValue<Vector2>();
            
            //Camrate
            GameEvents.OnCameraYaw?.Invoke(inputLook.x * cameraSpeedRate * Time.deltaTime * (invertCameraYawDirect ? -1f : 1f));
            GameEvents.OnCameraPitch?.Invoke(-inputLook.y * cameraSpeedRate * Time.deltaTime * (invertCameraPitchDirect ? -1f : 1f));

            inputLook = lookDirectAction.action.ReadValue<Vector2>();
            
            //Campitch
            GameEvents.OnCameraYaw?.Invoke(inputLook.x * cameraSpeedDirect * (invertCameraYawRate ? -1f : 1f));
            GameEvents.OnCameraPitch?.Invoke(-inputLook.y * cameraSpeedDirect * (invertCameraPitchRate ? -1f : 1f));

        }
    }
}