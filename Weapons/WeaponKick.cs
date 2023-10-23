using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponKick : MonoBehaviour
{

    public Vector3 startingLocation;
    public Transform WeaponHoldingConstraint;
    private Vector3 startingConstraintLocation;

    // Start is called before the first frame update
    void Start()
    {
        startingLocation = transform.localPosition;
        startingConstraintLocation = WeaponHoldingConstraint.transform.localPosition;
    }

    public void Kick(float amt)
    {

        // if (LeanTween.isTweening(gameObject))
        // {
        //     LeanTween.cancel(gameObject);
        // }


        transform.localPosition -= new Vector3(0, 0, amt / 10);
        WeaponHoldingConstraint.transform.localPosition -= new Vector3(0, 0, amt / 10);
        LeanTween.value(gameObject, (v) =>
        {
            transform.localPosition = v;
        }, transform.localPosition, startingLocation, 0.25f);

        LeanTween.value(gameObject, (v) =>
       {
           WeaponHoldingConstraint.localPosition = v;
       }, WeaponHoldingConstraint.localPosition, startingConstraintLocation, 0.25f);
    }
}
