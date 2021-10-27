using UnityEngine;

using DefaultCompany.ProjectNinja.StateMachine;
using DefaultCompany.ProjectNinja.Locator;
using DefaultCompany.ProjectNinja.SceneLoadManagement;

namespace DefaultCompany.ProjectNinja.Managers
{
    public class GameManager : ServiceRegister<GameManager>
    {
        [SerializeField] private SceneLoader _sceneLoader;

        private StateMachine<GameManager> _stateMachine;
        private bool _blockStateMachine;


        public override void Awake()
        {
            base.Awake();
            Initialization();
        }

        private void Initialization()
        {
            _stateMachine = new StateMachine<GameManager>(owner: this);
        }

        private void Start()
        {
            SetNewState(PrepareGameState.Instance);
            _blockStateMachine = true;
        }

        private void Update() => _stateMachine.StateMachineUpdate();

        public override void OnDisable()
        {
            base.OnDisable();
            _stateMachine.ClearStates();
        }


        internal void EnterPlayMode()
        {
        }


        public void MainMenu()
        {
            _sceneLoader?.MainMenu();
        }


        public void RestartGame()
        {
            _sceneLoader?.RestartGame();
        }

        public void NextLevel()
        {
            _sceneLoader?.NextLevel();
        }

        internal void SetNewState( GameState<GameManager> newState )
        {
            if(!_blockStateMachine)
                _stateMachine.ChangeState(newState);
        }

        public void ReportStateWhenDone() => _blockStateMachine = false;
    }
}
