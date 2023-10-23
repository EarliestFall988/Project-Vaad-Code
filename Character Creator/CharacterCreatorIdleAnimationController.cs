using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class CharacterCreatorIdleAnimationController : MonoBehaviour
{

    public Animator animator;
    public bool checkFingers = true;

    public AudioSource audioSource;

    public List<AudioClip> movementAudioClips = new List<AudioClip>();

    public List<AudioClip> RunningAudioClips = new List<AudioClip>();

    public RigBuilder RigBuilder;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CheckFingers(3f));
    }


    public void PlayMovementAudio()
    {
        int x = Random.Range(0, movementAudioClips.Count);
        audioSource.clip = movementAudioClips[x];
        audioSource.Play();
    }

    IEnumerator CheckFingers(float time)
    {
        yield return new WaitForSeconds(time * 10);
        if (checkFingers)
        {
            animator.SetTrigger("CheckFingers");
            float nextCheck = Random.Range(0, 5);

            StartCoroutine(CheckFingers(nextCheck));
        }
    }

    public void PlayRunningAudio()
    {
        int x = Random.Range(0, RunningAudioClips.Count);
        audioSource.clip = RunningAudioClips[x];
        audioSource.Play();
    }

    public void RunOffScene()
    {
        animator.SetTrigger("RunOffScene");
    }

    public void RemoveFootStick()
    {
        RigBuilder.enabled = false;
    }
}
