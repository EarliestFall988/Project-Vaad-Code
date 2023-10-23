using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAudioEvents : MonoBehaviour
{

    public List<AudioClip> DrawWeaponAudioClips = new List<AudioClip>();
    public List<AudioClip> RattleClips = new List<AudioClip>();

    public float StrafeSpeed = 0;

    [Header("Running/Main Audio Clips")]
    public List<AudioClip> FootstepFoleyAudioClipsGround = new List<AudioClip>();
    public List<AudioClip> FootstepFoleyAudioClipsConcrete = new List<AudioClip>();
    public List<AudioClip> FootstepFoleyAudioClipsSnow = new List<AudioClip>();
    public List<AudioClip> FootstepFoleyAudioClipsWater = new List<AudioClip>();
    public List<AudioClip> FootstepFoleyAudioClipsMud = new List<AudioClip>();
    public List<AudioClip> FootstepFoleyAudioClipsMetal = new List<AudioClip>();
    public List<AudioClip> FootstepFoleyAudioClipsGrass = new List<AudioClip>();

    [Header("Walking Audio Clips")]
    public List<AudioClip> FootstepFoleyAudioClipsGroundWalk = new List<AudioClip>();
    public List<AudioClip> FootstepFoleyAudioClipsConcreteWalk = new List<AudioClip>();
    public List<AudioClip> FootstepFoleyAudioClipsSnowWalk = new List<AudioClip>();
    public List<AudioClip> FootstepFoleyAudioClipsWaterWalk = new List<AudioClip>();
    public List<AudioClip> FootstepFoleyAudioClipsMudWalk = new List<AudioClip>();
    public List<AudioClip> FootstepFoleyAudioClipsMetalWalk = new List<AudioClip>();
    public List<AudioClip> FootstepFoleyAudioClipsGrassWalk = new List<AudioClip>();

    public string FootstepFoleyAudioClipsGroundTag = "Ground";

    public AudioSource audioSource;

    public void PlayOneDrawWeaponAudioClip()
    {
        if (DrawWeaponAudioClips.Count > 0)
        {
            var clip = DrawWeaponAudioClips[UnityEngine.Random.Range(0, DrawWeaponAudioClips.Count)];
            if (clip != null)
            {
                audioSource.PlayOneShot(clip);
            }
        }
    }

    void OnTriggerEnter(Collider col)
    {
        Debug.Log("collision" + col.gameObject.tag);
        var obj = col.GetComponent<MaterialData>();
        if (obj != null)
        {
            FootstepFoleyAudioClipsGroundTag = obj.MaterialName;
        }
    }

    public void PlayFootstepFoleyAudioClipGround()
    {

        if (StrafeSpeed <= 5)
            return;

        Debug.Log("tag" + FootstepFoleyAudioClipsGroundTag);

        if (FootstepFoleyAudioClipsGroundTag == "Dirt")
        {
            if (FootstepFoleyAudioClipsGround.Count > 0)
            {
                var clip = FootstepFoleyAudioClipsGround[UnityEngine.Random.Range(0, FootstepFoleyAudioClipsGround.Count)];
                if (clip != null)
                {
                    audioSource.PlayOneShot(clip);
                }
            }
            return;
        }

        if (FootstepFoleyAudioClipsGroundTag == "Concrete")
        {
            if (FootstepFoleyAudioClipsConcrete.Count > 0)
            {
                var clip = FootstepFoleyAudioClipsConcrete[UnityEngine.Random.Range(0, FootstepFoleyAudioClipsConcrete.Count)];
                if (clip != null)
                {
                    audioSource.PlayOneShot(clip);
                }
            }

            return;
        }

        if (FootstepFoleyAudioClipsGroundTag == "Snow")
        {
            if (FootstepFoleyAudioClipsSnow.Count > 0)
            {
                var clip = FootstepFoleyAudioClipsSnow[UnityEngine.Random.Range(0, FootstepFoleyAudioClipsSnow.Count)];
                if (clip != null)
                {
                    audioSource.PlayOneShot(clip);
                }
            }

            return;
        }

        if (FootstepFoleyAudioClipsGroundTag == "Water")
        {
            if (FootstepFoleyAudioClipsWater.Count > 0)
            {
                var clip = FootstepFoleyAudioClipsWater[UnityEngine.Random.Range(0, FootstepFoleyAudioClipsWater.Count)];
                if (clip != null)
                {
                    audioSource.PlayOneShot(clip);
                }
            }

            return;
        }


        if (FootstepFoleyAudioClipsGroundTag == "Mud")
        {
            if (FootstepFoleyAudioClipsMud.Count > 0)
            {
                var clip = FootstepFoleyAudioClipsMud[UnityEngine.Random.Range(0, FootstepFoleyAudioClipsMud.Count)];
                if (clip != null)
                {
                    audioSource.PlayOneShot(clip);
                }
            }

            return;
        }


        if (FootstepFoleyAudioClipsGroundTag == "Metal")
        {
            if (FootstepFoleyAudioClipsMetal.Count > 0)
            {
                var clip = FootstepFoleyAudioClipsMetal[UnityEngine.Random.Range(0, FootstepFoleyAudioClipsMetal.Count)];
                if (clip != null)
                {
                    audioSource.PlayOneShot(clip);
                }
            }

            return;
        }


        if (FootstepFoleyAudioClipsGroundTag == "Grass")
        {
            if (FootstepFoleyAudioClipsGrass.Count > 0)
            {
                var clip = FootstepFoleyAudioClipsGrass[UnityEngine.Random.Range(0, FootstepFoleyAudioClipsGrass.Count)];
                if (clip != null)
                {
                    audioSource.PlayOneShot(clip);
                }
            }

            return;
        }

        Debug.LogError("No footstep foley audio clips found for tag: " + FootstepFoleyAudioClipsGroundTag);
    }
    public void PlayFootstepFoleyAudioClipGroundWalk()
    {

        if (StrafeSpeed > 5)
        {
            return;
        }


        // Debug.Log("tag" + FootstepFoleyAudioClipsGroundTag);

        if (FootstepFoleyAudioClipsGroundTag == "Dirt")
        {
            if (FootstepFoleyAudioClipsGroundWalk.Count > 0)
            {
                var clip = FootstepFoleyAudioClipsGround[UnityEngine.Random.Range(0, FootstepFoleyAudioClipsGround.Count)];
                if (clip != null)
                {
                    audioSource.PlayOneShot(clip);
                }
            }
            return;
        }

        if (FootstepFoleyAudioClipsGroundTag == "Concrete")
        {
            if (FootstepFoleyAudioClipsConcreteWalk.Count > 0)
            {
                var clip = FootstepFoleyAudioClipsConcrete[UnityEngine.Random.Range(0, FootstepFoleyAudioClipsConcrete.Count)];
                if (clip != null)
                {
                    audioSource.PlayOneShot(clip);
                }
            }

            return;
        }

        if (FootstepFoleyAudioClipsGroundTag == "Snow")
        {
            if (FootstepFoleyAudioClipsSnowWalk.Count > 0)
            {
                var clip = FootstepFoleyAudioClipsSnow[UnityEngine.Random.Range(0, FootstepFoleyAudioClipsSnow.Count)];
                if (clip != null)
                {
                    audioSource.PlayOneShot(clip);
                }
            }

            return;
        }

        if (FootstepFoleyAudioClipsGroundTag == "Water")
        {
            if (FootstepFoleyAudioClipsWaterWalk.Count > 0)
            {
                var clip = FootstepFoleyAudioClipsWater[UnityEngine.Random.Range(0, FootstepFoleyAudioClipsWater.Count)];
                if (clip != null)
                {
                    audioSource.PlayOneShot(clip);
                }
            }

            return;
        }


        if (FootstepFoleyAudioClipsGroundTag == "Mud")
        {
            if (FootstepFoleyAudioClipsMudWalk.Count > 0)
            {
                var clip = FootstepFoleyAudioClipsMud[UnityEngine.Random.Range(0, FootstepFoleyAudioClipsMud.Count)];
                if (clip != null)
                {
                    audioSource.PlayOneShot(clip);
                }
            }

            return;
        }


        if (FootstepFoleyAudioClipsGroundTag == "Metal")
        {
            if (FootstepFoleyAudioClipsMetalWalk.Count > 0)
            {
                var clip = FootstepFoleyAudioClipsMetal[UnityEngine.Random.Range(0, FootstepFoleyAudioClipsMetal.Count)];
                if (clip != null)
                {
                    audioSource.PlayOneShot(clip);
                }
            }

            return;
        }


        if (FootstepFoleyAudioClipsGroundTag == "Grass")
        {
            if (FootstepFoleyAudioClipsGrassWalk.Count > 0)
            {
                var clip = FootstepFoleyAudioClipsGrass[UnityEngine.Random.Range(0, FootstepFoleyAudioClipsGrass.Count)];
                if (clip != null)
                {
                    audioSource.PlayOneShot(clip);
                }
            }

            return;
        }

        Debug.LogError("No footstep foley audio clips found for tag: " + FootstepFoleyAudioClipsGroundTag);
    }

    public void PlayRattleAudio()
    {
        if (StrafeSpeed > 5)
            return;

        if (RattleClips.Count > 0)
        {
            var clip = RattleClips[UnityEngine.Random.Range(0, RattleClips.Count)];
            if (clip != null)
            {
                audioSource.PlayOneShot(clip);
            }
        }
    }
    public void PlayRattleAudioRun()
    {
        if (StrafeSpeed <= 5)
            return;

        if (RattleClips.Count > 0)
        {
            var clip = RattleClips[UnityEngine.Random.Range(0, RattleClips.Count)];
            if (clip != null)
            {
                audioSource.PlayOneShot(clip);
            }
        }
    }

}
