using UnityEngine;
using Core.Events;
using UI.Data;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private UIFactory factory;

        void OnEnable()
        {
            UIEvents.OnClimbAvailable += HandleClimb;
        }

        void OnDisable()
        {
            UIEvents.OnClimbAvailable -= HandleClimb;
        }

        private void HandleClimb(bool show, Vector3 worldPos)
        {
            Debug.Log($"HandleClimb: {show} at {worldPos}");
            if (show)
                factory.Show(new ClimbPromptData(worldPos));
            else
                factory.Hide("CLIMB_PROMPT");
        }
    }
}