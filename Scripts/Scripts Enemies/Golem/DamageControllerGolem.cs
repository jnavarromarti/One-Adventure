using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageControllerGolem : MonoBehaviour
{
    Animator animator;
    EnemyGeneralScript enemy;
    IAGolem golem;
    Rigidbody2D rdb2D;

    void Awake()
    {
        animator = GetComponentInParent<Animator>();
        enemy = GetComponentInParent<EnemyGeneralScript>();
        golem = GetComponentInParent<IAGolem>();
        rdb2D = GetComponentInParent<Rigidbody2D>();
    }

    public void GolemDamaged()
    {
        Vector2 knockback = new Vector2(-1f * (golem.isFacingRight ? 1 : -1), 1f);
        rdb2D.AddForce(knockback * enemy.knockbackForceX, ForceMode2D.Impulse);
        StartCoroutine(Damaged());
    }
    public void TakeDamage(int damage)
    {
        enemy.enemyHealth -= damage;
        animator.SetBool("GolemH", true);
    }
    public void HandleDeath()
    {
        if (enemy.enemyHealth <= 0)
        {
            StartCoroutine(Death());
        }
    }
    public IEnumerator Damaged()
    {
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("GolemH", false);
    }
    public IEnumerator Death()
    {
        animator.SetBool("GolemD", true);
        Debug.Log("Enemy killed!");
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}

