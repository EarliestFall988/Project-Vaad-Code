using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterChangesController : MonoBehaviour
{

    public string cosmeticStringID = "player_cosmetic";
    private Cosmetic cosmetic;

    public CharacterCreatorController BodyController;
    public CharacterCreatorController HeadController;

    public CharacterCreatorController AttachmentController;

    public bool allowCharacterChanges = true;

    public void Start()
    {
        LoadCosmetic();
        if (cosmetic != null)
            SetCosmetic(cosmetic);
        else
        {
            Debug.LogWarning("cosmetic is null, creating new one.");
            cosmetic = new Cosmetic();
        }

    }

    private void Update()
    {


        if(allowCharacterChanges != true && cosmetic.BodyId != null && cosmetic.BodyId != "")
        {
            return;
        }

        var item = BodyController.items[BodyController.currentIndex];
        var item2 = HeadController.items[HeadController.currentIndex];
        var item3 = AttachmentController.items[AttachmentController.currentIndex];

        cosmetic.BodyId = item.id;
        cosmetic.HeadId = item2.id;
        cosmetic.AttachmentId = item3.id;
    }

    /// <summary>
    /// Save the cosmetic to the player prefs
    /// </summary>
    /// <returns></returns>
    public void SaveChanges()
    {
        if (cosmetic == null)
            throw new System.Exception("Cosmetic is null");

        PlayerPrefs.SetString(cosmeticStringID, JsonUtility.ToJson(cosmetic));
        PlayerPrefs.Save();

        // Debug.Log("saved changes");
    }

    /// <summary>
    /// Load the cosmetic from the player prefs
    /// </summary>
    /// <returns></returns>
    public void LoadCosmetic()
    {
        var cosmeticString = PlayerPrefs.GetString(cosmeticStringID);


        if (cosmeticString == "")
            return;

        try
        {
            var cosmeticValues = JsonUtility.FromJson<Cosmetic>(cosmeticString);
            cosmetic = cosmeticValues;
        }
        catch (Exception ex)
        {
            Debug.LogError(ex);
        }
    }

    /// <summary>
    /// Set the cosmetic and return true if it was successful
    /// </summary>
    /// <param name="cosmetic"></param>
    /// <returns></returns>
    public bool SetCosmetic(Cosmetic cosmetic)
    {
        this.cosmetic = cosmetic;

        if (cosmetic == null)
            return false;

        if (BodyController == null)
            throw new System.Exception("BodyController is null");

        BodyController.SetId(cosmetic.BodyId);

        if (HeadController == null)
            throw new System.Exception("HeadController is null");

        HeadController.SetId(cosmetic.HeadId);

        if (AttachmentController == null)
            throw new System.Exception("AttachmentController is null");

        AttachmentController.SetId(cosmetic.AttachmentId);

        return true;
    }
}
