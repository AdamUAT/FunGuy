using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSystem : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<Camera>() != null) //Checks to see if the triggered area is connected to a camera.
        {
            //deactivates the current camera in the viewport and activates the new one. The component camera is deactivated, not the gameobject, so the trigger still works.
            if(Camera.current != null) //At the start of the game, this method is called before the camera turns on, so it would be null. This isn't good.
                Camera.current.GetComponent<Camera>().enabled = false;
            collision.gameObject.GetComponent<Camera>().enabled = true;
        }
    }
}
