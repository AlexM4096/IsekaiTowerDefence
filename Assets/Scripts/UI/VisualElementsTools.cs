using UnityEngine.UIElements;

namespace UI
{
    public static class VisualElementsTools
    {
        public static void SetActive(this VisualElement visualElement, bool state)
        {
            visualElement.style.display = state ? DisplayStyle.Flex : DisplayStyle.None;
        }

        public static void Enable(this VisualElement visualElement) => visualElement.SetActive(true);
        public static void Disable(this VisualElement visualElement) => visualElement.SetActive(false);
    }
}