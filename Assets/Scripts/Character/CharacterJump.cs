using Core.Events;
using UnityEngine;

namespace Character
{
    [RequireComponent(typeof(CharacterMotor))]
    public class CharacterJump : MonoBehaviour
    {
        private CharacterMotor motor;

        void Awake()
        {
            motor = GetComponent<CharacterMotor>();
        }

        void OnEnable()
        {
            GameEvents.OnCharacterJump += TryJump;
        }

        void OnDisable()
        {
            GameEvents.OnCharacterJump -= TryJump;
        }

        private void TryJump()
        {
            if (motor.IsGrounded)
                motor.Jump();
        }
    }
}