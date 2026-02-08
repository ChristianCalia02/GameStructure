using UnityEngine;

namespace Character
{
    public class CharacterGravity
    {
        private float verticalVelocity;
        private readonly float jumpHeight;
        private readonly float gravityMultiplier;

        public float VerticalVelocity => verticalVelocity;

        public CharacterGravity(float jumpHeight, float gravityMultiplier)
        {
            this.jumpHeight = jumpHeight;
            this.gravityMultiplier = gravityMultiplier;
        }

        public void UpdateGravity(bool isGrounded)
        {
            if (isGrounded && verticalVelocity < 0f)
                verticalVelocity = -4f;

            verticalVelocity += Physics.gravity.y * gravityMultiplier * Time.deltaTime;
        }

        public void Jump()
        {
            verticalVelocity = Mathf.Sqrt(2f * jumpHeight * -Physics.gravity.y);
        }

        public void Reset()
        {
            verticalVelocity = 0f;
        }
    }
}