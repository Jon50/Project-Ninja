using UnityEngine;

using TGM.FutureRacingGP.Managers;

namespace TGM.FutureRacingGP.StateMachine
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