using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider))]
public class KillVolume : MonoBehaviour
{

    BoxCollider collider;
    public bool PlayerOnly = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    void OnTriggerEnter(Collider col)
    {

        if (PlayerOnly && !col.gameObject.CompareTag("Player"))
        {
            return;
        }

        var health = col.gameObject.GetComponent<Health>();

        if (health != null)
        {
            health.Kill();
        }
    }


    void OnDrawGizmos()
    {
        Gizmos.matrix = this.transform.localToWorldMatrix;
        Gizmos.color = Color.red;
        collider = GetComponent<BoxCollider>();
        var size = collider.size;

        Gizmos.DrawIcon(transform.position, "skull icon.png", true);

        Gizmos.DrawWireCube(Vector3.zero, size);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.matrix = this.transform.localToWorldMatrix;
        Gizmos.color = Color.red;
        collider = GetComponent<BoxCollider>();
        var size = collider.size;

        Gizmos.DrawCube(Vector3.zero, size);
    }
}
