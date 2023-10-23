using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager main;
    public GameObject LoadingScreen;

    private PlayerProgression _playerProgression;

    [SerializeField]
    private SceneIndexes StartingScene = SceneIndexes.CharacterCreator;
    [SerializeField]
    private SceneIndexes MenusScene = SceneIndexes.Menus;
    public bool IsLoading = false;


    private bool _canOpenMenuStore = false;
    public bool CanOpenMenu => _canOpenMenuStore;

    void Awake()
    {
        main = this;
        SceneManager.LoadSceneAsync((int)MenusScene, LoadSceneMode.Additive);

        if (SceneManager.sceneCount < 3)
            SceneManager.LoadSceneAsync((int)StartingScene, LoadSceneMode.Additive);

        LoadingScreen.SetActive(false);
        _playerProgression = new PlayerProgression();
    }

    private List<AsyncOperation> scenesToUnload = new List<AsyncOperation>();
    private List<AsyncOperation> scenesToLoad = new List<AsyncOperation>();

    /// <summary>
    /// Load the next scene(s) and unload old scenes(s)
    /// </summary>
    /// <param name="scenesToUnload">the list of scenes to unload</param>
    /// <param name="scenesToLoad">the list of scenes to load</param>
    /// <param name="includeLoadingScreen">Should the loading screen be shown? Can turn off if the game is only loading new chunks of the game world instead.</param>
    public void LoadNextScene(List<SceneIndexes> scenesToUnload, List<SceneIndexes> scenesToLoad, bool includeLoadingScreen = true)
    {
        IsLoading = true;

        if (includeLoadingScreen)
            LoadingScreen.SetActive(true);

        foreach (var x in scenesToUnload)
        {
            this.scenesToUnload.Add(SceneManager.UnloadSceneAsync((int)x));
        }

        foreach (var x in scenesToLoad)
        {
            this.scenesToLoad.Add(SceneManager.LoadSceneAsync((int)x, LoadSceneMode.Additive));
        }


        StartCoroutine(HandleSceneLoadingOperations());

    }

    public void SetCanOpenMenu(bool canOpen)
    {
        _canOpenMenuStore = canOpen;


        if (MenuController.main != null && MenuController.main.IsMenuOpen && !CanOpenMenu) //close the menu if the user is not allowed to open the menu
        {
            MenuController.main.CloseMenu();
        }
    }

    IEnumerator HandleSceneLoadingOperations()
    {
        for (int i = 0; i < scenesToUnload.Count; i++)
        {
            while (!scenesToUnload[i].isDone)
            {
                yield return null;
            }
        }

        for (int i = 0; i < scenesToLoad.Count; i++)
        {
            while (!scenesToLoad[i].isDone)
            {
                yield return null;
            }
        }

        if (LoadingScreen.activeSelf)
            LoadingScreen.SetActive(false);

        IsLoading = false;
    }
}
