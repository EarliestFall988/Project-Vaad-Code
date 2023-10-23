using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class ReticleController : MonoBehaviour
{

    public Image LeftReloadReticleCircle;
    public Image RightReloadReticleCircle;
    public Image ReticleDot;
    public Image ReticleBlock;

    public Image PressToActivateDialog;
    public TMPro.TMP_Text PressToActivateDialogText;

    void Start() => Reload(0);

    public void Reload(float reloadTime)
    {

        if (reloadTime > 0)
        {
            LeanTween.value(gameObject, (float val) =>
            {
                LeftReloadReticleCircle.fillAmount = val / 0.5f;
                RightReloadReticleCircle.fillAmount = val / 0.5f;
            }, 0, 0.5f, reloadTime);


            LeanTween.delayedCall(reloadTime + 0.125f, () =>
            {
                LeftReloadReticleCircle.fillAmount = 0;
                RightReloadReticleCircle.fillAmount = 0;
            });

        }
        else
        {

            LeanTween.cancel(gameObject);

            LeftReloadReticleCircle.fillAmount = 0;
            RightReloadReticleCircle.fillAmount = 0;
        }
    }

    public void Aiming(AimingType type)
    {

        Debug.Log("recevied aim type: " + type);

        if (type == AimingType.Scope)
        {
            LeftReloadReticleCircle.gameObject.SetActive(false);
            RightReloadReticleCircle.gameObject.SetActive(false);
            ReticleBlock.gameObject.SetActive(false);
            return;
        }
        else
        {
            LeftReloadReticleCircle.gameObject.SetActive(true);
            RightReloadReticleCircle.gameObject.SetActive(true);
            ReticleBlock.gameObject.SetActive(true);

            ReticleDot.gameObject.SetActive(!(type == AimingType.NotUsingWeapon));
        }

        if (type == AimingType.HipFire)
        {

            LeanTween.value(gameObject, (v) =>
            {
                ReticleBlock.rectTransform.sizeDelta = v;
            }, ReticleBlock.rectTransform.sizeDelta, new Vector2(70, 70), 0.125f);

            ReticleBlock.rectTransform.rotation = quaternion.Euler(new Vector3(0, 0, 40));
        }

        if (type == AimingType.SimpleAim)
        {
            LeanTween.value(gameObject, (v) =>
           {
               ReticleBlock.rectTransform.sizeDelta = v;
           }, ReticleBlock.rectTransform.sizeDelta, new Vector2(30, 30), 0.125f);
            ReticleBlock.rectTransform.rotation = quaternion.Euler(new Vector3(0, 0, 0));
        }


        if (type == AimingType.NotUsingWeapon)
        {
            LeanTween.value(gameObject, (v) =>
           {
               ReticleBlock.rectTransform.sizeDelta = v;
           }, ReticleBlock.rectTransform.sizeDelta, new Vector2(200, 200), 0.125f);
            ReticleBlock.rectTransform.rotation = quaternion.Euler(new Vector3(0, 0, 40));
        }
    }

    public void HandlePressToActivateDialog(string text)
    {
        if (text == "")
        {
             PressToActivateDialogText.text = "";
            PressToActivateDialog.gameObject.SetActive(false);
        }
        else
        {
            PressToActivateDialog.gameObject.SetActive(true);
            PressToActivateDialogText.text = text;
        }
    }
}
