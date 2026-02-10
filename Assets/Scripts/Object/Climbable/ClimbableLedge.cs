using Core.Interfaces;
using UnityEngine;

namespace Climb
{
    [RequireComponent(typeof(Collider))]
    public class ClimbableLedge : MonoBehaviour, IClimbable
    {
        [Header("Snap Settings")]
        [Tooltip("Transform snapPoint (child of the object)")]
        [SerializeField] private Transform snapPoint;
        [Tooltip("Quanto sopra il punto centrale del Transform il player deve arrivare")]
        [SerializeField] private float verticalOffset = 1.5f;

        [Tooltip("Quanto davanti/indietro rispetto al muro il player deve fermarsi")]
        [SerializeField] private float forwardOffset = 0.2f;

        private void Reset()
        {
            Collider col = GetComponent<Collider>();
            col.isTrigger = true;
        }


        public bool CanClimb(ICharacterContext character)
        {
            return true;
        }

        public void OnClimbStart(ICharacterContext character)
        {
            Debug.Log($"Climb started on {name}");
        }

        public void OnClimbStop(ICharacterContext character)
        {
            Debug.Log($"Climb stopped on {name}");
        }

        public Transform GetClimbTransform()
        {
            return snapPoint != null ? snapPoint : transform;
        }

        public Vector3 GetSnapPosition()
        {
            Transform t = GetClimbTransform();
            return t.position + t.up * verticalOffset - t.forward * forwardOffset;
        }

        public Vector3 GetSnapForward()
        {
            Transform t = GetClimbTransform();
            return -t.forward;
        }


        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(GetSnapPosition(), 0.1f);

            Gizmos.color = Color.blue;
            Gizmos.DrawRay(GetSnapPosition(), -GetSnapForward() * 0.5f);
        }
    }
}