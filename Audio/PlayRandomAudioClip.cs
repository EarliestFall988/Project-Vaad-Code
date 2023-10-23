using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayRandomAudioClip : MonoBehaviour
{

    public AudioSource Source;
    public List<AudioClip> Clips = new List<AudioClip>();

    private int lastClipIndex = -1;

    // Start is called before the first frame update
    void Start() => StartCoroutine(PlayAudioClip(0));

    IEnumerator PlayAudioClip(float time)
    {

        if (Clips.Count <= 1)
        {
            throw new System.Exception("there needs to be at least two clips in the list");
        }

        yield return new WaitForSeconds(time);

        int clip = Random.Range(0, Clips.Count);

        while (clip == lastClipIndex)
        {
            clip = Random.Range(0, Clips.Count);
        }

        lastClipIndex = clip;

        Source.PlayOneShot(Clips[clip]);

        StartCoroutine(PlayAudioClip(Random.Range(1f, 3f)));

    }
}
