using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEventBus : MonoBehaviour
{

    public WeaponsManager weaponsManager;
    public Health health;
    public CameraGameplayController cameraGameplayController;

    public static CharacterEventBus main;

    public int AmmoPickup(AmmoPickup source)
    {
        return weaponsManager.AmmoPickup(source);
    }

    public void AmmoPickup(int amount)
    {
        weaponsManager.AmmoPickup(amount);
    }


    void OnDisable()
    {
        main = null;
    }

    void OnEnable()
    {
        main = this;
    }
}
