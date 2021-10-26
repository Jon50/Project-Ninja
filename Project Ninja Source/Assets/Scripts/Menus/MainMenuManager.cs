using System.Threading.Tasks;
using UnityEngine;

namespace TGM.FutureRacingGP.Managers
{
    public class MainMenuManager : Menu
    {
        private void Start()
        {
            Application.targetFrameRate = 300;
            QualitySettings.vSyncCount = 0;

            if (LoadAssetsFromRemote.Scenes.Count == 0)
                Instantiate(new GameObject()).AddComponent(typeof(LoadAssetsFromRemote)).GetComponent<LoadAssetsFromRemote>().Start();
        }
    }
}