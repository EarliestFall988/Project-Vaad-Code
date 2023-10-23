using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CharacterItem : MonoBehaviour
{

    public string itemName;
    public string type = "";
    public string id = "";

    public string description = "";

    public List<GameObject> items = new List<GameObject>();
    public List<GameObject> ItemsToHide = new List<GameObject>();

    public void SetActive(bool active)
    {

        if (id == "")
        {
            throw new Exception("Item id is empty");
        }

        foreach (var item in items)
        {
            if (item == null)
                continue;

            item.SetActive(active);
        }

        foreach (var x in ItemsToHide)
        {
            if (x == null)
                continue;

            x.SetActive(!active);
        }
    }
}
