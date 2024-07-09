using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    [SerializeField] public Transform[] waypoints;
    [SerializeField] public int currentPoint;
    [SerializeField] public float speed;
    [SerializeField] public bool isWaiting;
    [SerializeField] public float waitTime;
    bool moveToA;
    bool moveToB;

    private void Update()
    {
        MovePlatform();
    }
    private void MovePlatform()
    {
        if (transform.position != waypoints[currentPoint].position)
        {
            transform.position = Vector3.MoveTowards(transform.position, waypoints[currentPoint].position, speed * Time.deltaTime);
        }
        else if (!isWaiting)
        {
            StartCoroutine(Wait());
        }
    }
        IEnumerator Wait()
        {
            isWaiting = true;
            yield return new WaitForSeconds(waitTime);
            currentPoint++;
            if (currentPoint >= waypoints.Length)
            {
                currentPoint = 0;
            }
            isWaiting = false;
        }
    }
