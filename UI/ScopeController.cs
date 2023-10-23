using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScopeController : MonoBehaviour
{

    public Image Scope1;

    ///event to set the scope
    public void SetScope(AimingType type)
    {

        // Debug.Log("Received " + type);

        if (type != AimingType.Scope)
        {
            Scope1.enabled = false;
        }
        else
        {
            Scope1.enabled = true;
        }
    }
}
