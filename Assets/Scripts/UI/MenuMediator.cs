using UnityEngine;

namespace UI
{
    public class MenuMediator : MonoBehaviour
    {
        [SerializeField] private MainMenuController mainMenuController;
        [SerializeField] private SoundMenuController soundMenuController;

        public void OpenSoundMenu() => soundMenuController.OnOpenAction();
        public void CloseSoundMenu() => soundMenuController.OnCloseAction();
    }
}