using UnityEngine;

namespace Character
{
    [RequireComponent(typeof(Animator))]
    public class CharacterAnimator : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private CharacterRoot root;

        private bool wasGrounded;

        void Awake()
        {
            animator = animator ?? GetComponent<Animator>();
            root = root ?? GetComponent<CharacterRoot>();
        }

        void LateUpdate()
        {
            if (root == null) return;

            animator.SetBool("IsGrounded", root.IsGrounded);
            animator.SetBool("IsMoving", root.IsMoving);
            animator.SetBool("IsRunning", root.IsSprinting);
            animator.SetBool("IsHanging", root.Climb != null && root.Climb.IsHanging);

            if (wasGrounded && !root.IsGrounded)
            {
                animator.SetTrigger("Jump");
            }

            wasGrounded = root.IsGrounded;
        }
    }
}