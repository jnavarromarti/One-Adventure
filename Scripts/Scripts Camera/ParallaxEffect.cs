using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    //Script del efecto parallax, este en base a la posicion de la camara va añadiendo terreno en el eje horizontal x para dar la sensacion de que hay un mapa infinito.
    //La velocidad a la que se actualiza depende de lo lejana o cercana que se encuentre del heroe, a mayor lejania el objeto tiene mayor parallaxmultiplier a mas cercania menor.
    [SerializeField] private float parallaxEffectMultiplier;
    private Transform cameraTransform;
    private Vector3 previousCameraPosition;
    private float textureUnitSizeX,  texturePositionX;

    void Start()
    {
        cameraTransform = Camera.main.transform;
        previousCameraPosition = cameraTransform.position;
        textureUnitSizeX = GetComponent<SpriteRenderer>().bounds.size.x;
        texturePositionX = transform.position.x;
    }

    void Update()
    {
        float deltaX = (cameraTransform.position.x - previousCameraPosition.x)* parallaxEffectMultiplier;
        float moveAmount = cameraTransform.position.x *  (1 - parallaxEffectMultiplier);
        transform.Translate(new Vector3(deltaX, 0f, 0f));
        previousCameraPosition = cameraTransform.position;
        if (moveAmount > texturePositionX + textureUnitSizeX)
        {
            transform.Translate(new Vector3(textureUnitSizeX, 0f, 0f));
            texturePositionX += textureUnitSizeX;
        }
        else if (moveAmount < texturePositionX - textureUnitSizeX)
        {
            transform.Translate(new Vector3(-textureUnitSizeX, 0f, 0f));
            texturePositionX -= textureUnitSizeX;

        }
    }
}
