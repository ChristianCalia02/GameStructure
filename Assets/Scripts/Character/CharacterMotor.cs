using UnityEngine;
using Core.Events;
using Core.Interfaces;

namespace Character { 
    [RequireComponent(typeof(CharacterController))]
    public class CharacterMotor : MonoBehaviour, ICharacterMover
    {
        [SerializeField] private float speed = 3f;
        [SerializeField] private float rotationSpeed = 10f;

        private CharacterController controller;
        private Vector3 moveInput;

        [SerializeField] private float groundCheckDistance = 0.2f; // distance with foot
        [SerializeField] private float groundCheckRadius = 0.3f;   // ray of spherecast
        [SerializeField] private LayerMask groundMask;              // layer from ground
        private bool isGrounded;

        [SerializeField] private Animator animator;
        public bool IsMoving { get; private set; }

        void Awake()
        {
            controller = GetComponent<CharacterController>();
        }

        void OnEnable()
        {
            GameEvents.OnCharacterMove += Move;
        }

        void OnDisable()
        {
            GameEvents.OnCharacterMove -= Move;
        }

        void Update()
        {
            CheckGround();

            Vector3 move = moveInput;
            move.y = 0f;

            // apply movement
            controller.Move(move * speed * Time.deltaTime);

            // rotate the player in the movement's direction
            if (move.sqrMagnitude > 0.01f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(move);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }

            // update animations
            if (animator != null)
            {
                Vector3 localMove = transform.InverseTransformDirection(move);
                float moveMag = localMove.magnitude;

                animator.SetFloat("MoveX", localMove.x);
                animator.SetFloat("MoveY", localMove.z);
                animator.SetFloat("MoveMagnitude", moveMag);
            }

            IsMoving = move.sqrMagnitude > 0.01f && isGrounded;
            moveInput = Vector3.zero;
        }

        public void Move(Vector3 direction)
        {
            moveInput += direction;
        }

        private void CheckGround()
        {
            Vector3 origin = transform.position + Vector3.up * 0.1f; // leggermente sopra i piedi
            isGrounded = Physics.SphereCast(
                origin,
                groundCheckRadius,
                Vector3.down,
                out RaycastHit hit,
                groundCheckDistance + 0.1f,
                groundMask,
                QueryTriggerInteraction.Ignore
            );
        }
    }
}