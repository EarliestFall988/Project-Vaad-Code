using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxVolumes : MonoBehaviour
{

    public Health health;

    public void Damage(float amt)
    {
        health.TakeDamage(Mathf.Abs(amt));
    }
}
