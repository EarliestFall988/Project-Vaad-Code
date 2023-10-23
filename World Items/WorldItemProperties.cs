using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New/World Item Properties")]
public class WorldItemProperties : ScriptableObject
{
    public string Name = "Item";
    [TextArea]
    public string Description = "Pickup this item";
    public Sprite Icon;
    public int HP = 0;
    public int Experience = 0;
    public int Ammo = 0;
    public int Weight = 1;
}
