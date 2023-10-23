using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WeaponsManager : MonoBehaviour
{

    public List<Gun> PrimaryWeapons = new List<Gun>();
    public List<Gun> SpecialWeapons = new List<Gun>();

    public GameObject Primary => PrimaryGun.gameObject;
    public Gun PrimaryGun;

    public GameObject Special => SpecialGun.gameObject;
    public Gun SpecialGun;

    public GameObject CurrentActiveWeapon => CurrentActiveGun.gameObject;
    public Gun CurrentActiveGun;

    private bool setFirstTwoWeapons = true;

    public UnityEvent<bool> OnWeaponSwap = new UnityEvent<bool>();
    public UnityEvent<Gun> OnPrimaryWeaponUpdate = new UnityEvent<Gun>();
    public UnityEvent<Gun> OnSecondaryWeaponUpdate = new UnityEvent<Gun>();

    public UnityEvent<AimingType> Aiming = new UnityEvent<AimingType>();
    private AimingType currentAimingTypeTrigger = AimingType.Reset;

    public UnityEvent<float> OnReload = new UnityEvent<float>();
    private bool firedReloadEvent = false;

    public Animator Animator;
    public CharacterIKFunctions characterIKFunctions;
    public bool GoodFireAngle = false;
    public bool isSprinting = false;
    public bool isFiring = false;
    public float ZoomCameraFOV = 60;

    public bool isAiming = false;

    // Start is called before the first frame update
    void Start()
    {

        LeanTween.delayedCall(1, () =>
        {
            if (setFirstTwoWeapons)
            {
                if (SpecialWeapons.Count > 0)
                    SetSpecialWeapon(SpecialWeapons[0], false);

                if (PrimaryWeapons.Count > 0)
                    SetPrimaryWeapon(PrimaryWeapons[0]);

                return;
            }
        });
    }

    public void SetPrimaryWeapon(Gun weapon, bool equip = true)
    {

        var gun = weapon.GetComponentInChildren<Gun>();

        OnPrimaryWeaponUpdate?.Invoke(gun);
        gun.anim = Animator;
        gun.CharacterIKFunctions = characterIKFunctions;
        PrimaryGun = gun;

        if (equip)
            SetPrimaryWeaponActive();
    }

    public void SetSpecialWeapon(Gun weapon, bool equip = true)
    {


        var gun = weapon.GetComponentInChildren<Gun>();

        OnSecondaryWeaponUpdate?.Invoke(gun);
        gun.anim = Animator;
        gun.CharacterIKFunctions = characterIKFunctions;
        SpecialGun = gun;

        if (equip)
            SetSpecialWeaponActive();
    }

    public List<Gun> GetPrimaryWeapons()
    {
        var guns = new List<Gun>();

        foreach (var weapon in PrimaryWeapons)
        {
            // Debug.Log("primary weapon " + weapon.name);
            guns.Add(weapon.GetComponentInChildren<Gun>());
        }

        return guns;
    }

    public List<Gun> GetSpecialWeapons()
    {
        var guns = new List<Gun>();

        foreach (var weapon in SpecialWeapons)
        {
            // Debug.Log("special weapon " + weapon.name);
            guns.Add(weapon.GetComponentInChildren<Gun>());
        }

        return guns;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ToggleWeapons();
        }

        // Debug.Log("sprinting " + isSprinting);


        if (PrimaryGun != null)
        {

            PrimaryGun.isSprinting = isSprinting;
            PrimaryGun.GoodFireAngle = GoodFireAngle;
        }

        if (SpecialGun != null)
        {
            SpecialGun.isSprinting = isSprinting;
            SpecialGun.GoodFireAngle = GoodFireAngle;
        }

        if (CurrentActiveGun != null)
        {
            isFiring = CurrentActiveGun.isFiring;
            ZoomCameraFOV = CurrentActiveGun.settings.zoomFOV;
            if (CurrentActiveGun.isReloading && !firedReloadEvent)
            {
                OnReload?.Invoke(CurrentActiveGun.settings.reloadTime);
                firedReloadEvent = true;
            }

            if (firedReloadEvent && !CurrentActiveGun.isReloading)
            {
                OnReload?.Invoke(-1);
                firedReloadEvent = false;
            }

            if (isAiming && CurrentActiveGun.weaponReady && !CurrentActiveGun.isReloading)
            {
                if (CurrentActiveGun.settings.scopeZoom)
                {

                    if (currentAimingTypeTrigger != AimingType.Scope)
                    {
                        Aiming?.Invoke(AimingType.Scope);
                        currentAimingTypeTrigger = AimingType.Scope;
                    }
                }
                else
                {

                    if (currentAimingTypeTrigger != AimingType.SimpleAim)
                    {
                        Aiming?.Invoke(AimingType.SimpleAim);
                        currentAimingTypeTrigger = AimingType.SimpleAim;
                    }
                }
            }
            else if (!CurrentActiveGun.weaponReady || CurrentActiveGun.isReloading)
            {
                if (currentAimingTypeTrigger != AimingType.NotUsingWeapon)
                {
                    Aiming?.Invoke(AimingType.NotUsingWeapon);
                    currentAimingTypeTrigger = AimingType.NotUsingWeapon;
                }
            }
            else
            {

                if (currentAimingTypeTrigger != AimingType.HipFire)
                {
                    Aiming?.Invoke(AimingType.HipFire);
                    currentAimingTypeTrigger = AimingType.HipFire;
                }
            }
        }
        else
        {
            if (currentAimingTypeTrigger != AimingType.Reset)
            {
                Aiming?.Invoke(AimingType.Reset);
                currentAimingTypeTrigger = AimingType.Reset;
            }
        }
    }

    void ToggleWeapons()
    {
        if (Primary != null && Special != null)
        {
            if (Primary.activeInHierarchy)
            {
                SetSpecialWeaponActive();
            }
            else if (Special.activeInHierarchy)
            {
                SetPrimaryWeaponActive();
            }
        }
        else if (Primary == null && Special != null)
        {

            SetSpecialWeaponActive();
            return;
        }
        else if (Special == null && Primary != null)
        {
            SetPrimaryWeaponActive();
            return;
        }

        // Debug.Log("Player Has No Weapons!");
    }

    void SetPrimaryWeaponActive()
    {
        if (Special != null)
            Special.transform.parent.gameObject.SetActive(false);

        for (int i = 0; i < PrimaryWeapons.Count; i++)
        {
            PrimaryWeapons[i].transform.parent.gameObject.SetActive(false);
        }

        Debug.Log("setting primary weapon active " + PrimaryGun.settings.name);

        PrimaryGun.transform.parent.gameObject.SetActive(true);


        CurrentActiveGun = PrimaryGun;

        OnWeaponSwap?.Invoke(true);
    }

    void SetSpecialWeaponActive()
    {
        if (Primary != null)
            Primary.transform.parent.gameObject.SetActive(false);

        for (int i = 0; i < SpecialWeapons.Count; i++)
        {
            SpecialWeapons[i].transform.parent.gameObject.SetActive(false);
        }

        SpecialGun.transform.parent.gameObject.SetActive(true);

        CurrentActiveGun = SpecialGun;

        OnWeaponSwap?.Invoke(false);
    }

    public void SetAiming(bool aiming)
    {
        isAiming = aiming;
    }


    public int AmmoPickup(AmmoPickup pickup)
    {
        if (pickup.AmmoType == "Primary")
        {
            return PrimaryGun.AddToBulletsLeft(pickup.AmmoAmount);
        }
        else if (pickup.AmmoType == "Special")
        {
            return SpecialGun.AddToBulletsLeft(pickup.AmmoAmount);
        }

        return 0;
    }

    public void AmmoPickup(int amount)
    {
        if (CurrentActiveGun != null)
        {
            CurrentActiveGun.AddToBulletsLeft(amount);
        }
    }
}

public enum AimingType
{
    Reset,
    NotUsingWeapon,
    HipFire,
    SimpleAim,
    Scope
}
