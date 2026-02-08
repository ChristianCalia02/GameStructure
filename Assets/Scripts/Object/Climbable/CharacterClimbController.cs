using Core.Events;
using Character;
using UnityEngine;

namespace Climb
{
    [RequireComponent(typeof(CharacterClimb))]
    public class CharacterClimbController : MonoBehaviour
    {
        private CharacterClimb climb;
        private CharacterRoot root;

        [SerializeField] private Transform leftHandTarget;
        [SerializeField] private Transform rightHandTarget;

        [SerializeField] private float verticalOffset = 0.0f;    // hight over the edge
        [SerializeField] private float forwardOffset = 0.1f;     // distance from the edge

        void Awake()
        {
            climb = GetComponent<CharacterClimb>();
            root = GetComponent<CharacterRoot>();
        }

        void OnEnable()
        {
            InputEvents.OnClimbPressed += TryClimb;
        }

        void OnDisable()
        {
            InputEvents.OnClimbPressed -= TryClimb;
        }

        private void TryClimb()
        {
            if (climb.IsHanging)
            {
                climb.StopHang();
                return;
            }

            if (!climb.CanClimb)
                return;

            climb.StartHang();
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