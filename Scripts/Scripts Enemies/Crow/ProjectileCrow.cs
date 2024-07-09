using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCrow : MonoBehaviour
{
    //Este script se encarga del movimiento de los proyectiles, los cuales detectan la posicion del hero, establecen una velocidad constante y se dirigen hacia esta.
    //Si el proyectil desaparece hacia el infinito es eliminado despues de cierto tiempo, igual que si se choca contra el suelo.
    [SerializeField] private float speed;
    private Transform hero;
    private Rigidbody2D rdb2D;
    void Start()
    {
        hero = GameObject.FindGameObjectWithTag("Player").transform;
        rdb2D = GetComponent<Rigidbody2D>();
        Shooting();
    }
    private void Shooting()
    {
        Vector2 direction = (hero.position - transform.position).normalized;
        rdb2D.velocity = direction * speed;
        StartCoroutine(DestroyProjectile());
    }
    IEnumerator DestroyProjectile()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Terrain"))
        {
            Destroy(gameObject);
        }
    }
}
