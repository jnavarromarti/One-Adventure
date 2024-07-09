using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DamageControl : MonoBehaviour
{
    //Scrip que gestiona las animaciones de muerte del heroe, su colision con los distintos gameobjects (enemigos), las trampas,y proyectiles. Si alguno de estos choca con el, se ejecutara el handle death y por ende el metodo respawn volviendo al inicio.
    [SerializeField] private HeroController heroController;
    private Collider2D heroCollider;
    private Animator animator;
    public int deathCounter;

    private void Awake()
    {
        heroCollider = GetComponent<Collider2D>();
        animator = GetComponentInParent<Animator>();
    }

    public void SetDeath()
    {
        animator.SetBool("Damage", true);
        StartCoroutine(Death());
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            SetDeath();
        }
        else if (collision.gameObject.CompareTag("Projectile"))
        {
            SetDeath();
        }
        else if (collision.gameObject.CompareTag("Traps"))
        {
            SetDeath();
        }
    }
    public IEnumerator Death()
    {
        heroController.enabled = false;
        Debug.Log("You died!");
        yield return new WaitForSeconds (0.5f);
        animator.SetBool("Damage", false);
        animator.SetBool("Death", true);
        yield return new WaitForSeconds(1.5f);
        animator.SetBool("Death", false);
        heroController.enabled = true;
        GetComponentInParent<HeroController>().Respawn();
        deathCounter++;
    }
}
