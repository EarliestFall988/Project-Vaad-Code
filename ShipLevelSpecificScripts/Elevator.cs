using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Elevator : MonoBehaviour
{

    public Image BlackoutImage;

    public GameObject Character;

    public List<Transform> ElevatorCharacterSpawnLocations = new List<Transform>();


    public void GoToLevel(ElevatorLevels level)
    {
        StartCoroutine(Blackout(level));
    }


    IEnumerator Blackout(ElevatorLevels level)
    {
        BlackoutImage.gameObject.SetActive(true);

        yield return new WaitForSeconds(1f);

        Character.transform.position = ElevatorCharacterSpawnLocations[(int)level].position;
        Character.transform.rotation = ElevatorCharacterSpawnLocations[(int)level].rotation;

        BlackoutImage.gameObject.SetActive(false);
    }
}

public enum ElevatorLevels
{
    Upper,
    Lower
}
