using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectorControllerCrow : MonoBehaviour
{
    //Tiene el mismo funcionamiento que el Detector norma, una vez el heroe entra en el collider se activa la IA de persecucion y el disparo a no ser que el jugador abandone la zona y el Crow volvera a su estado de patruya
    [SerializeField] public PatrolAI patrol;
    [SerializeField] public ShootCrowAI shoot;
    [SerializeField] public IACrow crow;

    private void Start()
    {
        patrol = GetComponentInParent<PatrolAI>();
        shoot = GetComponentInParent<ShootCrowAI>();
        crow = GetComponentInParent<IACrow>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            patrol.enabled = false;
            crow.enabled = true;
            shoot.EnableShooting();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            patrol.enabled = true;
            crow.enabled = false;
            shoot.DisableShooting();
        }
    }
}
