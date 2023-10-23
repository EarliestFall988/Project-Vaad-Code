using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Health Settings", menuName = "New/Health Settings")]
public class HealthSettings : ScriptableObject
{

    [Header("Health")]
    public float HealthPoints = 75;
    public float MaxHealth = 100;

    [Header("Shields")]
    public float ShieldPoints = 20;
    public float MaxShields = 40;
    public float ShieldRechargeDelay = 5;
    public float ShieldRechargeRate = 5;
}
