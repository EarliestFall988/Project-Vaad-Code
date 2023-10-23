using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldItem : MonoBehaviour
{
    /// <summary>
    /// the world item properties
    /// </summary>
    public WorldItemProperties Properties;

    /// <summary>
    /// pickup the item
    /// </summary>
    public void Pickup()
    {
        if (Properties.HP > 0)
        {
            CharacterEventBus.main.health.Heal(Properties.HP);
        }

        if (Properties.Experience > 0)
        {
            PlayerProgression.main.AddExperience(Properties.Experience);
        }

        if (Properties.Ammo > 0)
        {
            CharacterEventBus.main.AmmoPickup(Properties.Ammo); // need to think about this a bit more...
        }

        Destroy(gameObject);
    }
}
