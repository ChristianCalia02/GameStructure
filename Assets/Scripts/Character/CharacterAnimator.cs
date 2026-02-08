using Core.Events;
using UnityEngine;

namespace Character
{
    [RequireComponent(typeof(Animator))]
    public class CharacterAnimator : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        private CharacterRoot root;

        void Awake()
        {
            root = GetComponent<CharacterRoot>();
            animator = animator ?? GetComponent<Animator>();
        }

        void LateUpdate()
        {
            if (root == null) return;

            animator.SetBool("IsGrounded", root.IsGrounded);
            animator.SetBool("IsMoving", root.IsMoving);
            animator.SetBool("IsRunning", root.IsSprinting);
            animator.SetBool("IsHanging", root.Climb != null && root.Climb.IsHanging);
        }
    }
}