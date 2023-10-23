using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Mathematics;
using System;

public class WeaponUIInfo : MonoBehaviour
{

    public TMPro.TMP_Text PrimaryAmmoText;
    public TMPro.TMP_Text SpecialAmmoText;

    public Image PrimaryWeaponImage;
    public Image SpecialWeaponImage;

    public Image PrimaryTotalBulletsImage;
    public Image SpecialTotalBulletsImage;

    public RectTransform PrimaryWeaponContainer;
    public RectTransform SpecialWeaponContainer;


    public bool toggle = false;
    private bool primary = false;

    public float sizeDeltaEnabled = 30;
    public float sizeDeltaDisabled = -30;

    public float anchoredPositionEnabled = -15;
    public float anchoredPositionDisabled = 15;

    public float bulletTextSizeEnabledPrimary = 30;
    public float bulletTextSizeDisabledPrimary = 15;

    public float WeaponImagePrimarySizeEnabled = 30;
    public float WeaponImagePrimarySizeDisabled = 15;

    public float sizeDeltaEnabledSecondary = 30;
    public float sizeDeltaDisabledSecondary = -30;

    public float anchoredPositionEnabledSecondary = -15;
    public float anchoredPositionDisabledSecondary = 15;

    public float bulletTextSizeEnabledSecondary = 30;
    public float bulletTextSizeDisabledSecondary = 15;

    public float WeaponImageSecondarySizeEnabled = 30;
    public float WeaponImageSecondarySizeDisabled = 15;

    private Gun PrimaryWeapon;
    private Gun SecondaryWeapon;

    void Update()
    {
        if (toggle)
        {
            SetPrimaryOrSecondaryWeaponActive(!primary);
            toggle = false;
        }


        if (PrimaryWeapon != null)
            UpdatePrimaryWeapon(PrimaryWeapon.isReloading ? 0 : PrimaryWeapon.bulletsInClip, PrimaryWeapon.settings.clipSize, PrimaryWeapon.bulletsLeft, PrimaryWeapon.settings.totalBulletsLeft, PrimaryWeapon.settings.WeaponSilhouette);
        if (SecondaryWeapon != null)
            UpdateSpecialWeapon(SecondaryWeapon.isReloading ? 0 : SecondaryWeapon.bulletsInClip, SecondaryWeapon.settings.clipSize, SecondaryWeapon.bulletsLeft, SecondaryWeapon.settings.totalBulletsLeft, SecondaryWeapon.settings.WeaponSilhouette);
    }

    public void SetPrimaryWeapon(Gun g)
    {
        PrimaryWeapon = g;
        // UpdatePrimaryWeapon(g.bulletsInClip, g.settings.bulletsLeft, g.settings.totalBulletsLeft, g.settings.WeaponSilhouette);
    }

    public void SetSecondaryWeapon(Gun g)
    {
        SecondaryWeapon = g;
        // UpdateSpecialWeapon(g.bulletsInClip, g.settings.bulletsLeft, g.settings.totalBulletsLeft, g.settings.WeaponSilhouette);
    }

