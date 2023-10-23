using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New/MusicPlayerList")]
public class MusicPlayerList : ScriptableObject
{
    /// <summary>
    /// The list of music
    /// </summary>
    public List<AudioClip> musicList;

    public AudioClip GetRandomClipFromList()
    {
        if (musicList.Count > 0)
        {
            int randomIndex = Random.Range(0, musicList.Count);
            AudioClip clip = musicList[randomIndex];
            return clip;
        }


        Debug.LogError("empty music list");
        return null;
    }
}
