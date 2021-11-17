using KadoNem.ProjectNinja.StateMachine;

namespace KadoNem.ProjectNinja.Managers
{
    public class PauseMenuManager : Menu
    {
        private GameManager _gameManager;

        private void Start() => _gameManager = transform.parent.GetComponentInChildren<GameManager>();

        public void ToggleInitialMenu()
        {
            if (!GamePauseState.Instance.IsGamePaused)
            {
                initialMenu.SetActive(!initialMenu.activeSelf);
                _gameManager.SetNewState(GamePauseState.Instance);
            }
            else
            {
                initialMenu.SetActive(!initialMenu.activeSelf);
                _gameManager.SetNewState(GamePlayingState.Instance);
            }
        }
    }
}