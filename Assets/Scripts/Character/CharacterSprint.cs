using Core.Events;
using UnityEngine;

namespace Character
{
    [RequireComponent(typeof(CharacterMotor))]
    public class CharacterSprint : MonoBehaviour
    {
        [SerializeField] private float walkSpeed = 3f;
        [SerializeField] private float runSpeed = 6f;
        [SerializeField] private float deceleration = 10f;
        [SerializeField] private Animator animator;

        private CharacterMotor motor;
        private float currentSpeed;
        public bool isSprinting;
        public bool IsSprinting => isSprinting;

        void Awake()
        {
            motor = GetComponent<CharacterMotor>();
            currentSpeed = walkSpeed;
            motor.SetSpeed(currentSpeed);
        }

        void OnEnable()
        {
            GameEvents.OnCharacterSprint += SetSprint;
        }

        void OnDisable()
        {
            GameEvents.OnCharacterSprint -= SetSprint;
        }

        void Update()
        {
            float targetSpeed = isSprinting ? runSpeed : walkSpeed;

            currentSpeed = Mathf.MoveTowards(
                currentSpeed,
                targetSpeed,
                deceleration * Time.deltaTime
            );

            motor.SetSpeed(currentSpeed);
        }

        private void SetSprint(bool sprinting)
        {
            isSprinting = sprinting;
        }
    }
}