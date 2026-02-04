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

        private void OnAnimatorIK(int layerIndex)
        {
            if (!climb.IsHanging)
            {
                ResetIK();
                return;
            }

            ApplyHandIK(AvatarIKGoal.LeftHand, leftHandTarget);
            ApplyHandIK(AvatarIKGoal.RightHand, rightHandTarget);
        }

        private void ApplyHandIK(AvatarIKGoal goal, Transform target)
        {
            animator.SetIKPositionWeight(goal, 1f);
            animator.SetIKRotationWeight(goal, 1f);
            animator.SetIKPosition(goal, target.position);
            animator.SetIKRotation(goal, target.rotation);
        }

        private void ResetIK()
        {
            animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0f);
            animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0f);
            animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0f);
            animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0f);
        }
    }
}
