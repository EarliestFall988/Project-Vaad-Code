using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{

    public int AmmoAmount = 10;
    public string AmmoType = "Primary"; // Primary or Special
    public GameObject Parent;

    void Start()
    {
        if (Parent == null)
        {
            Debug.Log("Ammo Pickup has no parent");
        }
    }

    void OnTriggerEnter(Collider col)
    {

        // Debug.Log(col.gameObject.name + " has entered the ammo pickup trigger - tag is: " + col.gameObject.tag + "");

        if (col.gameObject.tag == "Player")
        {

            Debug.Log(col.gameObject.name + " has entered the ammo pickup trigger - tag is: " + col.gameObject.tag + "");

            CharacterEventBus characterEventBus = col.gameObject.GetComponent<CharacterEventBus>();
            if (characterEventBus != null)
            {
                int leftOver = characterEventBus.AmmoPickup(this);
                AmmoAmount -= leftOver;

                if (AmmoAmount <= 0)
                {
                    if (Parent != null)
                        Destroy(Parent);
                    else
                        Destroy(gameObject);
                    return;
                }
            }
        }
    }
}
