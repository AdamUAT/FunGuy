using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
  [SerializeField]
  private GameObject door;

  [SerializeField]
  private SoundPlayer soundPlayer;

  private void OnTriggerEnter2D(Collider2D other)
  {
    Debug.Log("lkajlkjl");
    if (other.gameObject.CompareTag("Player"))
    {
      if (door.activeSelf)
      {
        soundPlayer.gameObject.transform.position = door.transform.position;
        soundPlayer.Play();
        door.SetActive(false);
      }
    }
  }
}
