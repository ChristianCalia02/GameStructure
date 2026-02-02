using UnityEngine;
using UnityEngine.Assertions;

namespace Cam
{
    public class FirstPersonCamera : CameraBase
    {
        [SerializeField] private float eyesLevel = 1.7f;
        [SerializeField] private float minPitch = -50f;
        [SerializeField] private float maxPitch = 50f;

        private float yaw = 0f;
        private float pitch = 0f;

        void LateUpdate()
        {
            AdaptPosition();
            AdaptRotation();
        }

        public override void AdaptToTarget()
        {
            Assert.IsNotNull(target);

            pitch = 0.0f;
            //yaw = target.eulerAngles.y;

            //LateUpdate();
        }

        private void AdaptPosition()
        {
            if (target == null)
                return;

            //transform.position = target.TransformPoint(Vector3.up * eyesLevel);
            transform.position = target.position + (Vector3.up * eyesLevel + target.forward * 0.2f);
        }
        private void AdaptRotation()
        {
            Quaternion rotation = Quaternion.identity;

            rotation = Quaternion.AngleAxis(pitch, Vector3.right) * rotation;

            rotation = Quaternion.AngleAxis(yaw, Vector3.up) * rotation;

            transform.rotation = rotation;
        }
        public override void RotateYaw(float degrees)
        {
            yaw += degrees;
            yaw %= 360.0f;
        }

        public override void RotatePitch(float degrees)
        {
            pitch += degrees;
            pitch = Mathf.Clamp(pitch, minPitch, maxPitch);
        }
    }
}