using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicCoverHandler : MonoBehaviour
{

    public Transform StartPoint;
    public Vector2 CoverAnalyzerSize;

    public float CoverAnalyzerSpreadDistance;

    public float minRayDistance;
    public float maxRayDistance;

    public List<Transform> Targets = new List<Transform>();
    public List<Vector3> CoverPoints = new List<Vector3>();


    void FixedUpdate()
    {
        if (CoverAnalyzerSpreadDistance <= 0.1)
        {
            Debug.LogError("the spread distance must be greater than 0.1");
            return;
        }

        for (float i = 0; i < CoverAnalyzerSize.x; i += CoverAnalyzerSpreadDistance)
        {
            for (float k = 0; k < CoverAnalyzerSize.y; k += CoverAnalyzerSpreadDistance)
            {
                Vector3 pos = new Vector3(StartPoint.position.x + i, StartPoint.position.y, StartPoint.position.z + k);

                RaycastHit hit;

                if (Physics.Raycast(pos, Vector3.down, out hit, maxRayDistance))
                {
                    if (hit.distance > minRayDistance)
                    {
                        RaycastHit hit2;

                        var newPoint = hit.point + new Vector3(0, minRayDistance, 0);
                        newPoint = new Vector3(newPoint.x, newPoint.y / 2, newPoint.z);

                        bool validCoverPoint = true;
                        int l = 0;

                        while (l < Targets.Count && validCoverPoint)
                        {
                            var lookat = Targets[l].transform.position + new Vector3(0, 0.5f, 0) - newPoint;

                            if (Physics.Raycast(newPoint, lookat, out hit2, CoverAnalyzerSize.x + CoverAnalyzerSize.y) && hit2.collider.gameObject == Targets[l].gameObject)
                            {
                                validCoverPoint = false;
                            }

                            l++;
                        }

                        if (validCoverPoint)
                        {
                            CoverPoints.Add(hit.point);
                            Debug.DrawLine(hit.point, newPoint, Color.green);
                        }
                    }
                    else
                    {
                        // Debug.DrawLine(pos, pos + Vector3.down * minRayDistance, Color.red);
                    }
                }
                else
                {
                    // Debug.DrawLine(pos, pos + Vector3.down * maxRayDistance, Color.red);
                }
            }
        }
    }

    public void AddTarget(Transform newTarget)
    {
        Targets.Add(newTarget);
    }


    public void RemoveTarget(Transform target)
    {
        Targets.Remove(target);
    }



    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(StartPoint.position, StartPoint.position + new Vector3(CoverAnalyzerSize.x, 0, 0));
        Gizmos.DrawLine(StartPoint.position, StartPoint.position + new Vector3(0, 0, CoverAnalyzerSize.y));
        Gizmos.DrawLine(StartPoint.position + new Vector3(CoverAnalyzerSize.x, 0, 0), StartPoint.position + new Vector3(CoverAnalyzerSize.x, 0, CoverAnalyzerSize.y));
        Gizmos.DrawLine(StartPoint.position + new Vector3(0, 0, CoverAnalyzerSize.y), StartPoint.position + new Vector3(CoverAnalyzerSize.x, 0, CoverAnalyzerSize.y));
        Gizmos.DrawLine(StartPoint.position, StartPoint.position + new Vector3(0, 0, CoverAnalyzerSize.y));


        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(StartPoint.position + new Vector3(0, -minRayDistance, 0), StartPoint.position + new Vector3(CoverAnalyzerSize.x, -minRayDistance, 0));
        Gizmos.DrawLine(StartPoint.position + new Vector3(0, -minRayDistance, 0), StartPoint.position + new Vector3(0, -minRayDistance, CoverAnalyzerSize.y));
        Gizmos.DrawLine(StartPoint.position + new Vector3(CoverAnalyzerSize.x, -minRayDistance, 0), StartPoint.position + new Vector3(CoverAnalyzerSize.x, -minRayDistance, CoverAnalyzerSize.y));
        Gizmos.DrawLine(StartPoint.position + new Vector3(0, -minRayDistance, CoverAnalyzerSize.y), StartPoint.position + new Vector3(CoverAnalyzerSize.x, -minRayDistance, CoverAnalyzerSize.y));
        Gizmos.DrawLine(StartPoint.position + new Vector3(0, -minRayDistance, 0), StartPoint.position + new Vector3(0, -minRayDistance, CoverAnalyzerSize.y));


        Gizmos.color = Color.blue;
        Gizmos.DrawLine(StartPoint.position + new Vector3(0, -maxRayDistance, 0), StartPoint.position + new Vector3(CoverAnalyzerSize.x, -maxRayDistance, 0));
        Gizmos.DrawLine(StartPoint.position + new Vector3(0, -maxRayDistance, 0), StartPoint.position + new Vector3(0, -maxRayDistance, CoverAnalyzerSize.y));
        Gizmos.DrawLine(StartPoint.position + new Vector3(CoverAnalyzerSize.x, -maxRayDistance, 0), StartPoint.position + new Vector3(CoverAnalyzerSize.x, -maxRayDistance, CoverAnalyzerSize.y));
        Gizmos.DrawLine(StartPoint.position + new Vector3(0, -maxRayDistance, CoverAnalyzerSize.y), StartPoint.position + new Vector3(CoverAnalyzerSize.x, -maxRayDistance, CoverAnalyzerSize.y));
        Gizmos.DrawLine(StartPoint.position + new Vector3(0, -maxRayDistance, 0), StartPoint.position + new Vector3(0, -maxRayDistance, CoverAnalyzerSize.y));
    }
}
