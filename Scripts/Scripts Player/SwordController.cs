using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour
{
    //Script del sword, es el encargado de proporcionar el daño a los distintos enemigos asi como de ejecutar los metodos handle death si su vida llega a 0.
    //Por otra parte diferencia el tipo de ataque segun un switch, si es un ataque rapido/normal o un ataque saltando el swordController solo restara un punto de vida. Mientras que si es un charged attack este le restará 3 puntos de vida al enemyGeneralScript
    private Collider2D swordCollider;
    public enum AttackType { Attack, JumpAttack, ChargedAttack };
    private AttackType currentAttack = AttackType.Attack;

    private void Awake()
    {
        swordCollider = GetComponent<Collider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyGeneralScript enemy = collision.gameObject.GetComponent<EnemyGeneralScript>();
            DamageControllerSlime slime = collision.gameObject.GetComponent<DamageControllerSlime>();
            DamageControllerCrow crow = collision.gameObject.GetComponent<DamageControllerCrow>();
            DamageControllerGolem golem = collision.gameObject.GetComponent<DamageControllerGolem>();
            if (slime != null)
            {
                slime.TakeDamage(CalculateDamage());
                slime.SlimeDamaged();
                slime.HandleDeath();
            }
            else if (crow != null)
            {
                crow.TakeDamage(CalculateDamage());
                crow.GolemDamaged();
                crow.HandleDeath();
            }
            else if (golem != null)
            {
                golem.TakeDamage(CalculateDamage());
                golem.GolemDamaged();
                golem.HandleDeath();
            }
        }
    }
    public int CalculateDamage()
    {
        switch (currentAttack)
        {
            case AttackType.ChargedAttack:
                return 3;
            case AttackType.JumpAttack:
            case AttackType.Attack:
            default:
                return 1;
        }
    }
    public void SetAttackType(AttackType attackType)
    {
        currentAttack = attackType;
    }
}
