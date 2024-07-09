using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    //Script de una camara de estilo metroidvania, esta sigue al heroe en todas sus componentes y ademas posee cierta realitizacion en base a la definida en el inspector.
    public Transform hero;
    public Transform cameraRoomLimit;
    public static CameraControl instance;
    public float dampSpeed;

    [Range(-10,10)] public float xMin;
    [Range(-10,10)] public float xMax;
    [Range(-10,10)] public float yMin;
    [Range(-10,10)] public float yMax;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Update()
    {
        var posMinY = cameraRoomLimit.GetComponent<BoxCollider2D>().bounds.min.y + yMin;
        var posMinX = cameraRoomLimit.GetComponent<BoxCollider2D>().bounds.min.x + xMin;
        var posMaxY = cameraRoomLimit.GetComponent<BoxCollider2D>().bounds.max.y + yMax;
        var posMaxX = cameraRoomLimit.GetComponent<BoxCollider2D>().bounds.max.x + xMax;

        Vector3 cameraPos = new Vector3(Mathf.Clamp(hero.position.x, posMinX, posMaxX), Mathf.Clamp(hero.position.y, posMinY, posMaxY), Mathf.Clamp(hero.position.z, -10f, -10f));
        Vector3 smoothPos = Vector3.Lerp(transform.position, cameraPos, dampSpeed * Time.deltaTime);
        transform.position = smoothPos;
    }
}
