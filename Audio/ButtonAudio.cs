using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonAudio : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler
{

    public AudioSource source;

    public List<AudioClip> SelectClips = new List<AudioClip>();
    public List<AudioClip> HoverClips = new List<AudioClip>();

    public void OnPointerClick(PointerEventData eventData)
    {
        if (SelectClips.Count > 0)
        {
            int randomIndex = Random.Range(0, SelectClips.Count);
            AudioClip clip = SelectClips[randomIndex];
            source.clip = clip;
            source.Play();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (HoverClips.Count > 0)
        {
            int randomIndex = Random.Range(0, HoverClips.Count);
            AudioClip clip = HoverClips[randomIndex];
            source.clip = clip;
            source.Play();
        }
    }
}
