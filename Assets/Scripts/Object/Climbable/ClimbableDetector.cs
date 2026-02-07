using Climb;
using UnityEngine;


namespace Climb {
    [RequireComponent(typeof(CharacterClimb))]
    public class ClimbDetector : MonoBehaviour
    {
        private CharacterClimb climb;
        private ClimbableLedge currentLedge;

        void Awake()
        {
            climb = GetComponentInParent<CharacterClimb>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out ClimbableLedge ledge))
                return;

            currentLedge = ledge;
            climb.SetAvailableLedge(ledge);
        }

        private void OnTriggerExit(Collider other)
        {
            if (currentLedge == null)
                return;

            if (other.GetComponent<ClimbableLedge>() == currentLedge)
            {
                climb.ClearLedge();
                currentLedge = null;
            }
        }
    }
}