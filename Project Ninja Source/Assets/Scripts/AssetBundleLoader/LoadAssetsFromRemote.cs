using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceLocations;
using System.Linq;
using System;
using System.Net;
using System.IO.Compression;
using System.IO;

public enum AddressablesProfiles { DEFAULT, AWS, CLOUD, CUSTOM }
public enum ResourceLabels { aws_start_scenes, aws_game_scenes, aws_content }

[RequireComponent(typeof(CanvasGroup))]
public class LoadAssetsFromRemote : MonoBehaviour
{
    //[SerializeField] private ResourceLabels _labels;
    [SerializeField] private Slider _slider;
    [SerializeField] private AddressablesProfiles _profile = AddressablesProfiles.AWS;
    [SerializeField] private string _customCatalogLocation;
    [SerializeField] private string _customResourceLocation;

    private CanvasGroup _canvasGroup;
    private string _oldHash;
    private string _currentHash;

    public static LoadAssetsFromRemote Instance;
    //public static List<IResourceLocation> Scenes;
    public static List<List<IResourceLocation>> resources;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            //Scenes = new List<IResourceLocation>();
            resources = new List<List<IResourceLocation>>();

            _canvasGroup = GetComponent<CanvasGroup>();
            _canvasGroup.alpha = 1;
        }
        else
            Destroy(gameObject);

        Time.timeScale = 1;
    }

    public IEnumerator Start()
    {
        if(_profile == AddressablesProfiles.CUSTOM)
        {
            var webClient = new WebClient();
            webClient.DownloadFile(new Uri(_customCatalogLocation), Application.persistentDataPath + @"\catalog.zip");
            webClient.DownloadFileCompleted += ( sender, completed ) => { };

            if(Directory.Exists(Application.persistentDataPath + @"\catalog"))
            {
                _oldHash = File.ReadAllText(Directory.GetFiles(Application.persistentDataPath + @"\catalog", "*.hash")[0]);
                Debug.Log("Old Hash: " + _oldHash);
                Directory.Delete(Application.persistentDataPath + @"\catalog", true);
            }

            ZipFile.ExtractToDirectory(Application.persistentDataPath + @"\catalog.zip", Application.persistentDataPath);

            _currentHash = File.ReadAllText(Directory.GetFiles(Application.persistentDataPath + @"\catalog", "*.hash")[0]);

            Debug.Log("Current Hash: " + _currentHash);
            Debug.Log(_oldHash == _currentHash);

            if(string.IsNullOrEmpty(_oldHash) == true ? false : _oldHash != _currentHash)
            {
                Debug.Log("Hashed");
                Directory.Delete(Application.persistentDataPath + @"\android", true);
            }

            Debug.Log(Directory.Exists(Application.persistentDataPath + @"\" + Application.platform));
            Debug.Log(Application.persistentDataPath + @"\" + Application.platform);

            if(Directory.Exists(Application.persistentDataPath + @"\android") == false)
            {
                Debug.Log("Resources Downloaded");
                webClient.DownloadFile(new Uri(_customResourceLocation), Application.persistentDataPath + @"\resources.zip");
                webClient.DownloadFileCompleted += ( sender, completed ) => { };
                ZipFile.ExtractToDirectory(Application.persistentDataPath + @"\resources.zip", Application.persistentDataPath);
            }
        }

        var isDone = false;
        while(_canvasGroup?.alpha != 0f)
        {
            _canvasGroup.alpha -= Time.deltaTime;
            yield return null;
        }

        if(_canvasGroup.alpha == 0f)
            isDone = true;

        var labels = Enum.GetNames(typeof(ResourceLabels));

        for(int i = 0; i < labels.Length; i++)
            yield return StartCoroutine(LoadAssets(labels[i], labelIndex: i));

        yield return new WaitUntil(() => isDone);

        if(resources.Any(( x ) => x.Count <= 0)) // Could interrupt in future. By then, consider removing.
        {
            Debug.LogError("Resources not loaded");
            yield break;
        }

        while(_canvasGroup?.alpha != 1f)
        {
            _canvasGroup.alpha += Time.deltaTime;
            yield return null;
        }

        if(SceneManager.GetActiveScene().name != nameof(Level.MainMenu))
            Addressables.LoadSceneAsync(resources[(int)ResourceLabels.aws_start_scenes].First(( x ) => x.InternalId.Contains(nameof(Level.MainMenu)))/*.Where(( x ) => x.InternalId.Contains(nameof(Level.MainMenu)))*/);
    }

    private IEnumerator LoadAssets( string label, int labelIndex )
    {
        var isDone = false;
        var locations = Addressables.LoadResourceLocationsAsync(label);
        locations.Completed += ( operation ) =>
        {
            resources.Add(new List<IResourceLocation>());

            var resourcesList = resources[labelIndex];

            int index = -1;
            foreach(var location in operation.Result)
            {
                index++;
                if(resourcesList.Contains(location) /*Scenes.Contains(location)*/)
                {
                    //Scenes[index] = location;
                    resourcesList[index] = location;
                    continue;
                }

                //Scenes.Add(location);
                resourcesList.Add(location);
            }

            //Scenes.Sort(( a, b ) => a.InternalId.CompareTo(b.InternalId));
            resourcesList.Sort(( a, b ) => a.InternalId.CompareTo(b.InternalId));
            isDone = true;
        };

        yield return new WaitUntil(() => isDone);

        isDone = false;

        var download = Addressables.DownloadDependenciesAsync(label);
        download.Completed += ( operation ) =>
        {
            isDone = true;

            if(_slider != null)
                _slider.value = _slider.maxValue;
        };

        while(!isDone)
        {
            if(_slider != null)
                _slider.value = (download.PercentComplete + locations.PercentComplete);
            yield return null;
        }

        yield return new WaitUntil(() => isDone);
    }
}
