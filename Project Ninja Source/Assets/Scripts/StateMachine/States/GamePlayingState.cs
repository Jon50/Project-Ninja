using UnityEngine;

using DefaultCompany.ProjectNinja.Managers;

namespace DefaultCompany.ProjectNinja.StateMachine
{
    public class GamePlayingState : GameState<GameManager>
    {
        public static GamePlayingState Instance => StateSingleton<GamePlayingState>.MakeInstatnce;


        public override void EnterState(GameManager owner)
        {
            Time.timeScale = 1;
            owner.EnterPlayMode();
        }

        public override void ExitState(GameManager owner)
        {
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