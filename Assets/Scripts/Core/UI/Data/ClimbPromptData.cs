using UnityEngine;
using Core.UI;

namespace UI.Data
{
    public class ClimbPromptData : IUIData
    {
        public string Id => "CLIMB_PROMPT";

        public Vector3 WorldPosition { get; }

        public ClimbPromptData(Vector3 worldPosition)
        {
            WorldPosition = worldPosition;
        }
    }
}
