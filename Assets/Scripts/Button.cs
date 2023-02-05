using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField]
    private GameObject door;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("lkajlkjl");
        if (other.gameObject.CompareTag("Player"))
        {
            door.SetActive(false);
        }
    }
}
