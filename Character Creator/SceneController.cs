using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class SceneController : MonoBehaviour
{

    public Image blackoutImage;

    public GameObject LoadingScreen;

    private bool CrossFade = false;
    public List<AudioSource> AudioToFadeOut = new List<AudioSource>();

    public SceneIndexes NextSceneIndex = SceneIndexes.AITesting;

    public UnityEvent OnEnd;

    public bool Quit;
    public bool NextScene;

    float alpha = 0;

    public void End()
    {
        StartCoroutine(EndAfter(1.5f));
    }

    IEnumerator EndAfter(float time)
    {


        OnEnd?.Invoke();

        yield return new WaitForSeconds(time);
        // LoadingScreen.SetActive(true);
        GameManager.main.LoadNextScene(new List<SceneIndexes>() { SceneIndexes.CharacterCreator }, new List<SceneIndexes>() { NextSceneIndex });
        // blackoutImage.gameObject.SetActive(true);
        // CrossFade = true;



        yield return new WaitForSeconds(1.5f);

        // if (Quit)
        //     Application.Quit();
        // else if (NextScene)
        //     UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 1);
        // else
        //     UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }

    void Update()
    {
        if (CrossFade)
        {
            // blackoutImage.color = new Color(blackoutImage.color.r, blackoutImage.color.g, blackoutImage.color.b, alpha);
            // alpha += 1f * Time.deltaTime;

            foreach (var x in AudioToFadeOut)
            {
                if (x == null)
                    continue;

                if (x.volume > 0)
                    x.volume -= 0.5f * Time.deltaTime;
            }
        }
    }
}
