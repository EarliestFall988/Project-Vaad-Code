using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ActivateTrigger : MonoBehaviour
{

    public string TriggerName;

    public UnityEvent TriggerEvent = new UnityEvent();

    public AudioSource Source;
    public AudioClip clip;


    public void Activate()
    {
        TriggerEvent?.Invoke();
        Source.PlayOneShot(clip);
    }
}
