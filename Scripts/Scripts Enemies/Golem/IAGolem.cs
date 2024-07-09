using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAGolem : MonoBehaviour
{
    //Version para el golem de la IA de persecucion, una vez el heroe entra en la zona del collider el Golem se desplaza hacia el a no ser que el heroe deje de estar en el collider.
    [SerializeField] private Transform hero;
    [SerializeField] private float distanceLimit;
    [SerializeField] private Rigidbody2D rdb2D;
    public Animator animator;
    EnemyGeneralScript enemy;
    [SerializeField] public bool isAttacking;
    public bool isFacingRight = true;
    GroundCheck groundCheck;
    public bool isHitted;



    void Start()
    {
        enemy = GetComponent<EnemyGeneralScript>();
        rdb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        groundCheck = GetComponent<GroundCheck>();
        isHitted = false;
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, hero.position) > distanceLimit)
        {
            transform.position = Vector2.MoveTowards(transform.position, hero.position, enemy.enemySpeed * Time.deltaTime);
            animator.SetBool("GolemW", true);
        }
        else
        {
            HandleAttack();
        }
        bool isHeroRight = hero.position.x > transform.position.x;
        Flip(isHeroRight);
    }
    private void HandleAttack()
    {
        animator.SetBool("GolemA", true);
        StartCoroutine(Attack());
    }
    private void Flip(bool isHeroRight)
    {
        if (isHeroRight && !isFacingRight || !isHeroRight && isFacingRight)
        {
            isFacingRight = !isFacingRight;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }
    IEnumerator Attack()
    {
        yield return new WaitForSeconds(1.1f);
        animator.SetBool("GolemA", false);
    }
}
