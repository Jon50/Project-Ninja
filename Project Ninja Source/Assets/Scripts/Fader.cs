using System;
using System.Collections;
using UnityEngine;

public class Fader : MonoBehaviour
{
    public CanvasGroup canvasGroup;

    public static Fader Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;

        if (transform.parent)
            transform.SetParent(null);

        DontDestroyOnLoad(gameObject);
    }


    public void FadeIn(Action OnCompleteCall)
    {
        StartCoroutine(FadeInRoutine(OnCompleteCall));
    }

    public void FadeOut()
    {
        StartCoroutine(FadeOutRoutine());
    }

    private IEnumerator FadeInRoutine(Action OnComplete)
    {
        Time.timeScale = 1f; //$$
        while (canvasGroup.alpha < 1f)
        {
            canvasGroup.alpha += Time.deltaTime;
            yield return 0f;
        }

        OnComplete?.Invoke();
    }

    private IEnumerator FadeOutRoutine()
    {
        Time.timeScale = 1f; //$$
        while (canvasGroup.alpha > 0f)
        {
            canvasGroup.alpha -= Time.deltaTime;
            yield return 0f;
        }
    }
}
