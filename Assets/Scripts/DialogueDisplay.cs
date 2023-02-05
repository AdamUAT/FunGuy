using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueDisplay : MonoBehaviour
{
    [SerializeField]
    private GameObject playerDialogue;
    [SerializeField]
    private GameObject paragraphDialogue;
    private GameObject player;

    //The two sets of timers tells update which one's active.
    private float playerTimer;
    private float paragraphTimer;
    [SerializeField]
    private float lengthOfTimer = 5.0f; //The length of the timer in seconds

    //The function to call to display dialogue as if it was said from the character.
    public void DisplayPlayerDialogue(string content)
    {
        ActivatePlayerDialogue();

    }

    //The function to call to display any naration dialogue
    public void DisplayParagraphDialogue(string content)
    {

    }

    //Reveals the player speech bubble, scales it, moves it to the player, and then positions it so it's not out of bounds.
    private void ActivatePlayerDialogue()
    {
        playerDialogue.SetActive(true);

        playerTimer = lengthOfTimer;
    }

    //Updates the dialogue box to match the current possition of the player
    private void UpdatePlayerDialogue()
    {
        Vector2 playerScreenPositionPixels;
        Vector2 playerScreenPositionPercentage;

        if (Camera.current == null) //For some reason, Camera.current is null around 10% of the time. No clue why.
            return; //If the camera is null, then don't do any of the below calculations.


        playerScreenPositionPixels = Camera.current.WorldToScreenPoint(player.transform.position);
        playerScreenPositionPercentage = playerScreenPositionPixels / new Vector2(Camera.current.pixelWidth, Camera.current.pixelHeight);
        playerDialogue.GetComponent<RectTransform>().anchorMin = playerScreenPositionPercentage;
        playerDialogue.GetComponent<RectTransform>().anchorMax = playerScreenPositionPercentage;

        if (playerScreenPositionPixels.x + 500 >= Camera.current.pixelWidth && playerScreenPositionPixels.y - 400 <= 0)
        {
            playerDialogue.GetComponent<RectTransform>().localScale = new Vector2(-1, -1);
        }
        else if (playerScreenPositionPixels.x + 500 >= Camera.current.pixelWidth)
        {
            playerDialogue.GetComponent<RectTransform>().localScale = new Vector2(-1, 1);
        }
        else if (playerScreenPositionPixels.y - 400 <= 0)
        {
            playerDialogue.GetComponent<RectTransform>().localScale = new Vector2(1, -1);
        }
        else
        {
            playerDialogue.GetComponent<RectTransform>().localScale = new Vector2(1, 1);
        }

        playerTimer -= Time.deltaTime;
    }

    private void DeactivatePlayerDialogue()
    {
        playerDialogue.SetActive(false);
    }


    private void ActivateParagraphDialogue()
    {

    }

    private void UpdateParagraphDialogue()
    {

    }

    private void DeactivateParagraphDialogue()
    {

    }

    public void SetPlayer(GameObject _player) { player = _player; }

    void Update()
    {
        //If the image was already deactivated, then it knows to not call Deactivate...() again.
        if (playerTimer > 0)
            UpdatePlayerDialogue();
        else if (playerDialogue.activeSelf)
            DeactivatePlayerDialogue();

        if (playerTimer > 0)
            UpdateParagraphDialogue();
        else if (playerDialogue.activeSelf)
            DeactivateParagraphDialogue();
    }
}
