using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Camera camera;
    private BoxCollider2D bounds;

    void Start()
    {
        camera = GetComponent<Camera>();
        bounds = GetComponent<BoxCollider2D>();
        bounds.size = new Vector2(camera.orthographicSize * 16 / 9 * 2, camera.orthographicSize * 2); //Sets the size of the collider to the size of the camera.

    }
}
