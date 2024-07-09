using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IASlime : MonoBehaviour
{
    [SerializeField] private Transform hero;
    [SerializeField] private float distanceLimit;
    [SerializeField] private Rigidbody2D rdb2D;
    public Animator animator;
    EnemyGeneralScript enemy;
    public bool isFacingRight = true;
    private float visionRadius;
    private float attackRadius;
    [SerializeField] public bool isAttacking;
    private Vector2 directionAttack;
    GroundCheck groundCheck;
    public bool isHitted;



    void Start()
    {
        enemy = GetComponent<EnemyGeneralScript>();
        rdb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        groundCheck = GetComponentInChildren<GroundCheck>();

    }

    void Update()
    {
        if (Vector2.Distance(transform.position, hero.position) > distanceLimit)
        {
            transform.position = Vector2.MoveTowards(transform.position, hero.position, enemy.enemySpeed * Time.deltaTime);
        }
        else
        {
            Jump();
        }
        bool isHeroRight = hero.position.x > transform.position.x;
        Flip(isHeroRight);
    }

    private void Jump()
    {
        bool isGrounded = groundCheck.isGrounded;
        if (isGrounded)
        {
            Vector2 directionAttack = (hero.transform.position - transform.position).normalized;
            float jumpVelocity = Mathf.Sqrt(2 * enemy.enemyJumpForce * Mathf.Abs(Physics2D.gravity.y));
            rdb2D.velocity= new Vector2(directionAttack.x* enemy.enemySpeed, jumpVelocity);
        }

        if (rdb2D.velocity.y > 0){
            animator.SetBool("SlimeJ", true);
        }
        else if (rdb2D.velocity.y < 0)
        {
            animator.SetBool("SlimeJOut", true);
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
