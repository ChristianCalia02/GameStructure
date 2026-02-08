using UnityEngine;

namespace Character
{
    public class CharacterRotation
    {
        private readonly float rotationSpeed;

        public CharacterRotation(float rotationSpeed) => this.rotationSpeed = rotationSpeed;

        public void RotateTowards(Transform transform, Vector3 direction)
        {
            if (direction.sqrMagnitude < 0.001f)
                return;

            Quaternion targetRot = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotationSpeed * Time.deltaTime);
        }
    }
}