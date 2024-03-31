using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    public Transform playerTransform;
    public SceneLoadEventSO loadEvent;
    public GameSceneSO menuScene;
    public GameSceneSO firstLoadScene;
    public GameSceneSO secondLoadScene;
    public GameSceneSO sceneToLoad;
    public GameSceneSO currentScene;
    public Vector3 posToGo;
    public bool isFade;
    public float fadeDuration;
    public bool isLoading;
    public PlayerController playerController;

    [Header("广播")]
    public VoidEventSO afterSceneLoadedEvent;
    public FadeEventSO fadeEvent;



    private void Awake()
    {
        // Addressables.LoadSceneAsync(firstLoadScene.sceneReference, LoadSceneMode.Additive);
        currentScene = menuScene;
        currentScene.sceneReference.LoadSceneAsync(LoadSceneMode.Additive);
        loadEvent.RaiseLoadRequestEvent(sceneToLoad, posToGo, true);
    }

    private void OnEnable()
    {
        loadEvent.LoadRequestEvent += OnLoadRequestEvent;
    }

    private void OnDisable()
    {
        loadEvent.LoadRequestEvent -= OnLoadRequestEvent;
    }

    private void OnLoadRequestEvent(GameSceneSO scene, Vector3 pos, bool isFade)
    {
        if (isLoading) return;
        isLoading = true;
        sceneToLoad = scene;
        posToGo = pos;
        this.isFade = isFade;
        if (currentScene != null)
            StartCoroutine(UnloadPreviousScene());
    }

    private IEnumerator UnloadPreviousScene()
    {
        if (isFade)
        {
            fadeEvent.FadeIn(fadeDuration);
        }

        yield return new WaitForSeconds(fadeDuration);
        currentScene.sceneReference.UnLoadScene();
        playerTransform.gameObject.SetActive(false);
        LoadNewScene();

    }

    private void LoadNewScene()
    {
        var loadNewScene = sceneToLoad.sceneReference.LoadSceneAsync(LoadSceneMode.Additive, true);
        loadNewScene.Completed += OnLoadCompleted;

    }

    private void OnLoadCompleted(AsyncOperationHandle<SceneInstance> handle)
    {
        currentScene = sceneToLoad;

        playerTransform.position = posToGo;

        playerTransform.gameObject.SetActive(true);

        if (isFade)
        {
            // todo
            fadeEvent.FadeOut(fadeDuration);
        }
        isLoading = false;

        if(currentScene.sceneType == SceneType.Location) afterSceneLoadedEvent.RaiseEvent();
  
        Cinemachine.CinemachineVirtualCamera virtualCamera = FindObjectOfType<Cinemachine.CinemachineVirtualCamera>();

        // 将Follow目标设置为空
        virtualCamera.Follow = playerTransform;
        virtualCamera.LookAt = playerTransform;
    }
}
