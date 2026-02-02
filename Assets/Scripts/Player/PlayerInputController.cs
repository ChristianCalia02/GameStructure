using Core.Events;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;
using UnityEngine.Windows;
using static UnityEngine.EventSystems.StandaloneInputModule;

namespace Player
{
    public class PlayerInputController : MonoBehaviour
    {
        [Header("Actions")]
        [SerializeField] 
        private InputActionReference move;
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
            move.action.Enable();
            lookDirectAction.action.Enable();
            lookRateAction.action.Enable();
        }

        void OnDisable()
        {
            move.action.Disable();
            lookDirectAction.action.Disable();
            lookRateAction.action.Disable();
        }

        void Update()
        {
            Vector2 inputMove = move.action.ReadValue<Vector2>();
            Transform camTransform = Camera.main.transform;
            
            //Charactermove
            GameEvents.OnCharacterMove?.Invoke(camTransform.forward, inputMove.y);
            GameEvents.OnCharacterMove?.Invoke(camTransform.right, inputMove.x);


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