using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class CharacterCreatorController : MonoBehaviour
{

    public int currentIndex = 0;
    public List<CharacterItem> items = new List<CharacterItem>();

    public string type;

    public Cosmetic cosmetic;
    public Button NextButton;
    public Button BackButton;
    public TMPro.TMP_Text DescriptionText;


    void OnDisable()
    {
        foreach (var item in items)
        {
            item.SetActive(false);
        }

        if (NextButton != null && BackButton != null)
        {
            NextButton.interactable = false;
            BackButton.interactable = false;
        }

    }

    void OnEnable()
    {
        Toggle();
        if (NextButton != null && BackButton != null)
        {
            NextButton.interactable = true;
            BackButton.interactable = true;
        }
    }

    public void Toggle()
    {
        Next();
        Back();
    }

    public void SetId(string id)
    {

        items[currentIndex].SetActive(false);

        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].id == id)
            {
                currentIndex = i;
                items[currentIndex].SetActive(true);
                return;
            }
        }

        if (DescriptionText != null)
            DescriptionText.text = items[currentIndex].description;
    }

    public string GetName()
    {
        return items[currentIndex].itemName;
    }

    public void Next()
    {

        items[currentIndex].SetActive(false);
        currentIndex++;

        if (currentIndex > items.Count - 1)
        {
            currentIndex = 0;
        }

        items[currentIndex].SetActive(true);

        if (DescriptionText != null)
            DescriptionText.text = items[currentIndex].description;
    }

    public void Back()
    {

        items[currentIndex].SetActive(false);
        currentIndex--;

        if (currentIndex < 0)
        {
            currentIndex = items.Count - 1;
        }

        items[currentIndex].SetActive(true);

        if (DescriptionText != null)
            DescriptionText.text = items[currentIndex].description;
    }
}
