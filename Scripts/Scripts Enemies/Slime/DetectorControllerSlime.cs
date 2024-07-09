using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectorControllerSlime : MonoBehaviour
{
    //Gestor de deteccion del Slime, tambien ejecuta la funcion de corregir la animacion si el heroe avandona la zona.
    [SerializeField] public IASlime slime;
    [SerializeField] public PatrolAI patrol;
    [SerializeField] public Animator animator;

    private void Start()
    {
        slime = GetComponentInParent<IASlime>();
        patrol = GetComponentInParent<PatrolAI>();
        animator = GetComponentInParent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            slime.enabled = true;
            patrol.enabled = false;
            slime.isAttacking = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            slime.enabled = false;
            patrol.enabled = true;
            slime.isAttacking = false;
            animator.SetBool("SlimeI", true);
            animator.SetBool("SlimeJOut", false);
            animator.SetBool("SlimeJ", false);
        }
    }
}
