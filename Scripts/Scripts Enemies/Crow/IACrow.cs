using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IACrow : MonoBehaviour
{
    // Esta es una variacion de la IA de persecucion para el enemigo Crow, una vez detecta al heroe de dirige hacia el hasta llegar a cierta distancia donde se lanzara hacia el heroe.
    [SerializeField] private Transform hero;
    [SerializeField] private float distanceLimit;
    [SerializeField] private Rigidbody2D rdb2D;
    public Animator animator;
    EnemyGeneralScript enemy;
    public bool isFacingRight = true;
    GroundCheck groundCheck;

    void Start()
    {
        enemy = GetComponent<EnemyGeneralScript>();
        rdb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        groundCheck = GetComponent<GroundCheck>();
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, hero.position) > distanceLimit)
        {
            transform.position = Vector2.MoveTowards(transform.position, hero.position, enemy.enemySpeed * Time.deltaTime);
        }
        else
        {
            LimitDistance();
        }
        bool isHeroRight = hero.position.x > transform.position.x;
        Flip(isHeroRight);
    }
    private void LimitDistance(){
        bool isGrounded = groundCheck.isGrounded;
        if (isGrounded)
        {
            rdb2D.velocity =new Vector2(rdb2D.velocity.x , 0);
        }
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
}

