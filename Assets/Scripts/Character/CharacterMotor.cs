using Core.Events;
using Core.Interfaces;
using UnityEngine;
using UnityCharCtrl = UnityEngine.CharacterController;

namespace Character
{
    [RequireComponent(typeof(UnityCharCtrl))]
    public class CharacterMotor : MonoBehaviour
    {
        private UnityCharCtrl ctrl;

        [SerializeField] private float rotationSpeed = 10f;
        private Vector3 moveDirection;
        private float verticalVelocity;

        [SerializeField] private float groundCheckDistance = 0.2f;
        [SerializeField] private float groundCheckRadius = 0.3f;
        [SerializeField] private LayerMask groundMask;

        [SerializeField] private Animator animator;
        public bool IsMoving { get; private set; }

        private float currentSpeed = 0f;

        void Awake()
        {
            ctrl = GetComponent<UnityCharCtrl>();
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
            // Movement
            Vector3 worldMove = moveDirection * currentSpeed;

            // Gravity
            if (!IsGrounded())
                verticalVelocity += Physics.gravity.y * Time.deltaTime;
            else
                verticalVelocity = -2f;

            worldMove.y = verticalVelocity;

            ctrl.Move(worldMove * Time.deltaTime);

            // Animations
            if (animator != null)
            {
                Vector3 localMove = transform.InverseTransformDirection(moveDirection);
                animator.SetFloat("MoveX", localMove.x, 0.1f, Time.deltaTime);
                animator.SetFloat("MoveZ", localMove.z, 0.1f, Time.deltaTime);
                IsMoving = moveDirection.sqrMagnitude > 0.01f;
            }

            // reset moveDirection all frame
            moveDirection = Vector3.zero;
        }

        public void Move(Vector3 direction, float magnitude)
        {
            moveDirection += direction.normalized * magnitude;

            if (moveDirection.sqrMagnitude > 0.001f)
            {
                Quaternion targetRot = Quaternion.LookRotation(moveDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotationSpeed * Time.deltaTime);
            }
        }

        public void SetSpeed(float speed)
        {
            currentSpeed = speed;
        }

        public void SetRunningAnimation(bool isRunning)
        {
            if (animator != null)
                animator.SetBool("IsRunning", isRunning);
        }

        private bool IsGrounded()
        {
            Vector3 origin = transform.position + Vector3.up * 0.1f;
            return Physics.SphereCast(origin, groundCheckRadius, Vector3.down, out _, groundCheckDistance + 0.1f, groundMask);
        }

        private void RotateCharacter(float degrees)
        {
            transform.Rotate(Vector3.up, degrees);
        }
    }
}