using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootCrowAI : MonoBehaviour
{
    /*Este script se encarga de gestionar los proyectiles asignados desde el motor, los proyectiles empiezan su recorrido desde el punto central del Crow y espaciaados por el shootingTime*/
    [SerializeField] public GameObject projectile;
    [SerializeField] public float shoottingTime;
    private bool canShoot = false;

    void Start()
    {
        StartCoroutine(Shooting());
    }
    IEnumerator Shooting()
    {
        while (true)
        {
            yield return new WaitForSeconds(shoottingTime);
            if (canShoot)
            {
                Instantiate(projectile, transform.position, Quaternion.identity);
            }
        }
    }
    public void EnableShooting()
    {
        canShoot = true;
    }
    public void DisableShooting()
    {
        canShoot = false;
    }
}
