using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSystem : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("ajkdsflka");
        if(collision.gameObject.GetComponent<Camera>() != null) //Checks to see if the triggered area is connected to a camera.
        {
            //deactivates the current camera in the viewport and activates the new one. The component camera is deactivated, not the gameobject, so the trigger still works.
            Camera.current.GetComponent<Camera>().enabled = false;
            collision.gameObject.GetComponent<Camera>().enabled = true;
        }
    }
}
