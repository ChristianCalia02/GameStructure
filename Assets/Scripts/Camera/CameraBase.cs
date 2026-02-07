using UnityEngine;
using Core.Interfaces;
using Core.Events;

namespace Cam {
    [RequireComponent(typeof(Camera))]
    public abstract class CameraBase : MonoBehaviour, ICameraLook
    {
        protected Transform target;
        public Camera cam { get; private set; }

        protected virtual void OnEnable()
        {
            cam = GetComponent<Camera>();

            CameraEvents.OnTargetChanged += SetTarget;
            CameraEvents.OnYaw += RotateYaw;
            CameraEvents.OnPitch += RotatePitch;
            CameraEvents.OnToggleLook += OnToggleLook;
        }

        protected virtual void OnDisable()
        {
            CameraEvents.OnTargetChanged -= SetTarget;
            CameraEvents.OnYaw -= RotateYaw;
            CameraEvents.OnPitch -= RotatePitch;
            CameraEvents.OnToggleLook -= OnToggleLook;
        }

        private void SetTarget(Transform t)
        {
            target = t;
            AdaptToTarget();
        }

        protected virtual void OnToggleLook(bool _)
        {
            ToggleLook();
        }

        public abstract void ToggleLook();

        public abstract void AdaptToTarget();
        public abstract void RotateYaw(float degrees);
        public abstract void RotatePitch(float degrees);
    }
}