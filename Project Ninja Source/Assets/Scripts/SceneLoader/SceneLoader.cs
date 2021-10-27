using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
using static DefaultCompany.ProjectNinja.Static.ConstantValues;
using static DefaultCompany.ProjectNinja.Save.SavingSystem;

public enum Level { MainMenu }

namespace DefaultCompany.ProjectNinja.SceneLoadManagement
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
            Fader.Instance.FadeIn(OnCompleteCall: Load);

            void Load()
            {
                if(toMainMenu)
                {
                    var loadingScene = SceneManager.LoadSceneAsync(nameof(Level.MainMenu));
                    loadingScene.completed += ( op ) =>
                    {
                        Fader.Instance.FadeOut();
                    };
                    return;
                }

                // TODO: Remove hardcoded value
                var location = LoadAssetsFromRemote.resources[0][CurrentSceneIndex];
                var loading = Addressables.LoadSceneAsync(location);
                loading.Completed += ( op ) =>
                {
                    Fader.Instance.FadeOut();
                };
            }
        }


        public void QuitGame() => Application.Quit();
    }
}
