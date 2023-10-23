using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WorldActivateCharacterHandler : MonoBehaviour
{
    public Camera Camera;
    public Transform Player;
    public float maxActivationDistance = 5;
    public UnityEvent<string> OnTriggerOption = new UnityEvent<string>();

    private string currentTriggerOption = "";


    void Update()
    {
        RaycastHit hit;

        Debug.DrawRay(Camera.transform.position, Camera.transform.forward * maxActivationDistance, Color.red);

        if (Physics.Raycast(Camera.transform.position, Camera.transform.forward, out hit, Mathf.Infinity))
        {

            Vector3 distance = hit.collider.gameObject.transform.position - Player.position;

            if (distance.magnitude > maxActivationDistance)
            {
                if (currentTriggerOption != "")
                    OnTriggerOption?.Invoke("");
                return;
            }

            var trigger = hit.collider.gameObject.GetComponent<ActivateTrigger>();

            if (trigger != null)
            {
                OnTriggerOption?.Invoke("Press F to " + trigger.TriggerName);

                if (Input.GetKeyDown(KeyCode.F))
                {
                    trigger.Activate();
                }

                currentTriggerOption = trigger.TriggerName;
            }
            else
            {
                if (currentTriggerOption != "")
                    OnTriggerOption?.Invoke("");
            }
        }
    }
}
