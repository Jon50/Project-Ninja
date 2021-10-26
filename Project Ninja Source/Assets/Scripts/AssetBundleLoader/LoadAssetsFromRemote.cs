using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceLocations;

[RequireComponent(typeof(CanvasGroup))]
public class LoadAssetsFromRemote : MonoBehaviour
{
    [SerializeField] private string _label = "tgm_remote";
    [SerializeField] private Slider _slider;

    private CanvasGroup _canvasGroup;

    public static LoadAssetsFromRemote Instance;
    public static List<IResourceLocation> Scenes;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Scenes = new List<IResourceLocation>();

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
        while (_canvasGroup?.alpha != 0f)
        {
            _canvasGroup.alpha -= Time.deltaTime;
            yield return 0f;
        }

        if (_canvasGroup.alpha == 0f)
            isDone = true;

        yield return StartCoroutine(LoadAssets(_label));
        yield return new WaitUntil(() => isDone);

        while (_canvasGroup?.alpha != 1f)
        {
            _canvasGroup.alpha += Time.deltaTime;
            yield return 0f;
        }

        if (SceneManager.GetActiveScene().name != nameof(Level.MainMenu))
            SceneManager.LoadSceneAsync(nameof(Level.MainMenu));
    }

    private IEnumerator LoadAssets(string label)
    {
        var isDone = false;
        var locations = Addressables.LoadResourceLocationsAsync(label);
        locations.Completed += (operation) =>
        {
            int index = -1;
            foreach (var location in operation.Result)
            {
                index++;
                if (Scenes.Contains(location))
                {
                    Scenes[index] = location;
                    continue;
                }

                Scenes.Add(location);
            }

            Scenes.Sort((a, b) => a.InternalId.CompareTo(b.InternalId));
            isDone = true;
        };

        yield return new WaitUntil(() => isDone);

        isDone = false;

        var download = Addressables.DownloadDependenciesAsync(label);
        download.Completed += (operation) =>
        {
            isDone = true;

            if (_slider != null)
                _slider.value = _slider.maxValue;
        };

        while (!isDone)
        {
            if (_slider != null)
                _slider.value = download.PercentComplete;
            yield return 0f;
        }

        yield return new WaitUntil(() => isDone);
    }
}
