using UnityEngine;

namespace DefaultCompany.ProjectNinja.Managers
{
    public class MainMenuManager : Menu
    {
        private void Start()
        {
            Application.targetFrameRate = 300;
            QualitySettings.vSyncCount = 0;

            if(LoadAssetsFromRemote.resources.Count == 0)
                Instantiate(new GameObject()).AddComponent(typeof(LoadAssetsFromRemote)).GetComponent<LoadAssetsFromRemote>().Start();
        }
    }
}