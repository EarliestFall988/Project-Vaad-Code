using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTestHelper : MonoBehaviour
{
    public bool EnableMenuAccess = false;
    public bool LoadScenes = true;
    public bool RemoveMenu = false;

    void Start() => SetLoadScene();

    public void SetLoadScene()
    {
        if (LoadScenes)
        {
            if (SceneManager.GetSceneByBuildIndex((int)SceneIndexes.Menus).isLoaded && RemoveMenu)
            {
                SceneManager.UnloadSceneAsync((int)SceneIndexes.Menus);
            }

            if (GameManager.main == null)
                SceneManager.LoadSceneAsync(0, LoadSceneMode.Additive);
        }

        StartCoroutine(SetEnableAccess());
    }

    IEnumerator SetEnableAccess()
    {
        yield return new WaitForSeconds(1f);
        GameManager.main.SetCanOpenMenu(EnableMenuAccess);
    }
}
