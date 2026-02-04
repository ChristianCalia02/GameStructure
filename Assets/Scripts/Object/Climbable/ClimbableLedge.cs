using UnityEngine;

namespace Climb
{
    public class ClimbableLedge : MonoBehaviour
    {
        [Header("Ledge definition")]
        [SerializeField] private Transform ledgePoint;
        [SerializeField] private Transform ledgeNormal;

        public Vector3 LedgePoint => ledgePoint.position;
        public Vector3 LedgeNormal => ledgeNormal.forward;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(ledgePoint.position, 0.08f);

            Gizmos.color = Color.blue;
            Gizmos.DrawRay(
                ledgePoint.position,
                ledgeNormal.forward * 0.3f
            );
        }
    }
}