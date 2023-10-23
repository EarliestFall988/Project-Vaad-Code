
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Interface for all bullets
/// </summary>
public abstract class IBullet : MonoBehaviour
{

    public float bulletsPerFire = 1; // 1 bullet per fire
    public float fireRate = 1 / 5; // 5 shots per second
    public Transform firePoint; // where the bullet is fired from
    public bool isFiring { get; private set; } // is the bullet firing?

    public Vector2 spreadLimit = new Vector2(0.1f, 0.1f); // how much the bullet sways
    public Vector2 KickBackRange = Vector2.one; // how much the gun kicks back
    public bool KickBackPerBullet = false; // does the gun kick back per bullet?

    public UnityEvent<Vector2> OnKickBack; // event for kickback

    public WeaponAudio WeaponAudio; // audio for the weapon

    public GunSettings Settings; // settings for the gun

    public void SetGunSettings(GunSettings settings)
    {
        bulletsPerFire = settings.bulletsPerFire;
        fireRate = settings.fireRate;
        spreadLimit = settings.spreadLimit;
        KickBackRange = settings.KickBackRange;
        KickBackPerBullet = settings.KickBackPerBullet;

        Settings = settings;
    }

    // <summary>
    /// Fire the bullet
    /// </summary>
    public void FireBullet(WeaponKick kick)
    {
        isFiring = true;
        StartCoroutine(Fire(kick));
    }

    /// <summary>
    /// Fire the bullet - paying attention to the fire rate and dispense of each bullet
    /// </summary>
    /// <returns></returns>
    IEnumerator Fire(WeaponKick kick)
    {
        for (int i = 0; i < bulletsPerFire; i++) // could think about subtracting bullets from clip here however it would be better to do it in the gun class
        {
            EjectBullet(CalculateSpread(), Settings.Damage, Settings.EffectiveRange);

            if (KickBackPerBullet)
            {
                KickBack(kick);
                if (!Settings.automatic)
                {
                    WeaponAudio.PlayFireSound();
                    WeaponAudio.PlayFireTailSound();
                    WeaponAudio.PlayEmptySounds();
                }
            }

            yield return new WaitForSeconds(fireRate);
        }

        if (!KickBackPerBullet)
        {
            KickBack(kick);
            if (!Settings.automatic)
            {
                WeaponAudio.PlayFireSound();
                WeaponAudio.PlayFireTailSound();
            }
        }
        else
        {
            if (!Settings.automatic)
            {
                WeaponAudio.PlayFireTailSound();
                WeaponAudio.PlayFireTailSound();
            }
        }



        isFiring = false;
    }



    private Vector3 CalculateSpread()
    {
        Vector3 centerDirection = firePoint.forward;
        Vector3 spread = Vector3.zero;

        spread.z = Random.Range(-spreadLimit.x / 100, spreadLimit.x / 100);
        spread.y = Random.Range(-spreadLimit.y / 100, spreadLimit.y / 100);

        Vector3 direction = centerDirection + spread;

        return direction;
    }


    private void KickBack(WeaponKick kick)
    {
        //place kickback here
        if (OnKickBack != null)
            OnKickBack.Invoke(new Vector2(Random.Range(0, KickBackRange.x), Random.Range(0, KickBackRange.y)));

        kick.Kick(Settings.KickBackModelAmount);
    }


    /// <summary>
    /// Eject the bullet
    /// </summary>
    protected abstract void EjectBullet(Vector3 spread, float damage, float range);
}
