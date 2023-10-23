using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class LookAtTarget : MonoBehaviour
{
    public Transform Target;
    public Transform Forward;

    public float limits;
    public bool useLimits;

    public Vector3 defaultDirectionOffset;
    public bool debug;

    public float min;
    public float max;

    public WeaponsManager weaponsManager;


    private void Start()
    {
        defaultDirectionOffset = transform.forward;
    }

    void Update()
    {

        Vector3 forward = Forward.forward; //Forward.TransformDirection(transform.forward);
        Vector3 toOther = Target.position - transform.position;
        // Vector3 direction = Target.position - transform.position;

        var dot = Vector3.Dot(forward, toOther);

        Vector3 direction = Target.position - transform.position;

        direction = new Vector3(direction.x, 0, direction.z);


        var signedAngle = Vector3.Angle(new Vector3(forward.x, 0, forward.z), direction);

        if (debug)
            // Debug.Log("Signed Angle: " + signedAngle);

            if (debug)
                Debug.Log("Look at forward: " + transform.localRotation.eulerAngles.y);

        if (signedAngle > max && useLimits)
        {
            weaponsManager.GoodFireAngle = false;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(forward), 0.2f);
            return;
        }

        if ((signedAngle < min) && useLimits)
        {

            weaponsManager.GoodFireAngle = false;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(forward), 0.2f);
            return;

        }

        weaponsManager.GoodFireAngle = true;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(toOther), 0.2f);

    }

}
