using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollisionControler : MonoBehaviour
{
    EnemyGeneralScript enemy;

    public void Start()
    {
        enemy = GetComponent<EnemyGeneralScript>();
    }
}
