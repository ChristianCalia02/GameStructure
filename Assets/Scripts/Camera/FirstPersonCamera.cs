using UnityEngine;

namespace Cam
{
    public class FirstPersonCamera : CameraBase
    {
        [SerializeField] private float eyesLevel = 1.7f;
        [SerializeField] private float minPitch = -50f;
        [SerializeField] private float maxPitch = 50f;

        private float yaw;
        private float pitch;

        void LateUpdate()
        {
            if (target == null) return;

            transform.position = target.TransformPoint(Vector3.up * eyesLevel);
            transform.rotation = Quaternion.Euler(pitch, yaw, 0f);
        }

        public override void AdaptToTarget()
        {
            if (target == null) return;
            yaw = target.eulerAngles.y;
            pitch = 0f;
        }

        public override void RotateYaw(float degrees)
        {
            yaw += degrees;
        }

        public override void RotatePitch(float degrees)
        {
            pitch = Mathf.Clamp(pitch + degrees, minPitch, maxPitch);
        }
    }
}