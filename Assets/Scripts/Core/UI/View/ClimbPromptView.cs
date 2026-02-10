using UnityEngine;
using Core.UI;
using UI.Data;

namespace UI.Views
{
    public class ClimbPromptView : MonoBehaviour, IUIElement
    {
        [SerializeField] private GameObject root;

        public void Show(IUIData data)
        {
            if (data is not ClimbPromptData climbData)
                return;

            root.SetActive(true);

            transform.position = climbData.WorldPosition;
            transform.forward = Camera.main.transform.forward;
        }

        public void Hide()
        {
            root.SetActive(false);
        }
    }
}
