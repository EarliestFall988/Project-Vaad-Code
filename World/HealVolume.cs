using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealVolume : MonoBehaviour
{
    public float HealthPerSecond;
    public bool MaxHealth = false;
    private List<Collider> colliders = new List<Collider>();

    private void Update()
    {
        foreach (Collider c in colliders)
        {
            if (c != null && c.gameObject.tag == "Player")
            {

                Debug.Log("found object colliding with" + c.gameObject.name);
                c.gameObject.GetComponent<Health>().Heal(HealthPerSecond * Time.deltaTime);

                if (MaxHealth)
                {
                    c.gameObject.GetComponent<Health>().SetMaxHealth();
                }
            }
            else
            {
                colliders.Remove(c);
                return;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            colliders.Add(other);
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            colliders.Remove(other);
        }
    }
}
