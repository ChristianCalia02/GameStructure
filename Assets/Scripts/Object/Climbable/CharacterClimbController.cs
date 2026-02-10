using Character;
using Core.Events;
using Core.Interfaces;
using UnityEngine;

namespace Climb
{
    [RequireComponent(typeof(CharacterClimb))]
    [RequireComponent(typeof(CharacterRoot))]
    public class CharacterClimbController : MonoBehaviour
    {
        private CharacterClimb climb;
        private CharacterRoot root;

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
                climb.StopClimb(root);
                return;
            }

            if (!climb.TryStartClimb(root))
                return;

            SnapToClimbPoint();
        }

        private void SnapToClimbPoint()
        {
            Transform climbTransform = climb.GetCurrentClimbTransform();
            if (climbTransform == null) return;

            IClimbable climbable = climbTransform.GetComponent<IClimbable>();
            if (climbable == null) return;

            CharacterController controller = GetComponent<CharacterController>();
            controller.enabled = false;

            // Snap pos and rot
            transform.position = ((ClimbableLedge)climbable).GetSnapPosition();
            transform.forward = ((ClimbableLedge)climbable).GetSnapForward();

            controller.enabled = true;
        }
    }
}