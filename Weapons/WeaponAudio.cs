using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAudio : MonoBehaviour
{

    public GunSettings GunSettings;

    /// <summary>
    /// The audio source for this weapon
    /// </summary>
    public AudioSource automaticFireSource1;

    /// <summary>
    /// The audio source for this weapon
    /// </summary>
    public AudioSource audioSource;

    /// <summary>
    /// Play the fire sound
    /// </summary>
    public void PlayFireSound()
    {


        if (GunSettings.automatic)
            Debug.LogError("Automatic firing sounds not implemented here. Please use PlayAutomaticFireSound() instead.");

        if (GunSettings.shotgun || GunSettings.boltAction)
        {
            audioSource.PlayOneShot(GunSettings.FireSounds[Random.Range(0, GunSettings.FireSounds.Count)]);

            StartCoroutine(FireResetSound());
            return;
        }
        else
        {
            if (GunSettings.FireSounds.Count > 0)
            {
                var clip = GunSettings.FireSounds[Random.Range(0, GunSettings.FireSounds.Count)];
                audioSource.PlayOneShot(GunSettings.FireSounds[Random.Range(0, GunSettings.FireSounds.Count)]);
            }
        }
    }

    /// <summary>
    /// Play the automatic fire sound
    /// </summary>
    /// <param name="isFiring">is the weapon firing?</param>
    public void PlayAutomaticFireSound(bool isFiring)
    {
        if (GunSettings.automatic)
        {
            if (isFiring)
            {
                if (!automaticFireSource1.isPlaying)
                {
                    automaticFireSource1.PlayScheduled(0);
                }
            }
            else
            {
                if (automaticFireSource1.isPlaying)
                {
                    automaticFireSource1.Stop();
                }
            }
        }
        else
        {
            Debug.LogError("Other Firing sounds not implemented here. Please use PlayFireSound() instead.");
        }
    }

    public void PlayEmptySounds()
    {
        if (GunSettings.EmptySounds.Count > 0)
        {
            audioSource.PlayOneShot(GunSettings.EmptySounds[Random.Range(0, GunSettings.EmptySounds.Count)]);
        }
    }


    public void PlayReloadSounds()
    {
        if (GunSettings.shotgun)
        {
            StartCoroutine(ReloadShotgunSounds());
        }
        else
        {
            StartCoroutine(ReloadFoleySounds());
        }
    }

    public void PlayFireTailSound()
    {
        if (GunSettings.FireTailSounds.Count > 0)
        {
            audioSource.PlayOneShot(GunSettings.FireTailSounds[Random.Range(0, GunSettings.FireTailSounds.Count)]);
        }
    }

    IEnumerator ReloadFoleySounds()
    {

        float foleyTime = GunSettings.reloadTime / GunSettings.totalReloadFoleySounds;

        float increment = 0;

        while (increment < GunSettings.reloadTime)
        {

            // Debug.Log("Increment: " + increment);
            // Debug.Log("Foley Time: " + foleyTime);

            if (GunSettings.ReloadFoleySounds.Count > 0)
            {
                audioSource.PlayOneShot(GunSettings.ReloadFoleySounds[Random.Range(0, GunSettings.ReloadFoleySounds.Count)]);
            }

            yield return new WaitForSeconds(foleyTime + Random.Range(-0.5f, 0.5f));
            increment += foleyTime;
        }

        if (GunSettings.ReloadSounds.Count > 0)
        {
            audioSource.PlayOneShot(GunSettings.ReloadSounds[Random.Range(0, GunSettings.ReloadSounds.Count)]);
        }
    }

    IEnumerator FireResetSound()
    {
        yield return new WaitForSeconds(0.25f);
        PlayFireResetSound();
    }

    private void PlayFireResetSound()
    {
        if (GunSettings.FireResetSounds.Count > 0)
        {
            audioSource.PlayOneShot(GunSettings.FireResetSounds[Random.Range(0, GunSettings.FireResetSounds.Count)]);
        }
    }

    IEnumerator ReloadShotgunSounds()
    {
        for (int i = 0; i < 8; i++)
        {
            if (GunSettings.ReloadSounds.Count > 0)
            {
                audioSource.PlayOneShot(GunSettings.ReloadSounds[Random.Range(0, GunSettings.ReloadSounds.Count)]);
            }

            yield return new WaitForSeconds(0.5f);
        }

        yield return new WaitForSeconds(0.25f);

        PlayFireResetSound();
    }

}
