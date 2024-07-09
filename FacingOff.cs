using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacingOff : MonoBehaviour
{
    PatrolAI patrolAI;
    public Transform[] patrolPoints;
    public Transform currentPoint;

    void Start()
    {
        patrolPoints = transform.parent.GetComponent<PatrolAI>().patrolPoints;
    }
    void Update()
    {
        FindClosestPoint();
        FlipToClosestPoint();
    }
    void FindClosestPoint()
    {
        float distanceToClosestPoint = Mathf.Infinity;
        Transform closestPoint = null;
        foreach (Transform point in patrolPoints)
        {
            float distanceToPoint = (point.position - this.transform.position).sqrMagnitude;
            if (distanceToPoint < distanceToClosestPoint)
            {
                distanceToClosestPoint = distanceToPoint;
                closestPoint = point;
            }
        }
        currentPoint = closestPoint;
    }
    void FlipToClosestPoint()
    {

        if (transform.position.x < currentPoint.position.x)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else if (transform.position.x > currentPoint.position.x)
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, currentPoint.position);
    }
}
