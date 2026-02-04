using UnityEngine;

namespace Climb
{
    public class CharacterIK : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private CharacterClimb climb;

        [Header("IK Targets")]
        [SerializeField] private Transform leftHandTarget;
        [SerializeField] private Transform rightHandTarget;
        [SerializeField] private Transform leftFootTarget;
        [SerializeField] private Transform rightFootTarget;
        [SerializeField] private Transform leftKneeHint;
        [SerializeField] private Transform rightKneeHint;
        private void OnAnimatorIK(int layerIndex)
        {
            if (!climb.IsHanging)
            {
                ResetIK();
                return;
            }

            // Hands
            ApplyIK(AvatarIKGoal.LeftHand, leftHandTarget, 1f);
            ApplyIK(AvatarIKGoal.RightHand, rightHandTarget, 1f);

            // Feet
            ApplyIK(AvatarIKGoal.LeftFoot, leftFootTarget,1f);
            ApplyIK(AvatarIKGoal.RightFoot, rightFootTarget,1f);

            // Knees
            ApplyKneeHint(AvatarIKHint.LeftKnee, leftKneeHint);
            ApplyKneeHint(AvatarIKHint.RightKnee, rightKneeHint);

            Debug.DrawRay(leftKneeHint.position, Vector3.up * 0.1f, Color.red);
            Debug.DrawRay(rightKneeHint.position, Vector3.up * 0.1f, Color.blue);
        }

        private void ApplyIK(AvatarIKGoal goal, Transform target, float weight)
        {
            animator.SetIKPositionWeight(goal, 1f);
            animator.SetIKRotationWeight(goal, 1f);
            animator.SetIKPosition(goal, target.position);
            animator.SetIKRotation(goal, target.rotation);
        }

        private void ApplyKneeHint(AvatarIKHint hint, Transform target)
        {
            if (target == null) return;

            animator.SetIKHintPositionWeight(hint, 1f);
            animator.SetIKHintPosition(hint, target.position);
        }

        private void ResetIK()
        {
            foreach (AvatarIKGoal goal in System.Enum.GetValues(typeof(AvatarIKGoal)))
            {
                animator.SetIKPositionWeight(goal, 0f);
                animator.SetIKRotationWeight(goal, 0f);
            }

            animator.SetIKHintPositionWeight(AvatarIKHint.LeftKnee, 0f);
            animator.SetIKHintPositionWeight(AvatarIKHint.RightKnee, 0f);
        }
    }
}
