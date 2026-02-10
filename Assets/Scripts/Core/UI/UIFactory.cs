using System.Collections.Generic;
using UnityEngine;
using Core.UI;
using UI.Views;

namespace UI
{
    public class UIFactory : MonoBehaviour
    {
        [SerializeField] private ClimbPromptView climbPromptPrefab;

        private Dictionary<string, IUIElement> activeElements = new();

        public void Show(IUIData data)
        {
            if (activeElements.TryGetValue(data.Id, out var existing))
            {
                existing.Show(data);
                return;
            }

            IUIElement element = CreateElement(data.Id);
            if (element == null)
                return;

            activeElements[data.Id] = element;
            element.Show(data);
        }

        public void Hide(string id)
        {
            if (!activeElements.TryGetValue(id, out var element))
                return;

            element.Hide();
        }

        private IUIElement CreateElement(string id)
        {
            switch (id)
            {
                case "CLIMB_PROMPT":
                    return Instantiate(climbPromptPrefab, transform);

                default:
                    Debug.LogWarning($"No UI registered for id: {id}");
                    return null;
            }
        }
    }
}