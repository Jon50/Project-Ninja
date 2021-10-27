using System.Collections.Generic;
using UnityEngine;

using DefaultCompany.ProjectNinja.Locator;
using DefaultCompany.ProjectNinja.Managers;

namespace DefaultCompany.ProjectNinja.StateMachine
{
    public class GamePauseState : GameState<GameManager>
    {
        public bool IsGamePaused { get; private set; }
        public static GamePauseState Instance => StateSingleton<GamePauseState>.MakeInstatnce;
        private List<StatePreparator> _statePreparators;
        private AudioSource[] _audioSources;

        public override void EnterState(GameManager owner)
        {
            _statePreparators = ServiceLocator.ResolveList<StatePreparator>();

            if (_statePreparators.Count != 0)
                foreach (var sp in _statePreparators)
                    sp.DisableComponents();

            if (_audioSources == null)
                _audioSources = GameObject.FindObjectsOfType<AudioSource>();

            for (int i = 0; i < _audioSources.Length; i++)
                _audioSources[i].enabled = false;

            Time.timeScale = 0;
            IsGamePaused = true;
        }

        public override void ExitState(GameManager owner)
        {
            foreach (var sp in _statePreparators)
                sp.EnableComponents();

            for (int i = 0; i < _audioSources.Length; i++)
                _audioSources[i].enabled = true;

            // _statePreparators.Clear();

            IsGamePaused = false;
        }

        public override void UpdateState(GameManager owner)
        {
        }

        public override void ClearInstance()
        {
            StateSingleton<GamePauseState>.MakeInstatnce = null;
        }
    }
}