using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PatrolBehavior : MonoBehaviour
{
    public List<Transform> waypoints;
    public float speed = 1.0f;
    public Transform CurrentWaypoint;
    private Transform currentWaypointCompare;

    private int waypointIndex = 0;

    NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        SetClosestWaypoint();
    }

    void Update()
    {
        if (agent.remainingDistance < 0.5f)
        {
            waypointIndex++;
            if (waypointIndex >= waypoints.Count)
            {
                waypointIndex = 0;
            }
            CurrentWaypoint = waypoints[waypointIndex];
        }

        if (currentWaypointCompare != CurrentWaypoint)
        {
            agent.SetDestination(CurrentWaypoint.position);
            currentWaypointCompare = CurrentWaypoint;
        }
    }


    void SetClosestWaypoint()
    {
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;

        int index = 0;

        foreach (Transform waypoint in waypoints)
        {
            Vector3 diff = waypoint.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                CurrentWaypoint = waypoint;
                distance = curDistance;
                waypointIndex = index;
            }

            index++;
        }
    }
}
