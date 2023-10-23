using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUIController : MonoBehaviour
{

    public Image HealthBarBackground;
    public List<Image> HealthBars = new List<Image>();


    [Range(0, 1)]
    public float healthPercent = 0.5f;

    public Color badHealthColor = Color.red;
    public Color goodHealthColor = Color.green;

    public float flashTime = 0.2f;
    private bool flashing = false;


    [Header("Debug")]
    [Range(0, 1)]
    public float newHealth = 0.5f;
    public bool setHealth = false;

    void Update()
    {

        for (int i = 0; i < HealthBars.Count; i++)
        {
            HealthBars[i].fillAmount = Mathf.Lerp(HealthBars[i].fillAmount, healthPercent, 0.1f);
        }


        if (healthPercent <= 0.25)
        {
            if (!flashing)
                StartCoroutine(FlashingRed());
        }
        else
        {
            if (flashing)
            {
                StopCoroutine(FlashingRed());
                flashing = false;
            }

            HealthBarBackground.color = goodHealthColor;
        }


        if (setHealth)
        {
            setHealth = false;
            SetHealth(newHealth);
        }
    }



    IEnumerator FlashingRed()
    {
        while (true)
        {
            flashing = true;
            HealthBarBackground.color = badHealthColor;
            yield return new WaitForSeconds(flashTime);
            HealthBarBackground.color = goodHealthColor;
            yield return new WaitForSeconds(flashTime);
            yield return null;
        }
    }


    

    public void SetHealth(float percentHealth)
    {
        healthPercent = percentHealth;
    }
}
