using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartController : MonoBehaviour
{

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            GoToCharacterController();
        }
    }

    public void GoToCharacterController()
    {
        GameManager.main.LoadNextScene(new List<SceneIndexes>() { SceneIndexes.Start }, new List<SceneIndexes>() { SceneIndexes.CharacterCreator });
    }
}
