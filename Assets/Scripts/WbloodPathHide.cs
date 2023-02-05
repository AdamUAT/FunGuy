using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WbloodPathHide : MonoBehaviour
{
    void Awake()
    {
        GetComponent<TilemapRenderer>().enabled = false; //Makes it so the player cannot see the path of the Wblood at runtime.
    }
}
