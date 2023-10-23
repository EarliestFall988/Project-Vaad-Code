using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOrbitTarget : MonoBehaviour
{

    public Transform Target;
    public Camera Camera;

    Vector3 offset = new Vector3(0, 0, 0);

    // Start is called before the first frame update
    void Start()
    {
        offset = Camera.transform.position - Target.position;
        Camera.transform.position = Target.position;
    }

    // Update is called once per frame
    void Update()
    {   
        Camera.transform.position = Vector3.Lerp(Camera.transform.position, Target.position + offset, 0.1f);
        Camera.transform.LookAt(Target);
    }
}
