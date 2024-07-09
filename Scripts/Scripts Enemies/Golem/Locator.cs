using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locator : MonoBehaviour
{
    public Transform[] patrolPoints;
    public Transform targetWaypoint;
    float distance;
    float minDistance;
    void Update()
    {
        FindClosestWaypoint();
        OrientateTarget();
    }
    void FindClosestWaypoint()
    {
        minDistance = Mathf.Infinity;
        Transform closestWaypoint = null;
        foreach (Transform waypoint in patrolPoints)
        {
            distance = Vector3.Distance(transform.position, waypoint.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestWaypoint = waypoint;
            }
        }
        targetWaypoint = closestWaypoint;
    }
    void OrientateTarget()
    {
        if (targetWaypoint != null)
        {
            Vector3 directionTarget = targetWaypoint.position - transform.position;
            if (directionTarget.x > 0)
            {
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            }
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, targetWaypoint.position);
    }
}
