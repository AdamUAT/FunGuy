using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
  void Start()
  {
    MusicManager.Instance.TransitionToSong(0, 0.0f);
  }

  public void PlayGame()
  {
    SceneManager.LoadScene("IntroCutscene");
    MusicManager.Instance.TransitionToSong(1,1.0f);
  }
}
