using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Watcher : MonoBehaviour
{

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Vector3 visualizer = transform.localScale;
        float visualizador =  Mathf.Max(visualizer.x, visualizer.y, visualizer.z);
        Gizmos.DrawWireSphere(transform.position, visualizador/2);
    }
}
