using Core.Interfaces;
using UnityEngine;

namespace Climb
{
    [RequireComponent(typeof(CharacterClimb))]
    public class ClimbDetector : MonoBehaviour
    {
        private CharacterClimb climb;
        private IClimbable currentClimbable;

        private void Awake()
        {
            climb = GetComponent<CharacterClimb>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out IClimbable climbable))
                return;

            currentClimbable = climbable;
            climb.SetAvailableClimbable(climbable);
        }

        private void OnTriggerExit(Collider other)
        {
            if (currentClimbable == null)
                return;

            if (other.TryGetComponent(out IClimbable climbable) && climbable == currentClimbable)
            {
                climb.ClearClimbable();
                currentClimbable = null;
            }
        }
    }
}