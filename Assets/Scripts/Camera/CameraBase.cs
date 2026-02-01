using UnityEngine;
using Core.Interfaces;
using Core.Events;

namespace Cam {
    [RequireComponent(typeof(Camera))]
    public abstract class CameraBase : MonoBehaviour, ICameraLook
    {
        protected Transform target;

        protected virtual void OnEnable()
        {
            GameEvents.OnCameraTargetChanged += SetTarget;
            GameEvents.OnCameraYaw += RotateYaw;
            GameEvents.OnCameraPitch += RotatePitch;
        }

        protected virtual void OnDisable()
        {
            GameEvents.OnCameraTargetChanged -= SetTarget;
            GameEvents.OnCameraYaw -= RotateYaw;
            GameEvents.OnCameraPitch -= RotatePitch;
        }

        private void SetTarget(Transform t)
        {
            target = t;
            AdaptToTarget();
        }

        public abstract void AdaptToTarget();
        public abstract void RotateYaw(float degrees);
        public abstract void RotatePitch(float degrees);
    }
}