using UnityEngine;
using Core.Events;
using Core.Interfaces;

namespace Character { 
    [RequireComponent(typeof(CharacterController))]
    public class CharacterMotor : MonoBehaviour, ICharacterMover
    {
        [SerializeField] private float speed = 3f;

        private CharacterController controller;
        private Vector3 moveInput;

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
            IsMoving = moveInput.sqrMagnitude > 0.01f;
            controller.Move(moveInput * speed * Time.deltaTime);
            moveInput = Vector3.zero;
        }

        public void Move(Vector3 direction)
        {
            moveInput += direction;
        }
    }
}