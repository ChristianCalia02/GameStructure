using Core.Events;
using UnityEngine;

namespace Character {
    [RequireComponent(typeof(CharacterMotor))]
    public class CharacterFootsteps : MonoBehaviour
    {
        [SerializeField] private float stepInterval = 0.5f;

        private float stepTimer;
        private CharacterMotor motor;

        void Awake()
        {
            motor = GetComponent<CharacterMotor>();
        }

        void Update()
        {
            if (!motor.IsMoving) return;

            stepTimer += Time.deltaTime;

            if (stepTimer >= stepInterval)
            {
                stepTimer = 0f;
                GameEvents.OnFootstep?.Invoke(transform.position);
            }
        }
    }
}
