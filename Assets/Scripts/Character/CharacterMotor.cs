using Core.Events;
using UnityEngine;

namespace Character
{
    [RequireComponent(typeof(CharacterController))]
    public class CharacterMotor : MonoBehaviour
    {
        private CharacterController controller;

        [Header("Movement")]
        [SerializeField] private float rotationSpeed = 10f;
        private Vector3 horizontalMove;
        private float currentSpeed;

        [Header("Jump & Gravity")]
        [SerializeField] private float jumpHeight = 2f;
        [SerializeField] private float gravityMultiplier = 1f;
        private float verticalVelocity;

        public bool IsMoving { get; private set; }
        public bool IsGrounded => controller.isGrounded;

        private bool hasMoveInput;

        void Awake()
        {
            controller = GetComponent<CharacterController>();
        }

        void OnEnable()
        {
            GameEvents.OnCharacterMove += Move;
            GameEvents.OnCameraYaw += RotateCharacter;
        }

        void OnDisable()
        {
            GameEvents.OnCharacterMove -= Move;
            GameEvents.OnCameraYaw -= RotateCharacter;
        }

        void Update()
        {
            ApplyGravity();
            ApplyMovement();
        }

        void LateUpdate()
        {
            UpdateIsMoving();
        }

        private void UpdateIsMoving()
        {
            IsMoving = hasMoveInput;
            hasMoveInput = false;
        }

        private void ApplyGravity()
        {
            if (IsGrounded && verticalVelocity < 0f)
                verticalVelocity = -4f;

            verticalVelocity += Physics.gravity.y * gravityMultiplier * Time.deltaTime;
        }

        private void ApplyMovement()
        {
            Vector3 move =
                horizontalMove * currentSpeed +
                Vector3.up * verticalVelocity;

            controller.Move(move * Time.deltaTime);

            horizontalMove = Vector3.zero;
        }

        public void Move(Vector3 direction, float magnitude)
        {
            if (direction.sqrMagnitude > 0.001f)
            {
                hasMoveInput = true;
                horizontalMove += direction.normalized * magnitude;

                Quaternion targetRot = Quaternion.LookRotation(horizontalMove);
                transform.rotation = Quaternion.Slerp(
                    transform.rotation,
                    targetRot,
                    rotationSpeed * Time.deltaTime
                );
            }
        }

        public void SetSpeed(float speed)
        {
            currentSpeed = speed;
        }

        public void Jump()
        {
            verticalVelocity = Mathf.Sqrt(
                2f * jumpHeight * -Physics.gravity.y
            );
        }

        private void RotateCharacter(float degrees)
        {
            transform.Rotate(Vector3.up, degrees);
        }
    }
}