using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathRigController : MonoBehaviour
{

    public Transform Player;
    public GameObject Ragdoll;


    public void SetOnDeath()
    {
        transform.position = Player.position;
        Ragdoll.transform.position = Player.position;
        Ragdoll.transform.rotation = Player.rotation;

        Ragdoll.SetActive(true);
    }
}
