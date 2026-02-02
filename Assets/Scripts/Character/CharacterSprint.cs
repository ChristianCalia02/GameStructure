using Core.Events;
using UnityEngine;

namespace Character
{
    [RequireComponent(typeof(CharacterMotor))]
    public class CharacterSprint : MonoBehaviour
    {
        [SerializeField] private float walkSpeed = 3f;
        [SerializeField] private float runSpeed = 6f;
        [SerializeField] private float deceleration = 10f; // velocità di rallentamento

        private CharacterMotor motor;
        private float currentSpeed;
        private float targetSpeed;
        private bool isSprinting = false;

        void Awake()
        {
            motor = GetComponent<CharacterMotor>();
            currentSpeed = walkSpeed;
            targetSpeed = walkSpeed;
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
            if (!isSprinting)
            {
                currentSpeed = Mathf.MoveTowards(currentSpeed, walkSpeed, deceleration * Time.deltaTime);
                motor.SetSpeed(currentSpeed);
            }
            else
            {
                currentSpeed = runSpeed;
                motor.SetSpeed(currentSpeed);
            }
        }

        private void SetSprint(bool sprinting)
        {
            isSprinting = sprinting;

            if (isSprinting)
            {
                currentSpeed = runSpeed;
                motor.SetSpeed(runSpeed);
                motor.SetRunningAnimation(true);
            }
            else
            {
                motor.SetRunningAnimation(false);
            }
        }
    }
}