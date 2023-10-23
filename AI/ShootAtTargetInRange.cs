using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAtTargetInRange : MonoBehaviour
{
  
    public List<GameObject> targets = new List<GameObject>();

    [SerializeField]
    private float fireDistance = 10;
    [SerializeField]
    private float lookDistance = 15;

    [SerializeField]
    private string context = "idle";

    public Transform LookAtTargetTransform;

    void Update()
    {
        Debug.Log("context: " + context);
        LookAtTarget();
        FireAtTarget();
    }

    void FireAtTarget()
    {

        if (context != "noticed" && context != "firing")
            return;

        if (targets.Count > 0)
        {
            foreach (GameObject target in targets)
            {
                if (target != null)
                {
                    if (Vector3.Distance(transform.position, target.transform.position + Vector3.up) < fireDistance)
                    {
                        // transform.LookAt(target.transform);
                        Debug.DrawRay(transform.position, transform.forward * 10, Color.red);
                        context = "firing";
                    }
                }
            }
        }
    }

    void LookAtTarget()
    {
        if (targets.Count > 0)
        {
            foreach (GameObject target in targets)
            {
                if (target != null)
                {
                    if (Vector3.Distance(transform.position, target.transform.position + Vector3.up) < lookDistance)
                    {

                        LookAtTargetTransform.LookAt(target.transform.position + Vector3.up);

                        Debug.DrawRay(LookAtTargetTransform.position, LookAtTargetTransform.forward * 10, Color.green);

                        Ray ray = new Ray(LookAtTargetTransform.position, LookAtTargetTransform.forward);
                        RaycastHit hit;

                        if (Physics.Raycast(ray, out hit, lookDistance))
                        {
                            if (hit.collider.gameObject == target)
                            {
                                transform.LookAt(target.transform.position + Vector3.up);
                                Debug.DrawRay(transform.position, transform.forward * 10, Color.yellow);
                                context = "noticed";
                                return;
                            }
                        }
                    }
                }
            }
        }

        context = "idle";
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, fireDistance);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, lookDistance);
    }
}
