using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] LayerMask terrainLayer;
    public bool isGrounded;
    public Vector2 direction;
    [SerializeField] float distance;

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.Raycast(this.transform.position, direction, distance, terrainLayer);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(this.transform.position, this.transform.position + new Vector3(direction.x, direction.y, 0) * distance);
    }
}
