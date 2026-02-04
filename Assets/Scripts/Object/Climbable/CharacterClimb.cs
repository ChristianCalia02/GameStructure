using Core.Events;
using Core.Interfaces;
using UnityEngine;

namespace Climb
{
    public class CharacterClimb : MonoBehaviour, IClimbState
    {
        public bool CanClimb => currentLedge != null;
        public bool IsHanging { get; private set; }

        public Vector3 LedgePoint => currentLedge.LedgePoint;
        public Vector3 LedgeNormal => currentLedge.LedgeNormal;

        private ClimbableLedge currentLedge;

        public void SetAvailableLedge(ClimbableLedge ledge)
        {
            currentLedge = ledge;
            GameEvents.OnClimbAvailable?.Invoke(true, ledge.LedgePoint);
        }

        public void ClearLedge()
        {
            currentLedge = null;
            GameEvents.OnClimbAvailable?.Invoke(false, Vector3.zero);
        }

        public void StartHang()
        {
            if (!CanClimb) return;

            IsHanging = true;
            GameEvents.OnClimbStarted?.Invoke(); 
        }

        public void StopHang()
        {
            IsHanging = false;
            GameEvents.OnClimbStopped?.Invoke(); 
        }
    }
}