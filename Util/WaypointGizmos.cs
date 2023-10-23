using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointGizmos : MonoBehaviour
{

    public List<Transform> waypoints;

    void OnDrawGizmos()
    {

        if (waypoints == null || waypoints.Count == 0)
        {
            return;
        }

        Gizmos.color = Color.green;
        foreach (Transform waypoint in waypoints)
        {
            Gizmos.DrawSphere(waypoint.position, 0.5f);
        }

        int i = 0;
        Gizmos.color = Color.blue;
        foreach (Transform waypoint in waypoints)
        {
            if (i < waypoints.Count - 1)
            {
                Gizmos.DrawLine(waypoint.position, waypoints[i + 1].position);
            }
            else
            {
                Gizmos.DrawLine(waypoint.position, waypoints[0].position);
            }
            i++;
        }
    }
}
