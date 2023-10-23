using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;


/// <summary>
/// Player backpack handles the player's inventory (weapons, world items, etc).
/// </summary>
public class Backpack
{

    #region weapons
    private List<GunData> PrimaryWeapons = new List<GunData>();
    private List<GunData> SpecialWeapons = new List<GunData>();

    private List<GunItem> WeaponsInBackpack = new List<GunItem>();

    public Backpack()
    {
        EquipmentController.OnGetGuns += GetGuns;
    }

    /// <summary>
    /// Get the Primary Weapons
    /// </summary>
    /// <returns></returns>
    public GunData[] GetPrimaryWeapons()
    {

        if (PrimaryWeapons.Count == 0)
            GetWeaponsFromPlayer();

        Debug.Log("found weapons: " + PrimaryWeapons.Count);

        return PrimaryWeapons.ToArray();
    }

    /// <summary>
    /// Get the Special Weapons
    /// </summary>
    /// <returns></returns>
    public GunData[] GetSpecialWeapons()
    {
        if (SpecialWeapons.Count == 0)
            GetWeaponsFromPlayer();

        Debug.Log("found weapons: " + SpecialWeapons.Count);

        return SpecialWeapons.ToArray();
    }

    /// <summary>
    /// Add a weapon to the backpack
    /// </summary>
    /// <param name="gun">the gun to add</param>
    /// <param name="location">should it be in the 'primary' or 'secondary' slot?</param>
    public void AddWeapon(Gun gun, string location)
    {
        if (location.Trim().ToLower() == "primary")
        {

            var newData = new GunData
            {
                // Equipped = equipped,
                Location = location,
                Gun = gun
            };

            PrimaryWeapons.Add(newData);
        }
        else if (location.Trim().ToLower() == "special")
        {
            var newData = new GunData
            {
                // Equipped = equipped,
                Location = location,
                Gun = gun
            };


            SpecialWeapons.Add(newData);
        }
    }

    /// <summary>
    /// Remove a weapon from the backpack   
    /// </summary>
    /// <param name="gun"></param>
    public void RemoveWeapon(Gun gun, string location)
    {
        if (location.Trim().ToLower() == "primary")
        {
            var gunData = PrimaryWeapons.GetEnumerator();
            while (gunData.MoveNext())
            {
                if (gunData.Current.Gun == gun)
                {
                    PrimaryWeapons.Remove(gunData.Current);
                    break;
                }
            }
        }
        else if (location.Trim().ToLower() == "special")
        {
            var gunData = SpecialWeapons.GetEnumerator();
            while (gunData.MoveNext())
            {
                if (gunData.Current.Gun == gun)
                {
                    SpecialWeapons.Remove(gunData.Current);
                    break;
                }
            }
        }
    }

    /// <summary>
    /// Clear the backpack
    /// </summary>
    public void Clear()
    {
        PrimaryWeapons.Clear();
        SpecialWeapons.Clear();
    }

    GunData[] GetGuns(string location)
    {


        if (location.Trim().ToLower() == "primary")
        {
            return GetPrimaryWeapons();
        }
        else if (location.Trim().ToLower() == "special")
        {
            return GetSpecialWeapons();
        }
        else
        {
            return new List<GunData>().ToArray();
        }
    }

    private void GetWeaponsFromPlayer()
    {

        if (CharacterEventBus.main == null)
            return;

        if (CharacterEventBus.main.weaponsManager == null)
            return;

        List<Gun> primaryGuns = CharacterEventBus.main.weaponsManager.GetPrimaryWeapons();
        List<Gun> specialGuns = CharacterEventBus.main.weaponsManager.GetSpecialWeapons();

        PrimaryWeapons.Clear();
        PrimaryWeapons.AddRange(primaryGuns.ConvertAll(x => new GunData
        {
            Gun = x,
            Location = "primary"
        }));

        SpecialWeapons.Clear();
        SpecialWeapons.AddRange(specialGuns.ConvertAll(x => new GunData
        {
            Gun = x,
            Location = "special"
        }));


    }

    #endregion


    #region world items

    private int _maximumWeight = 100;
    public int MaximumWeight => _maximumWeight;

    private int _currentWeight = 0;
    public int CurrentWeight => _currentWeight;

    private List<WorldItem> Items = new List<WorldItem>();

    public void AddItem(WorldItem item)
    {

        if (_currentWeight + item.Properties.Weight > _maximumWeight)
        {
            Debug.Log("Backpack is full");
            return;
        }

        _currentWeight += item.Properties.Weight;

        Items.Add(item);
    }

    public void RemoveItem(WorldItem item)
    {
        _currentWeight -= item.Properties.Weight;
        Items.Remove(item);
    }

    #endregion
}

/// <summary>
/// The data structure for a gun in the backpack being used in the world
/// </summary>
[Serializable]
public class GunData
{
    public Gun Gun;
    // public bool Equipped { get; set; }
    public string Location;
}

public class GunItem
{

    public GunSettings Properties;
}
