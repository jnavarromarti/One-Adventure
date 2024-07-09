using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectorControllerGolem : MonoBehaviour
{
    //Gestor de detector del Golem, si el player entra en el collider activa la inteligencia de persecucion, si este la abandona lo sedactiva.
    [SerializeField] public PatrolAIGolem patrol;
    [SerializeField] public IAGolem golem;

    private void Start()
    {
        patrol = GetComponentInParent<PatrolAIGolem>();
        golem = GetComponentInParent<IAGolem>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            patrol.enabled = false;
            golem.enabled = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            patrol.enabled = true;
            golem.enabled = false;
        }
    }
}
