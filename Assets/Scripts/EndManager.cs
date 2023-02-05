using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndManager : MonoBehaviour
{
    void Update()
    {
       if (Input.GetKey(KeyCode.Q))
        {
            SceneManager.LoadScene("MainMenu");
        } 
    }
}
