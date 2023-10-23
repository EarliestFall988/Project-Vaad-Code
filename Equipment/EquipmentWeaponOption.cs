using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquipmentWeaponOption : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private GunData GunData;
    [SerializeField]
    private Image ImageUI;

    public void SetGunData(GunData gunData)
    {
        GunData = gunData;
        ImageUI.sprite = gunData.Gun.settings.WeaponSilhouette;
    }

    public void Select()
    {
        Debug.Log(GunData);
        if (GunData != null && GunData.Location == "primary")
            CharacterEventBus.main.weaponsManager.SetPrimaryWeapon(GunData.Gun); // should probably be a little more robust than this

        if (GunData != null && GunData.Location == "special")
            CharacterEventBus.main.weaponsManager.SetSpecialWeapon(GunData.Gun); // should probably be a little more robust than this
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Select();
    }
}

