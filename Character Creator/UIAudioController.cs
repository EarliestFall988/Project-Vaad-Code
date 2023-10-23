using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class UIAudioController : MonoBehaviour
{

    AudioSource source;

    public List<AudioClip> swapAudioClips = new List<AudioClip>();
    public AudioClip selectAudioClip;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void PlaySwapAudio()
    {
        int x = Random.Range(0, swapAudioClips.Count);
        source.clip = swapAudioClips[x];
        source.Play();
    }

    public void PlaySelectAudio()
    {
        source.clip = selectAudioClip;
        source.Play();
    }
}
