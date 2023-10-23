using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class MusicTriggerVolume : MonoBehaviour
{

    public List<AudioSource> audioSourcesToStop = new List<AudioSource>();
    public List<AudioSource> audioSourcesToPlay = new List<AudioSource>();

    BoxCollider collider;


    public bool turnOffAfterTriggerOnce = true;
    private bool triggered = false;


    public void OnTriggerEnter()
    {

        if (turnOffAfterTriggerOnce && triggered)
            return;

        foreach (AudioSource audioSource in audioSourcesToStop)
        {
            audioSource.Stop();
        }
        foreach (AudioSource audioSource in audioSourcesToPlay)
        {
            audioSource.Play();
        }


        triggered = true;

    }

    void OnDrawGizmos()
    {
        Gizmos.matrix = this.transform.localToWorldMatrix;
        Gizmos.color = Color.green;
        collider = GetComponent<BoxCollider>();
        var size = collider.size;

        Gizmos.DrawIcon(transform.position, "drums.png", true);


        Gizmos.DrawWireCube(Vector3.zero, size);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.matrix = this.transform.localToWorldMatrix;
        Gizmos.color = Color.green;
        collider = GetComponent<BoxCollider>();
        var size = collider.size;

        Gizmos.DrawCube(Vector3.zero, size);
    }
}
