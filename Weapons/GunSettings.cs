using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "New/Weapon Settings")]
public class GunSettings : ScriptableObject
{
    public Sprite WeaponSilhouette; // the weapon sihlouette

    [Header("Gun Settings")]
    public float reloadTime = 5; // 5 seconds to reload
    public float masterFireRate = 1; // 5 shots per second

    public int clipSize = 10; // 10 bullets per clip

    public int bulletsInClip = 10; // 10 bullets in the clip

    public int bulletsLeft = 100; // 100 bullets left

    public int totalBulletsLeft = 100; // 100 bullets left

    public bool automatic = false;

    public float DrawTime = 0.5f; // 0.5 seconds to draw

    [Header("Hip Fire Settings")]
    public float Damage = 10; // 10 damage per bullet
    public float EffectiveRange = 100; // 100 meters
    public Vector2 spreadLimit = new Vector2(0.1f, 0.1f); // how much the bullet sways


    [Header("Aiming Settings")]

    public float zoomFOV = 60;
    public bool scopeZoom = false;
    public float AimDamage = 12; // 10 damage per bullet
    public float AimEffectiveRange = 120; // 100 meters
    public Vector2 AimSpreadLimit = new Vector2(0.05f, 0.05f); // how much the bullet sways

    [Header("Bullet Settings")]
    public float bulletsPerFire = 1; // 1 bullet per fire

    public float fireRate = 1 / 5; // 5 shots per second

    public float KickBackModelAmount = 0.5f; // how much the gun kicks back

    public Vector2 KickBackRange = Vector2.one; // how much the gun kicks back

    public bool KickBackPerBullet = true; // does the gun kick back per bullet?

    [Header("Audio")]

    public List<AudioClip> FireSounds = new List<AudioClip>();
    public List<AudioClip> ReloadSounds = new List<AudioClip>();
    public List<AudioClip> EmptySounds = new List<AudioClip>();

    public List<AudioClip> ReloadFoleySounds = new List<AudioClip>();
    public List<AudioClip> FireResetSounds = new List<AudioClip>();

    public List<AudioClip> FireTailSounds = new List<AudioClip>();

    public int totalReloadFoleySounds = 6;

    [Header("Other")]
    public bool shotgun = false;
    public bool boltAction = false;
}
