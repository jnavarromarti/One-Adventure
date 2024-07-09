using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolAIGolem : MonoBehaviour
{
    //Es una variacion del patroAI pero adaptado a las animaciones del golem.
    [SerializeField] public Transform[] patrolPoints;
    [SerializeField] EnemyGeneralScript enemy;
    [SerializeField] public int currentPoint;
    [SerializeField] public bool isWaiting;
    Animator animator;

    void Start()
    {
        enemy = GetComponent<EnemyGeneralScript>();
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        if (transform.position != patrolPoints[currentPoint].position)
        {
            transform.position = Vector3.MoveTowards(transform.position, patrolPoints[currentPoint].position, enemy.enemySpeed * Time.deltaTime);
            animator.SetBool("GolemW", true);
        }
        else if (!isWaiting)
        {
            StartCoroutine(Wait());
        }
    }
    IEnumerator Wait()
    {
        animator.SetBool("GolemW", false);
        isWaiting = true;
        yield return new WaitForSeconds(2f); 
        currentPoint++;
        if (currentPoint >= patrolPoints.Length)
        {
            currentPoint = 0;
        }
        isWaiting = false;
        Flip();
    }
    void Flip()
    {
        if (transform.position.x < patrolPoints[currentPoint].position.x)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else if (transform.position.x > patrolPoints[currentPoint].position.x)
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);

        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 0.1f);
    }
}