using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreenController : MonoBehaviour
{

    public List<string> Tips = new List<string>();
    public TMPro.TMP_Text TipsText;

    void Start()
    {
        var tip = Tips[Random.Range(0, Tips.Count)];
        TipsText.text = tip;
    }
}
