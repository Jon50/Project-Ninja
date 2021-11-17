using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
using static KadoNem.ProjectNinja.Static.ConstantValues;
using static KadoNem.ProjectNinja.Save.SavingSystem;
using System;
using System.Linq;

public enum Level { MainMenu }

namespace KadoNem.ProjectNinja.SceneLoadManagement
{
    [CreateAssetMenu(menuName = "SceneLoader")]
    public class SceneLoader : ScriptableObject
    {
        public int CurrentSceneIndex
        {
            get => LoadValue(CURRENT_SCENE_INDEX, 0);
            set => SaveValue(CURRENT_SCENE_INDEX, value);
        }


        public void MainMenu() => FadeAndLoad(toMainMenu: true);


        public void StartGame( int level )
        {
            CurrentSceneIndex = level;
            FadeAndLoad();
        }


        public void NextLevel()
        {
            CurrentSceneIndex += 1;
            FadeAndLoad();
        }


        public void RestartGame() => FadeAndLoad();


        private void FadeAndLoad( bool toMainMenu = false )
        {
            if(Fader.Instance == null)
                Load();
            else
                Fader.Instance.FadeIn(OnCompleteCall: Load);

            void Load()
            {
                if(toMainMenu)
                {
                    //var loadingScene = SceneManager.LoadSceneAsync(nameof(Level.MainMenu));
                    var mainMenu = LoadAssetsFromRemote.resources[(int)ResourceLabels.aws_start_scenes].First(( x ) => x.InternalId.Contains(nameof(Level.MainMenu)));
                    var loadingScene = Addressables.LoadSceneAsync(mainMenu);
                    loadingScene.Completed += ( op ) =>
                    {
                        Fader.Instance?.FadeOut();
                    };
                    return;
                }

                var location = LoadAssetsFromRemote.resources[(int)ResourceLabels.aws_game_scenes][CurrentSceneIndex];
                var loading = Addressables.LoadSceneAsync(location);
                loading.Completed += ( op ) =>
                {
                    Fader.Instance?.FadeOut();
                };
            }
        }


        public void QuitGame() => Application.Quit();
    }
}
