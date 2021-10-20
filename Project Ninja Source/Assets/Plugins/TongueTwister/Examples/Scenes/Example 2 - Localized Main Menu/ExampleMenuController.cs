using UnityEngine;

namespace TongueTwister.Examples.Scenes
{
    public class ExampleMenuController : MonoBehaviour
    {
        [SerializeField] private GameObject _mainMenu, _settingsMenu, _aboutMenu, _playMenu;

        public void OnPlayButtonClick()
        {
            _mainMenu.SetActive(false);
            _playMenu.SetActive(true);
        }

        public void OnSettingsButtonClick()
        {
            _mainMenu.SetActive(false);
            _settingsMenu.SetActive(true);
        }

        public void OnAboutButtonClick()
        {
            _mainMenu.SetActive(false);
            _aboutMenu.SetActive(true);
        }

        public void OnQuitButtonClick()
        {
            Application.Quit();
        }

        public void OnBackButtonClick()
        {
            _settingsMenu.SetActive(false);
            _aboutMenu.SetActive(false);
            _playMenu.SetActive(false);
            _mainMenu.SetActive(true);
        }
    }
}