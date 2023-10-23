using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class CharacterIKFunctions : MonoBehaviour
{
    public Rig rig;

    public GameObject WeaponsParent;

    public GameObject LookAtTargetTransform;
    public GameObject HandTransform;

    private Vector3 loc = Vector3.zero;

    public Vector3 LocalRotationWeaponHand = new Vector3(0, 90, -90);

    public bool RigActive
    {
        get
        {
            return rig.weight == 1;
        }
    }

    void Start()
    {
        loc = WeaponsParent.transform.localPosition;
    }

    public void TurnOffWeaponHoldConstraintBothHands()
    {

        if (rig.weight == 0)
            return;

        // rig.weight = 0;
        LeanTween.value(gameObject, (v) =>
        {
            rig.weight = v;
            SetWeaponsParentToLookAtTarget(false, v);
        }, rig.weight, 0, 0.125f);

        // LeanTween.delayedCall(1f, () =>
        // {
        //     SetWeaponsParentToLookAtTarget(true, 1);

        //     // WeaponsParent.transform.parent = LookAtTargetTransform.transform;
        //     // WeaponsParent.transform.localPosition = loc;
        //     // WeaponsParent.transform.localRotation = Quaternion.Euler(Vector3.zero);
        // });

    }

    public void TurnOnWeaponHoldConstraintBothHands()
    {

        if (rig.weight == 1)
            return;

        LeanTween.value(gameObject, (v) =>
       {
           rig.weight = v;
           SetWeaponsParentToLookAtTarget(true, v);
       }, rig.weight, 1, 0.125f);


    }

    /// <summary>
    /// Set the weapons parent to the look target or to the hand transform
    /// </summary>
    /// <param name="active"></param> <summary>
    /// 
    /// </summary>
    /// <param name="active"></param>
    public void SetWeaponsParentToLookAtTarget(bool active, float weight = -1)
    {

        if (weight != -1)
        {
            rig.weight = weight;
        }
        else
        {
            rig.weight = weight;
        }

        if (active)
        {
            WeaponsParent.transform.parent = LookAtTargetTransform.transform;
            WeaponsParent.transform.localPosition = loc;
            WeaponsParent.transform.localRotation = Quaternion.Euler(Vector3.zero);
        }
        else
        {
            // WeaponsParent.transform.localPosition = Vector3.zero;
            // WeaponsParent.transform.localRotation = Quaternion.Euler(LocalRotationWeaponHand);
            WeaponsParent.transform.parent = HandTransform.transform;
        }
    }
}
