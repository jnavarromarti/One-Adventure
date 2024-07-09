using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageControllerCrow : MonoBehaviour
{
    //Mismo funcionamiento que el DamageControlSlime, que tambien gestiona sus animaciones y sus caracteristicas definidas en el enemyGeneralScript    
    Animator animator;
    EnemyGeneralScript enemy;
    IACrow crow;
    Rigidbody2D rdb2D;

    void Awake()
    {
        animator = GetComponentInParent<Animator>();
        enemy = GetComponentInParent<EnemyGeneralScript>();
        crow = GetComponentInParent<IACrow>();
        rdb2D = GetComponentInParent<Rigidbody2D>();
    }

    public void GolemDamaged()
    {
        Vector2 knockback = new Vector2(-1f * (crow.isFacingRight ? 1 : -1), 1f);
        rdb2D.AddForce(knockback * enemy.knockbackForceX, ForceMode2D.Impulse);
        StartCoroutine(Damaged());
    }
    public void TakeDamage(int damage)
    {
        enemy.enemyHealth -= damage;
        animator.SetBool("CrowH", true);
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
        animator.SetBool("CrowH", false);
    }
    public IEnumerator Death()
    {
        animator.SetBool("CrowD", true);
        rdb2D.gravityScale = 1;
        Debug.Log("Enemy killed!");
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
