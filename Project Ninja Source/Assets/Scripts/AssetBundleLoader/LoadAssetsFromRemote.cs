using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceLocations;
using System.Linq;
using System;

public enum ResourceLabels { aws_start_scenes, aws_game_scenes, aws_content }

[RequireComponent(typeof(CanvasGroup))]
public class LoadAssetsFromRemote : MonoBehaviour
{
    //[SerializeField] private ResourceLabels _labels;
    [SerializeField] private Slider _slider;

    private CanvasGroup _canvasGroup;

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
    }

    public IEnumerator Start()
    {
        Time.timeScale = 1;

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
