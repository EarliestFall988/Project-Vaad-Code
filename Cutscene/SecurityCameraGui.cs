using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

public class SecurityCameraGui : MonoBehaviour
{

    public GameObject GroundSec;
    public GameObject ShipSec1;
    public GameObject ShipSec2;

    public TMPro.TMP_Text SecurityCameraText;

    public GameObject Volume;
    public GameObject Volume2;

    public GameObject Volume3;

    public AudioSource ShipDownSource;
    public AudioSource ShipUpSource;
    public GameObject SpaceShipEngine;
    public GameObject SpaceShipEngine2;

    public float AudioVolume = 0.5f;

    public GameObject SecurityCameraUI;
    public List<GameObject> CutsceneObj = new List<GameObject>();

    public float destroyTime = 8;
    public float audioSwitchTime = 9;

    public GameObject Character;

    public UnityEvent OnCutsceneComplete = new UnityEvent();

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Destroy());
        StartCoroutine(SwitchAudio());
        ShipDownSource.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (ShipSec2.gameObject.activeSelf)
        {
            SecurityCameraText.text = "Flight Path Forward";
            Volume.gameObject.SetActive(true);
            Volume2.gameObject.SetActive(false);
            Volume3.gameObject.SetActive(false);

            AudioVolume = 1;
            SpaceShipEngine.gameObject.SetActive(true);
            SpaceShipEngine2.gameObject.SetActive(true);

        }
        else if (ShipSec1.gameObject.activeSelf)
        {
            SecurityCameraText.text = "Sp - 234 to Ground";
            Volume2.gameObject.SetActive(true);
            Volume.gameObject.SetActive(false);
            Volume3.gameObject.SetActive(false);

            AudioVolume = 1;
            SpaceShipEngine.gameObject.SetActive(true);
            SpaceShipEngine2.gameObject.SetActive(false);
        }
        else
        {
            SecurityCameraText.text = "GND CMDR Security North Eye";
            Volume3.gameObject.SetActive(true);
            Volume.gameObject.SetActive(false);
            Volume2.gameObject.SetActive(false);

            AudioVolume = 0.5f;
            SpaceShipEngine.gameObject.SetActive(false);
            SpaceShipEngine2.gameObject.SetActive(false);
        }


        ShipUpSource.volume = AudioVolume;
        ShipDownSource.volume = AudioVolume;

    }


    IEnumerator SwitchAudio()
    {
        yield return new WaitForSeconds(audioSwitchTime);
        ShipUpSource.gameObject.SetActive(true);
        ShipUpSource.timeSamples = ShipDownSource.timeSamples - 4;
        ShipDownSource.gameObject.SetActive(false);
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(destroyTime);

        foreach (var x in CutsceneObj)
        {
            Destroy(x);
        }

        Destroy(Volume3);
        Destroy(Volume2);
        Destroy(Volume);
        Destroy(SecurityCameraUI);
        Character.gameObject.SetActive(true);
        GroundSec.gameObject.SetActive(false);
        gameObject.SetActive(false);
        OnCutsceneComplete?.Invoke();
    }
}
