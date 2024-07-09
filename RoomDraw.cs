using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomDraw : MonoBehaviour
{
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 visualizer = transform.localScale;
        Gizmos.DrawWireCube(transform.position, visualizer);
    }
}