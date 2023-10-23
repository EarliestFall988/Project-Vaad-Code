using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicPlayer : MonoBehaviour
{
    public MusicPlayerList MusicList;
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip ThemeClipToPlayFirst;
    public AudioClip CurrentClip => audioSource.clip;
    private bool start = true;

    void Start() => audioSource = GetComponent<AudioSource>();

    [SerializeField]
    private float timeBetweenClips = 100f;

    private bool startingNextClip = false;

    [SerializeField]
    private float clipLength = 0;
    [SerializeField]
    private float currentTime = 0;

    void Update()
    {

        if (start)
        {
            start = false;
            if (ThemeClipToPlayFirst != null)
            {
                audioSource.clip = ThemeClipToPlayFirst;
                audioSource.Play();
                clipLength = audioSource.clip.length;
                currentTime = 0;
            }
            else
            {
                PlayNextRandomClip();
            }
        }


        // if (!audioSource.isPlaying && !startingNextClip)
        // {
        //     StartCoroutine(PlayNextClipAfterDelay());
        //     startingNextClip = true;
        // }

        if (audioSource.isPlaying)
        {
            currentTime += Time.deltaTime;
        }


        if ((currentTime >= clipLength || !audioSource.isPlaying) && !startingNextClip)
        {
            StartCoroutine(PlayNextClipAfterDelay());
            startingNextClip = true;
        }
    }

    IEnumerator PlayNextClipAfterDelay()
    {
        yield return new WaitForSeconds(timeBetweenClips);
        PlayNextRandomClip();
        StopCoroutine(PlayNextClipAfterDelay());
        startingNextClip = false;
    }

    void PlayNextRandomClip()
    {
        AudioClip clip = MusicList.GetRandomClipFromList();
        while (clip == audioSource.clip) // make sure music does not get repeated
        {
            clip = MusicList.GetRandomClipFromList();
        }
        audioSource.clip = clip;
        audioSource.Play();
        clipLength = audioSource.clip.length;
        currentTime = 0;
    }
}

