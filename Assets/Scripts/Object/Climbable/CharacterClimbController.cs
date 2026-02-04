using Core.Events;
using Character;
using UnityEngine;

namespace Climb
{
    [RequireComponent(typeof(CharacterClimb))]
    public class CharacterClimbController : MonoBehaviour
    {
        private CharacterClimb climb;
        private CharacterMotor motor;

        [SerializeField] private Transform leftHandTarget;
        [SerializeField] private Transform rightHandTarget;

        [SerializeField] private float verticalOffset = 0.0f;    // hight over the edge
        [SerializeField] private float forwardOffset = 0.1f;     // distance from the edge

        void Awake()
        {
            climb = GetComponent<CharacterClimb>();
            motor = GetComponent<CharacterMotor>();
        }

        void OnEnable()
        {
            GameEvents.OnCharacterClimb += TryClimb;
        }

        void OnDisable()
        {
            GameEvents.OnCharacterClimb -= TryClimb;
        }

        private void TryClimb()
        {
            if (climb.IsHanging)
            {
                climb.StopHang();
                motor.enabled = true;
                return;
            }

            if (!climb.CanClimb)
                return;

            climb.StartHang();
            motor.enabled = false;
            SnapToLedge();
        }

        private void SnapToLedge()
        {
            var controller = GetComponent<CharacterController>();

            Vector3 basePos = climb.LedgePoint - climb.LedgeNormal * forwardOffset;

            controller.enabled = false;

            transform.position = basePos + Vector3.up * verticalOffset;

            transform.forward = -climb.LedgeNormal;

            controller.enabled = true;
        }
    }
}