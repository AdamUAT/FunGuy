using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerPawn playerPawn;
    [SerializeField]
    private KeyCode up;
    [SerializeField]
    private KeyCode down;
    [SerializeField]
    private KeyCode left;
    [SerializeField]
    private KeyCode right;

    void Update()
    {
        ProcessInputs();
    }

    public void ProcessInputs()
    {
        if (Input.GetKey(up))
        {
            playerPawn.MoveUp();
        }

        if (Input.GetKey(down))
        {
            playerPawn.MoveDown();
        }

        if (Input.GetKey(left))
        {
            playerPawn.MoveLeft();
        }

        if (Input.GetKey(right))
        {
            playerPawn.MoveRight();
        }
    }
}
