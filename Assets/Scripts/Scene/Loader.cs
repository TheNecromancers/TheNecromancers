using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Loader;

public static class Loader
{
    //Dumb 
    private class LoadingMonoBehaviour : MonoBehaviour { }
    public enum Scene
    {
        SandBox,
        Loading,
        TestScene
    }
    private static Action onLoaderCallback;
    private static AsyncOperation loadingAsyncOperation;
    public static void Load(Scene scene)
    {
        onLoaderCallback = () =>
        {
            GameObject loadingGameObject = new GameObject("Loading Game Object");
            loadingGameObject.AddComponent<LoadingMonoBehaviour>().StartCoroutine(LoadSceneAsync(scene));
            LoadSceneAsync(scene);
        };

        SceneManager.LoadScene(Scene.Loading.ToString());
    }

    private static IEnumerator LoadSceneAsync(Scene scene)
    {
        yield return null;

        loadingAsyncOperation = SceneManager.LoadSceneAsync(scene.ToString());

        while (!loadingAsyncOperation.isDone) 
        { 
            yield return null; 
        }
    }

    public static float GetLoadingProgress()
    {
        if(loadingAsyncOperation != null) 
        { 
            return loadingAsyncOperation.progress;
        } 
        else
        {
            return 1f;
        }
    }

    public static void LoaderCallback() 
    { 
        if (onLoaderCallback != null) 
        { 
            onLoaderCallback(); 
            onLoaderCallback = null;
        }
    }
}
