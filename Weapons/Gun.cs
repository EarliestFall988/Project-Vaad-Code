using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    public float reloadTime = 5; // 5 seconds to reload

    public float masterFireRate = 1; // 5 shots per second

    public int clipSize = 10; // 10 bullets per clip

    public int bulletsInClip = 10; // 10 bullets in the clip

    public int bulletsLeft = 100; // 100 bullets left

    public int totalBulletsLeft = 100; // 100 bullets left

    public bool isReloading = false; // is the gun reloading?

    public bool isFiring = false; // is the gun firing?

    public IBullet bullet;

    public bool automatic = false;

    public GunSettings settings;
    private string gunName = "";

    public bool GoodFireAngle = false;

    public WeaponAudio WeaponAudio; // audio for the weapon

    [Header("Debug")]
    public bool reset = false;

    private bool wasFiring = false;

    [Header("Limits")]
    public bool useLimits = true;
    public Transform Forward;
    public float limits = 300;

    private bool firstToUseWeapon = true;

    public WeaponKick Model;
    public Animator anim;
    public CharacterIKFunctions CharacterIKFunctions;

    public bool isSprinting = false;
    private bool sprintingTrigger = false;

    [Space(5)]
    public bool weaponReady = false;


    /// <summary>
    /// setup the bullet details for this specific weapon
    /// </summary>
    private void SetupBullet()
    {

        if (bullet == null)
        {
            Debug.LogError("No bullet assigned to gun!");
            return;
        }

        bullet.firePoint = transform;
        bullet.SetGunSettings(settings);
    }

    public void SetupWeapon(bool resetAmmo = false)
    {

        if (settings == null)
        {
            Debug.LogError("No settings assigned to gun!");
            return;
        }

        if (weaponReady && !resetAmmo)
            return;

        reloadTime = settings.reloadTime;
        masterFireRate = settings.masterFireRate;
        clipSize = settings.clipSize;

        automatic = settings.automatic;
        WeaponAudio.GunSettings = settings;

        SetupBullet();

        if (resetAmmo)
        {
            bulletsInClip = settings.bulletsInClip;
            bulletsLeft = settings.bulletsLeft;
            totalBulletsLeft = settings.totalBulletsLeft;
        }
    }

    /// <summary>
    /// Enable the weapon (and fire the pull out animation)
    /// </summary> <summary>
    /// 
    /// </summary>
    public void PullOutWeapon()
    {

        if (weaponReady)
            return;

        Model.gameObject.SetActive(true);
        SetWeaponUsable(true);

        anim.SetTrigger("Pull Out Weapon");
    }

    /// <summary>
    /// Disable the weapon entirely (and disable the model)
    /// </summary>
    public void StowWeapon()
    {
        if (!weaponReady)
            return;

        Model.gameObject.SetActive(false);
        SetWeaponUsable(false);

        if (CharacterIKFunctions != null)
            CharacterIKFunctions.SetWeaponsParentToLookAtTarget(false, 0);
    }


    /// <summary>
    /// Make the weapon useable or not useable (does nothing to the weapon model)
    /// </summary>
    /// <param name="isUsable">should the weapon be set usable?</param>
    private void SetWeaponUsable(bool isUsable)
    {
        if (isUsable)
        {
            firstToUseWeapon = false;
            StartCoroutine(DrawTime());
        }
        else
        {
            if (settings.automatic && wasFiring)
            {
                WeaponAudio.PlayAutomaticFireSound(false);
                WeaponAudio.PlayFireTailSound();
                wasFiring = false;
            }

            isFiring = false;
            weaponReady = false;

            CancelReload();

        }
    }

    // private bool IsFiringFromFront()
    // {
    //     Vector3 forward = transform.TransformDirection(Forward.forward);
    //     Vector3 toOther = bullet.firePoint.position - transform.position;

    //     var dot = Vector3.Dot(forward, toOther);

    //     return dot < limits;
    // }

    // Update is called once per frame
    void Update()
    {

        if (reset)
        {
            reset = false;
            SetupWeapon(true);
        }

        if (!isReloading && !isFiring && !bullet.isFiring && useLimits && weaponReady && !isSprinting)
        {
            if (Input.GetMouseButtonDown(0) && !automatic)
            {

                StartCoroutine(Fire());
            }

            if (Input.GetMouseButton(0) && automatic)
            {
                StartCoroutine(Fire());
            }
            else if (wasFiring && automatic && !Input.GetMouseButton(0))
            {
                WeaponAudio.PlayAutomaticFireSound(false);
                WeaponAudio.PlayFireTailSound();
                wasFiring = false;
            }

            if (Input.GetKeyDown(KeyCode.R) && !isReloading && !isFiring)
            {
                StartCoroutine(Reload());
            }
        }

        if (sprintingTrigger != isSprinting) //triggered by the player controller/weapons manager
        {
            sprintingTrigger = isSprinting;

            if (isSprinting)
            {
                SetWeaponUsable(false);

                if (CharacterIKFunctions != null)
                    CharacterIKFunctions.SetWeaponsParentToLookAtTarget(false, 0);
            }
            else
            {
                SetWeaponUsable(true);

                if (CharacterIKFunctions != null)
                    CharacterIKFunctions.SetWeaponsParentToLookAtTarget(true, 1);
            }
        }
    }

    IEnumerator Fire()
    {

        // Debug.Log("Firing!");

        bool fired = false;

        if (bulletsInClip > 0)
        {
            isFiring = true;
            fired = true;
            wasFiring = true;

            bulletsInClip--;
            // Debug.Log("Firing! Bullets left: " + bulletsInClip);


            if (bullet != null)
                bullet.FireBullet(Model);

            if (settings.automatic)
            {
                WeaponAudio.PlayAutomaticFireSound(true);
            }
        }
        else
        {
            if (settings.automatic && wasFiring)
            {
                WeaponAudio.PlayAutomaticFireSound(false);
                WeaponAudio.PlayFireTailSound();
                wasFiring = false;
            }

            isFiring = false;
            if (bulletsLeft > 0)
            {
                Debug.Log("Out of ammo! Reload Time!");
                StartCoroutine(Reload());
            }
            else
            {
                Debug.Log("Out of ammo! No bullets left to reload!");
                WeaponAudio.PlayEmptySounds();
            }
        }



        yield return new WaitForSeconds(masterFireRate);



        if (fired)
        {
            isFiring = false;
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;
        if (CharacterIKFunctions != null)
            CharacterIKFunctions.SetWeaponsParentToLookAtTarget(false, 0);

        if (anim != null)
            anim.SetBool("Reloading", true);

        WeaponAudio.PlayReloadSounds();

        // Debug.Log("Reloading...");

        yield return new WaitForSeconds(reloadTime);


        // if (!settings.shotgun)
        //     WeaponAudio.PlayReloadSounds();


        if (bulletsLeft < clipSize)
        {
            bulletsInClip = bulletsLeft;
            bulletsLeft = 0;
        }
        else
        {
            bulletsLeft -= clipSize;
            bulletsInClip = clipSize;
        }

        Debug.Log("Reloaded! Bullets in clip: " + bulletsInClip);

        isReloading = false;
        if (anim != null)
            anim.SetBool("Reloading", false);

        if (CharacterIKFunctions != null)
            CharacterIKFunctions.SetWeaponsParentToLookAtTarget(true, 1);
    }

    void CancelReload()
    {
        StopCoroutine(Reload());
        isReloading = false;
        if (anim != null)
            anim.SetBool("Reloading", false);
    }


    public int AddToBulletsLeft(int amt)
    {
        amt = Mathf.Abs(amt);
        int canPickUpAmt = Mathf.Clamp(amt, 0, totalBulletsLeft - bulletsLeft);

        bulletsLeft += canPickUpAmt;

        return Mathf.Abs(amt - canPickUpAmt);
    }

    IEnumerator DrawTime()
    {
        yield return new WaitForSeconds(settings.DrawTime);
        weaponReady = true;

        if (CharacterIKFunctions != null && !isSprinting)
            CharacterIKFunctions.SetWeaponsParentToLookAtTarget(true, 1);
    }

    void OnEnable()
    {
        if (gunName != settings.name || firstToUseWeapon) // initialize the weapon
        {
            gunName = settings.name;
            SetupWeapon(firstToUseWeapon);
        }

        PullOutWeapon(); // turn it on...
    }

    void OnDisable()
    {
        StowWeapon(); // turn it off ...
    }
}
