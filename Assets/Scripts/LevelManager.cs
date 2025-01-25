using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using UnityEngine.UI;
using DG.Tweening;
using System;
using UnityEngine.EventSystems;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _loaderCanvas;

    [SerializeField]
    private RawImage _background;

    private bool _isLoading;

    public void LoadScene(SceneName sceneName, bool transition = true)
    {
        if (_isLoading) return;

        StartCoroutine(InternalSceneLoad(sceneName, transition));
        _isLoading = true;
    }

    public Task EnableLoadingCanvas(float duration)
    {
        _loaderCanvas.SetActive(true);
        return _background.DOColor(Color.black, duration).SetEase(Ease.InQuad).AsyncWaitForCompletion();
    }

    public void DisableLoadingCanvas()
    {
        _background.DOColor(Color.clear, 0.3f);
    }

    private IEnumerator InternalSceneLoad(SceneName sceneName, bool transition)
    {
        // Load our destination scene
        var operation = SceneManager.LoadSceneAsync(sceneName.ToString());
        operation.allowSceneActivation = false;

        EnableLoadingCanvas(transition ? 0.3f : 0);
        if (transition) yield return new WaitForSeconds(0.3f);

        operation.allowSceneActivation = true;

        // If we are still loading postpone the routine,
        while (!operation.isDone)
        {
            yield return null;
        }

        DisableLoadingCanvas();

        _isLoading = false;
        // Start unloading the loading screen
        //loadingScreenStartUnloading?.Invoke();

        yield return null;
    }
}

public enum SceneName
{
    //If you add a scene put it at the bottom of the enum
    MainMenu,
    Game
}
