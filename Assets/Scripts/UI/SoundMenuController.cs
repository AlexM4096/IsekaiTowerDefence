using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{
    [RequireComponent(typeof(UIDocument))]    
    public class SoundMenuController : MonoBehaviour
    {
        [SerializeField] private MenuMediator menuMediator;
        
        private VisualElement _root;

        private Button _closeButton;
        
        private void Awake()                                   
        {                                                      
            UIDocument uiDocument = GetComponent<UIDocument>();
            _root = uiDocument.rootVisualElement;

            _closeButton = _root.Q<Button>("CloseButton");
            _closeButton.clicked += OnCloseButton;
            
            _root.Disable();
        }

        public void OnCloseAction()
        {
            _root.Disable();
        }

        public void OnOpenAction()
        {
            _root.Enable();
        }

        private void OnCloseButton()
        {
            print("CloseButton clicked");
            menuMediator.CloseSoundMenu();
        }
    }
}