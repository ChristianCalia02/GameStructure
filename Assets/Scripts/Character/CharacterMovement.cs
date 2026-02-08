using UnityEngine;

namespace Character
{
    public class CharacterMovement
    {
        private Vector3 horizontalMove;
        private float currentSpeed;
        private bool hasMoveInput;

        public bool IsMoving => hasMoveInput;
        public Vector3 HorizontalMove => horizontalMove * currentSpeed;

        public void Move(Vector3 direction, float magnitude)
        {
            if (direction.sqrMagnitude < 0.001f)
                return;

            hasMoveInput = true;
            horizontalMove += direction.normalized * magnitude;
        }

        public void SetSpeed(float speed) => currentSpeed = speed;

        public void Reset()
        {
            horizontalMove = Vector3.zero;
            hasMoveInput = false;
        }
    }
}