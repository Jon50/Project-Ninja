using UnityEngine;

using DefaultCompany.ProjectNinja.Managers;
using DefaultCompany.ProjectNinja.Locator;
using System.Collections.Generic;

namespace DefaultCompany.ProjectNinja.StateMachine
{
    public class NextLevelState : GameState<GameManager>
    {
        public static NextLevelState Instance = StateSingleton<NextLevelState>.MakeInstatnce;
        private List<StatePreparator> _statePreparators;

        public override void EnterState(GameManager owner)
        {
            _statePreparators = ServiceLocator.ResolveList<StatePreparator>();
            if (_statePreparators.Count != 0)
                foreach (var sp in _statePreparators)
                    sp.DisableComponents();
        }


        public override void ExitState(GameManager owner)
        {
            foreach (var sp in _statePreparators)
                sp.EnableComponents();

            // _statePreparators.Clear();
        }


        public override void UpdateState(GameManager owner)
        {
            owner.ReportStateWhenDone();
            owner.NextLevel();
        }

        public override void ClearInstance()
        {
            StateSingleton<GamePauseState>.MakeInstatnce = null;
        }
    }
}