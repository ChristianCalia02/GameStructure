using Core.Events;
using UnityEngine;

namespace UI
{
    public class ClimbPromptUI : MonoBehaviour
    {
        [SerializeField] private GameObject prompt;
        [SerializeField] private Vector3 offset = Vector3.zero;

        void OnEnable()
        {
            GameEvents.OnClimbAvailable += Toggle;
        }

        void OnDisable()
        {
            GameEvents.OnClimbAvailable -= Toggle;
        }

        private void Toggle(bool show, Vector3 worldPos)
        {
            prompt.SetActive(show);

            if (show)
            {
                transform.position = worldPos + offset;
                transform.forward = Camera.main.transform.forward;
            }
        }
    }
}