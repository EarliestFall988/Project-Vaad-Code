using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentWeaponSwapUI : MonoBehaviour
{
    public List<GunData> PrimaryWeapons;
    public List<GunData> SpecialWeapons;

    public EquipmentController controller;

    public bool PrimaryWeaponOptionsVisible = false;
    public bool SpecialWeaponOptionsVisible = false;

    public EquipmentWeaponOption WeaponOptionPrefab;

    public GameObject PrimaryWeaponOptionParent;
    public GameObject SpecialWeaponOptionParent;


    void UpdateItems()
    {
        foreach (Transform child in PrimaryWeaponOptionParent.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (Transform child in SpecialWeaponOptionParent.transform)
        {
            Destroy(child.gameObject);
        }

        PrimaryWeapons = controller.PrimaryGuns;
        SpecialWeapons = controller.SpecialGuns;

        foreach (var item in PrimaryWeapons)
        {
            var option = Instantiate(WeaponOptionPrefab, PrimaryWeaponOptionParent.transform);
            option.SetGunData(item);
        }

        foreach (var item in SpecialWeapons)
        {
            var option = Instantiate(WeaponOptionPrefab, SpecialWeaponOptionParent.transform);
            option.SetGunData(item);
            Debug.Log("Adding special weapon option: " + item.Gun.settings.name);
        }
    }

    void Update()
    {

        PrimaryWeaponOptionParent.SetActive(PrimaryWeaponOptionsVisible);
        SpecialWeaponOptionParent.SetActive(SpecialWeaponOptionsVisible);
    }

    public void TogglePrimaryWeaponOptions()
    {
        PrimaryWeaponOptionsVisible = !PrimaryWeaponOptionsVisible;
        SpecialWeaponOptionsVisible = false;
        UpdateItems();
    }

    public void ToggleSpecialWeaponOptions()
    {
        SpecialWeaponOptionsVisible = !SpecialWeaponOptionsVisible;
        PrimaryWeaponOptionsVisible = false;
        UpdateItems();
    }

    public void OnDisable()
    {
        PrimaryWeaponOptionsVisible = false;
        SpecialWeaponOptionsVisible = false;
    }
}
