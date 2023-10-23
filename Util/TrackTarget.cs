using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackTarget : MonoBehaviour
{

    public Transform Target;

    private Vector3 offset = new Vector3(0, 0, 0);
    private Quaternion offsetRot = Quaternion.identity;

    // Start is called before the first frame update
    void Start()
    {
        // offset = transform.position - transform.parent.position;
        // offsetRot = transform.rotation * Quaternion.Inverse(transform.parent.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Target.position;
        transform.rotation = Target.rotation;
    }
}
