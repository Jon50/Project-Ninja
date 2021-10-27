using System.Collections.Generic;
using UnityEngine;

using DefaultCompany.ProjectNinja.Locator;
using DefaultCompany.ProjectNinja.Managers;

namespace DefaultCompany.ProjectNinja.StateMachine
{
    public class PrepareGameState : GameState<GameManager>
    {
        public static PrepareGameState Instance = StateSingleton<PrepareGameState>.MakeInstatnce;

        private List<StatePreparator> _statePreparators = new List<StatePreparator>();


        public override void EnterState( GameManager owner )
        {
            _statePreparators = ServiceLocator.ResolveList<StatePreparator>();

            if(_statePreparators.Count == 0)
                return;

            foreach(var sp in _statePreparators)
                sp.DisableComponents();

            Prepare(owner);
        }


        private void Prepare( GameManager owner )
        {
            Time.timeScale = 1f;
            owner.ReportStateWhenDone();
            owner.SetNewState(GamePlayingState.Instance);
        }


        public override void ExitState( GameManager owner )
        {
            foreach(var sp in _statePreparators)
                sp.EnableComponents();
        }


        public override void UpdateState( GameManager owner )
        {
        }

        public override void ClearInstance()
        {
            StateSingleton<GamePauseState>.MakeInstatnce = null;
        }
    }
}