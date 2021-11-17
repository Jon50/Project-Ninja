using System;
using System.Collections.Generic;

namespace KadoNem.ProjectNinja.StateMachine
{
    public class StateMachine<T>
    {
        public GameState<T> CurrentState { get; private set; }
        private T _owner;

        private List<GameState<T>> _states = new List<GameState<T>>();

        public StateMachine(T owner)
        {
            _owner = owner;
            CurrentState = null;
        }

        public void ChangeState(GameState<T> newState)
        {
            if (newState.IsNull())
                return;

            AddStateToList(newState);

            CurrentState?.ExitState(_owner);
            CurrentState = newState;
            CurrentState.EnterState(_owner);
        }

        private void AddStateToList(GameState<T> newState)
        {
            if (_states.Contains(newState))
                return;

            _states.Add(newState);
        }

        public void ClearStates()
        {
            for (int i = 0; i < _states.Count; i++)
            {
                _states[i].ClearInstance();
            }
        }

        public void ClearCurrentState()
        {
            CurrentState.ExitState(_owner);
        }

        public void StateMachineUpdate() => CurrentState?.UpdateState(_owner);
    }
}