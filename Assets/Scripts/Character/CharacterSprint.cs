using UnityEngine;

namespace Character
{
    public class CharacterSprint
    {
        private readonly float walkSpeed;
        private readonly float runSpeed;
        private readonly float acceleration;

        private float currentSpeed;
        private bool isSprinting;

        public float CurrentSpeed => currentSpeed;
        public bool IsSprinting => isSprinting;

        public CharacterSprint(float walkSpeed, float runSpeed, float acceleration)
        {
            this.walkSpeed = walkSpeed;
            this.runSpeed = runSpeed;
            this.acceleration = acceleration;
            currentSpeed = walkSpeed;
        }

        public void SetSprint(bool sprint) => isSprinting = sprint;

        public void Update()
        {
            float targetSpeed = isSprinting ? runSpeed : walkSpeed;
            currentSpeed = Mathf.MoveTowards(currentSpeed, targetSpeed, acceleration * Time.deltaTime);
        }
    }
}