using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtBox : MonoBehaviour
{
    public float Damage;
    public bool InstantKill = false;
    private List<Collider> colliders = new List<Collider>();

    private void Update()
    {
        foreach (Collider c in colliders)
        {
            if (c != null && c.gameObject.tag == "Player")
            {

                // Debug.Log("found object colliding with" + c.gameObject.name);

                c.gameObject.GetComponent<Health>().TakeDamage(Mathf.Abs(Damage) * Time.deltaTime);
                if (InstantKill)
                {
                    c.gameObject.GetComponent<Health>().Kill();
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
