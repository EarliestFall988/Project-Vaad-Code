using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class RaycastBullet : IBullet
{

    public AudioImpactController audioImpactController;

    public List<string> tagsToIgnore = new List<string>();

    protected override void EjectBullet(Vector3 spread, float damage, float range)
    {
        FireRay(spread, damage, range);
    }

    /// <summary>
    /// Fire a ray from the fire point
    /// </summary>
    private void FireRay(Vector3 spread, float damage, float range)
    {
        RaycastHit hit;

        // Debug.DrawLine(firePoint.position, (transform.forward * range) + firePoint.position, Color.red, 1f);


        if (Physics.Raycast(firePoint.position, spread, out hit, range))
        {
            Debug.DrawRay(firePoint.position, firePoint.forward * range, Color.green, 1f);

            // Debug.Log("Hit: " + hit.transform.GetComponents().Length);
            if (hit.transform != null)
            {
                // Debug.Log("Hit: " + hit.transform.name + " " + hit.point.ToString());

                Health health = hit.transform.GetComponent<Health>();
                HitBoxVolumes hitVol = hit.transform.GetComponent<HitBoxVolumes>();

                Vector3 hitPoint = hit.point;

                // if (hit.transform.GetComponent<Collider>() != null)
                // {
                //     hitPoint = hit.transform.GetComponent<Collider>().ClosestPointOnBounds(firePoint.position);
                // }

                if (hit.transform.GetComponent<Rigidbody>() != null)
                {
                    hit.transform.GetComponent<Rigidbody>().AddForceAtPosition(transform.forward * 10, hitPoint, ForceMode.Impulse);
                }

                if (audioImpactController != null)
                {
                    var tag = "Metal";
                    var obj = hit.transform.GetComponent<MaterialData>();
                    if (obj != null)
                    {
                        tag = obj.MaterialName;
                    }

                    if (tag == "Mud" || tag == "Snow" || tag == "Grass")
                    {
                        tag = "Dirt";
                    }

                    audioImpactController.PlayImpact(tag, hitPoint, hit.normal);
                }

                if (health != null)
                {
                    // Debug.Log("Health: " + (health != null).ToString());
                    // Debug.Log("Damage: " + health.HealthPoints.ToString());

                    // Debug.Log("damage: " + damage.ToString());

                    health.TakeDamage(damage);
                }

                if (hitVol != null)
                {
                    hitVol.Damage(damage);
                }
            }
            else
            {
                Debug.Log("Hit: something but the transform was null");
            }
        }
        else
        {
            Debug.DrawRay(firePoint.position, firePoint.forward * range, Color.red, 1f);

            Debug.Log("Hit: Nothing");
        }
    }
}