    public void SetPrimaryOrSecondaryWeaponActive(bool primary)
    {

        Debug.Log("weapon primary active? " + primary);

        if (primary)
        {
            // PrimaryWeaponImage.color = Color.white;
            // SpecialWeaponImage.color = Color.grey;


            LeanTween.value(PrimaryWeaponContainer.gameObject, (v) =>
            {
                PrimaryWeaponContainer.sizeDelta = new Vector2(PrimaryWeaponContainer.sizeDelta.x, v.y);
                PrimaryWeaponContainer.anchoredPosition = new Vector2(PrimaryWeaponContainer.anchoredPosition.x, -v.y / 2);

            }, PrimaryWeaponContainer.sizeDelta, new Vector2(PrimaryWeaponContainer.sizeDelta.x, sizeDeltaEnabled), 0.25f).setEaseInOutCubic();

            LeanTween.value(PrimaryAmmoText.gameObject, bulletTextSizeDisabledPrimary, bulletTextSizeEnabledPrimary, 0.25f).setEaseInOutBack().setOnUpdate((float v) =>
            {
                PrimaryAmmoText.fontSize = v;
            });


            LeanTween.value(SpecialWeaponContainer.gameObject, (v) =>
            {
                SpecialWeaponContainer.sizeDelta = new Vector2(SpecialWeaponContainer.sizeDelta.x, v.y);
                SpecialWeaponContainer.anchoredPosition = new Vector2(SpecialWeaponContainer.anchoredPosition.x, v.y / 2);


            }, SpecialWeaponContainer.sizeDelta, new Vector2(SpecialWeaponContainer.sizeDelta.x, sizeDeltaDisabledSecondary), 0.25f).setEaseInOutCubic();


            LeanTween.value(SpecialAmmoText.gameObject, bulletTextSizeEnabledSecondary, bulletTextSizeDisabledSecondary, 0.25f).setEaseInOutBack().setOnUpdate((float v) =>
            {
                SpecialAmmoText.fontSize = v;
            });

            LeanTween.value(PrimaryWeaponImage.gameObject, (v) =>
            {
                PrimaryWeaponImage.color = v;
            }, PrimaryWeaponImage.color, Color.white, 0.5f).setEaseInOutCirc();

            LeanTween.value(PrimaryWeaponImage.gameObject, (v) =>
            {

                PrimaryWeaponImage.rectTransform.rotation = Quaternion.Euler(v);

            }, PrimaryWeaponImage.rectTransform.rotation.eulerAngles, new Vector3(0, 0, 30), 0.5f).setEaseInOutBack();


            LeanTween.value(SpecialWeaponImage.gameObject, (v) =>
            {
                SpecialWeaponImage.color = v;
            }, SpecialWeaponImage.color, Color.gray, 0.25f).setEaseInOutCirc();


            LeanTween.value(SpecialWeaponImage.gameObject, (v) =>
            {
                SpecialWeaponImage.rectTransform.rotation = Quaternion.Euler(v);
            }, SpecialWeaponImage.rectTransform.rotation.eulerAngles, new Vector3(0, 0, 0), 0.5f).setEaseInOutBack();

        }
        else
        {
            // PrimaryWeaponImage.color = Color.grey;
            // SpecialWeaponImage.color = Color.white;


            LeanTween.value(PrimaryWeaponContainer.gameObject, (v) =>
            {
                PrimaryWeaponContainer.sizeDelta = new Vector2(PrimaryWeaponContainer.sizeDelta.x, v.y);
                PrimaryWeaponContainer.anchoredPosition = new Vector2(PrimaryWeaponContainer.anchoredPosition.x, -v.y / 2);

            }, PrimaryWeaponContainer.sizeDelta, new Vector2(PrimaryWeaponContainer.sizeDelta.x, sizeDeltaDisabled), 0.25f).setEaseInOutCubic();

            LeanTween.value(PrimaryAmmoText.gameObject, bulletTextSizeEnabledPrimary, bulletTextSizeDisabledPrimary, 0.25f).setEaseInOutBack().setOnUpdate((float v) =>
            {
                PrimaryAmmoText.fontSize = v;
            });


            LeanTween.value(SpecialWeaponContainer.gameObject, (v) =>
            {
                SpecialWeaponContainer.sizeDelta = new Vector2(SpecialWeaponContainer.sizeDelta.x, v.y);
                SpecialWeaponContainer.anchoredPosition = new Vector2(SpecialWeaponContainer.anchoredPosition.x, v.y / 2);

            }, SpecialWeaponContainer.sizeDelta, new Vector2(SpecialWeaponContainer.sizeDelta.x, sizeDeltaEnabledSecondary), 0.25f).setEaseInOutCubic();


            LeanTween.value(SpecialAmmoText.gameObject, bulletTextSizeDisabledSecondary, bulletTextSizeEnabledSecondary, 0.25f).setEaseInOutBack().setOnUpdate((float v) =>
            {
                SpecialAmmoText.fontSize = v;
            });

            LeanTween.value(PrimaryWeaponImage.gameObject, (v) =>
            {
                PrimaryWeaponImage.color = v;
            }, PrimaryWeaponImage.color, Color.grey, 0.25f).setEaseInOutCubic();

            LeanTween.value(PrimaryWeaponImage.gameObject, (v) =>
            {
                PrimaryWeaponImage.rectTransform.rotation = Quaternion.Euler(v);

            }, PrimaryWeaponImage.rectTransform.rotation.eulerAngles, new Vector3(0, 0, 0), 0.5f).setEaseInOutBack();


            LeanTween.value(SpecialWeaponImage.gameObject, (v) =>
            {
                SpecialWeaponImage.color = v;
            }, SpecialWeaponImage.color, Color.white, 0.5f).setEaseInOutCubic();

            LeanTween.value(SpecialWeaponImage.gameObject, (v) =>
            {
                SpecialWeaponImage.rectTransform.rotation = Quaternion.Euler(v);

            }, SpecialWeaponImage.rectTransform.rotation.eulerAngles, new Vector3(0, 0, 30), 0.5f).setEaseInOutBack();
        }

        this.primary = primary;
    }

    public void UpdatePrimaryWeapon(float ammoCount, float totalClipCount, float currentAmmoToCarry, float ammoCarryLimit, Sprite primaryWeaponImage)
    {
        PrimaryAmmoText.text = ammoCount.ToString();

        if (ammoCount / totalClipCount < 0.3)
        {
            PrimaryAmmoText.color = Color.red;
        }
        else
        {
            PrimaryAmmoText.color = Color.white;
        }
        // PrimaryTotalBulletsImage.fillAmount = currentAmmoToCarry / ammoCarryLimit;
        LeanTween.value(PrimaryTotalBulletsImage.gameObject, (v) =>
        {
            PrimaryTotalBulletsImage.fillAmount = v;
        }, PrimaryTotalBulletsImage.fillAmount, currentAmmoToCarry / ammoCarryLimit, 0.25f).setEaseInOutCubic();
        PrimaryWeaponImage.sprite = primaryWeaponImage;
    }

    public void UpdateSpecialWeapon(float ammoCount, float totalClipCount, float currentAmmoToCarry, float ammoCarryLimit, Sprite secondaryWeaponImage)
    {
        SpecialAmmoText.text = ammoCount.ToString();
        // SpecialTotalBulletsImage.fillAmount = currentAmmoToCarry / ammoCarryLimit;

        if (ammoCount / totalClipCount < 0.3)
        {
            SpecialAmmoText.color = Color.red;
        }
        else
        {
            SpecialAmmoText.color = Color.white;
        }

        LeanTween.value(SpecialTotalBulletsImage.gameObject, (v) =>
        {
            SpecialTotalBulletsImage.fillAmount = v;
        }, SpecialTotalBulletsImage.fillAmount, currentAmmoToCarry / ammoCarryLimit, 0.25f).setEaseInOutCubic();

        SpecialWeaponImage.sprite = secondaryWeaponImage;
    }
}
