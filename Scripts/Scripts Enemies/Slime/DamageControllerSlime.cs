using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageControllerSlime : MonoBehaviour
{
    //Inicializamos las variables
    Animator animator;
    EnemyGeneralScript enemy;
    IASlime slime;
    Rigidbody2D rdb2D;
    //Este script gestiona el daño recibido desde el sword controller y actualiza la vida del enemy general script, por otra parte aplica sobre el
    //gameobject las fuerzas knockback para que haya un feedback de daño desde el heroe al slime. Si la vida es 0 ejecuta el metodo handle death y la corrutina para eliminar el objeto
    void Awake()
    {
        animator = GetComponentInParent<Animator>();
        enemy = GetComponentInParent<EnemyGeneralScript>();
        slime = GetComponentInParent<IASlime>();
        rdb2D = GetComponentInParent<Rigidbody2D>();
    }

    public void SlimeDamaged()
    {
        Vector2 knockback = new Vector2(-1f * (slime.isFacingRight ? 1 : -1), 1f);
        rdb2D.AddForce(knockback * enemy.knockbackForceX, ForceMode2D.Impulse);
        StartCoroutine(Damaged());
    }
    public void TakeDamage(int damage)
    {
        enemy.enemyHealth -= damage;
        animator.SetBool("SlimeH", true);
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
        animator.SetBool("SlimeH", false);
    }
    public IEnumerator Death()
    {
        animator.SetBool("SlimeD", true);
        Debug.Log("Enemy killed!");
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
