using Core.Events;
using Core.Interfaces;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityCharCtrl = UnityEngine.CharacterController;

namespace Character { 
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(UnityCharCtrl))]
    public class CharacterMotor : MonoBehaviour, ICharacterMover
    {
        private UnityCharCtrl ctrl;

        [SerializeField] private float speed = 3f;
        [SerializeField] private float rotationSpeed = 10f;

        private CharacterController controller;
        private Vector3 moveDirection;

        [SerializeField] private float groundCheckDistance = 0.2f; // distance with foot
        [SerializeField] private float groundCheckRadius = 0.3f;   // ray of spherecast
        [SerializeField] private LayerMask groundMask;              // layer from ground
        private bool isGrounded;

        [SerializeField] private Animator animator;
        public bool IsMoving { get; private set; }

        void Awake()
        {
            controller = GetComponent<CharacterController>();
            ctrl = GetComponent<UnityCharCtrl>();
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

            Vector3 moveDirection = ConsumeMoveDirection();
            Vector3 worldMove = moveDirection * speed;

            ctrl.SimpleMove(worldMove);

            // update animations
            if (animator != null)
            {
                Vector3 localMove = transform.InverseTransformDirection(moveDirection);
                float moveMag = localMove.magnitude;

                animator.SetFloat("MoveX", localMove.x);
                animator.SetFloat("MoveZ", localMove.z);
            }
        }

        public void Move(Vector3 worldDirection, float magnitude = 1.0f)
        {
            moveDirection += worldDirection * magnitude;
        }
        public Vector3 ConsumeMoveDirection()
        {
            Vector3 currentMoveDirection = Vector3.ClampMagnitude(moveDirection, 1.0f);
            float mag = currentMoveDirection.magnitude;
            currentMoveDirection.y = 0.0f;
            currentMoveDirection = currentMoveDirection.normalized * mag;
            moveDirection = Vector3.zero;
            return currentMoveDirection;
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