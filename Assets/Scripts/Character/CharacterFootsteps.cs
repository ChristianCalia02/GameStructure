using Core.Events;
using UnityEngine;

namespace Character
{
    [RequireComponent(typeof(CharacterRoot))]
    public class CharacterFootsteps : MonoBehaviour
    {
        [SerializeField] private float stepInterval = 0.5f;
        private float stepTimer;
        private CharacterRoot root;

        void Awake()
        {
            root = GetComponent<CharacterRoot>();
        }

        void Update()
        {
            if (!root.IsMoving) return;

            stepTimer += Time.deltaTime;

            if (stepTimer >= stepInterval)
            {
                stepTimer = 0f;
                AudioEvents.OnFootstep?.Invoke(transform.position);
            }
        }
    }
}