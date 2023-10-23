using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

public class AudioImpactController : MonoBehaviour
{
    public AudioSource audioSource;

    public List<AudioClip> WoodAudioClips = new List<AudioClip>();
    public List<AudioClip> MetalAudioClips = new List<AudioClip>();
    public List<AudioClip> DirtAudioClips = new List<AudioClip>();
    public List<AudioClip> WaterAudioClips = new List<AudioClip>();

    public GameObject WoodImpactPrefab;
    public GameObject MetalImpactPrefab;
    public GameObject DirtImpactPrefab;
    public GameObject WaterImpactPrefab;



    public void SpawnWoodImpact(Vector3 normal)
    {
        if (WoodImpactPrefab != null)
        {
            Instantiate(WoodImpactPrefab, transform.position, Quaternion.LookRotation(normal));
        }
    }

    public void SpawnMetalImpact(Vector3 normal)
    {
        if (MetalImpactPrefab != null)
        {
            Instantiate(MetalImpactPrefab, transform.position, Quaternion.LookRotation(normal));
        }
    }

    public void SpawnDirtImpact(Vector3 normal)
    {
        if (DirtImpactPrefab != null)
        {
            Instantiate(DirtImpactPrefab, transform.position, Quaternion.LookRotation(normal));
        }
    }

    public void SpawnWaterImpact(Vector3 normal)
    {
        if (WaterImpactPrefab != null)
        {
            Instantiate(WaterImpactPrefab, transform.position, Quaternion.LookRotation(normal));
        }
    }

    public void PlayWoodImpact()
    {
        if (WoodAudioClips.Count > 0)
        {
            audioSource.PlayOneShot(WoodAudioClips[UnityEngine.Random.Range(0, WoodAudioClips.Count)]);
        }
    }

    public void PlayMetalImpact()
    {
        if (MetalAudioClips.Count > 0)
        {
            audioSource.PlayOneShot(MetalAudioClips[UnityEngine.Random.Range(0, MetalAudioClips.Count)]);
        }
    }

    public void PlayDirtImpact()
    {
        if (DirtAudioClips.Count > 0)
        {
            audioSource.PlayOneShot(DirtAudioClips[UnityEngine.Random.Range(0, DirtAudioClips.Count)]);
        }
    }

    public void PlayWaterImpact()
    {
        if (WaterAudioClips.Count > 0)
        {
            audioSource.PlayOneShot(WaterAudioClips[UnityEngine.Random.Range(0, WaterAudioClips.Count)]);
        }
    }

    public void PlayImpact(string tag, Vector3 location, Vector3 normal)
    {

        transform.position = location;

        switch (tag)
        {
            case "Wood":
                SpawnWoodImpact(normal);
                PlayWoodImpact();
                break;

            case "Metal":
                SpawnDirtImpact(normal);
                PlayMetalImpact();
                break;

            case "Dirt":
                SpawnDirtImpact(normal);
                PlayDirtImpact();
                break;

            case "Water":
                SpawnWaterImpact(normal);
                PlayWaterImpact();
                break;

            //we're missing flesh...

            default:
                // do nothing for now...
                break;
        }
    }
}
