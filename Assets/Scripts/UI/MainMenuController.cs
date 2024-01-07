using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{
    [RequireComponent(typeof(UIDocument))]
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField] private MenuMediator menuMediator;
        
        private VisualElement _root;

        private Button _newGame;
        private Button _continueGame;
        private Button _selectLevel;
        private Button _quitGame;

        private Button _settingsMenu;
        private Button _soundMenuButton;
    
        private void Awake()
        {
            UIDocument uiDocument = GetComponent<UIDocument>();
            _root = uiDocument.rootVisualElement;

            GetElements();
        }

        private void GetElements()
        {
            _newGame = _root.Q<Button>("NewGame");
            _newGame.clicked += OnNewGame;
            
            _continueGame = _root.Q<Button>("ContinueGame");
            _continueGame.clicked += OnContinueGame;
            
            _selectLevel = _root.Q<Button>("SelectLevel");
            _selectLevel.clicked += OnSelectLevel;
            
            _quitGame = _root.Q<Button>("QuitGame");
            _quitGame.clicked += OnQuitGame;

            _settingsMenu = _root.Q<Button>("SettingsMenu");
            _settingsMenu.clicked += OnSettingsMenu;

            _soundMenuButton = _root.Q<Button>("SoundMenu");
            _soundMenuButton.clicked += OnSoundMenuButton;
        }
        
        private void OnNewGame()
        {
            print("NewGame clicked");
        }
        
        private void OnContinueGame()
        {
            print("ContinueGame clicked");
        }
        
        private void OnSelectLevel()
        {
            print("SelectLevel clicked");
        }
        
        private void OnQuitGame()
        {
            print("QuitGame clicked");
            Application.Quit();
        }
        
        private void OnSettingsMenu()
        {
            print("SettingsMenu clicked");
        }
        
        private void OnSoundMenuButton()
        {
            menuMediator.OpenSoundMenu();
            print("SoundMenu clicked");
        }

    }
}
