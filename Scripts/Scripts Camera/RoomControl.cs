using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomControl : MonoBehaviour
{
    //Room manager, detecta la entrada del heroe al espacion room para adaptar la camara a la posicion del heroe
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            transform.GetChild(0).gameObject.SetActive(true);
            CameraControl.instance.cameraRoomLimit = transform.GetChild(0);
        }
    }
    private void OnTriggerStay2D (Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CameraControl.instance.cameraRoomLimit = transform.GetChild(0);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}
