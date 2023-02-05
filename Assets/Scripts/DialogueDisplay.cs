using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueDisplay : MonoBehaviour
{
  [SerializeField]
  private GameObject playerDialogue;

  // [SerializeField]
  // private GameObject paragraphDialogue;

  [SerializeField]
  private TextMeshProUGUI dialogueText;

  private GameObject player;

  //The two sets of timers tells update which one's active.
  private float deactiveTime;

  [SerializeField]
  private float lengthOfTimer = 5.0f; //The length of the timer in seconds

  public enum ChatEvent {
    CasualChat,
    RunningIntoWhiteBloodCells,
    TrappingWhiteBloodCell,
    MovingThroughSelf,
    TrappedByBacteria,
    DefeatingBacteria,
    RunningIntoBlockade,
    PassingBlockade
  }

  [SerializeField]
  List<TextWeightTableEntry> casualChat;

  [SerializeField]
  List<TextWeightTableEntry> runningIntoWhiteBloodCells;

  [SerializeField]
  List<TextWeightTableEntry> trappingWhiteBloodCell;

  [SerializeField]
  List<TextWeightTableEntry> movingThroughSelf;

  [SerializeField]
  List<TextWeightTableEntry> trappedByBacteria;

  [SerializeField]
  List<TextWeightTableEntry> defeatingBacteria;

  [SerializeField]
  List<TextWeightTableEntry> runningIntoBlockade;

  [SerializeField]
  List<TextWeightTableEntry> passingBlockade;

  private void Start()
  {
    playerDialogue.SetActive(false);
  }

  private string GetStringFromEventTable(ChatEvent eventType)
  {
    switch(eventType){
      case ChatEvent.CasualChat:
        return GetRandomValueFromWeightedTable(casualChat);
      case ChatEvent.RunningIntoWhiteBloodCells:
        return GetRandomValueFromWeightedTable(runningIntoWhiteBloodCells);
      case ChatEvent.TrappingWhiteBloodCell:
        return GetRandomValueFromWeightedTable(trappingWhiteBloodCell);
      case ChatEvent.MovingThroughSelf:
        return GetRandomValueFromWeightedTable(movingThroughSelf);
      case ChatEvent.TrappedByBacteria:
        return GetRandomValueFromWeightedTable(movingThroughSelf);
      case ChatEvent.DefeatingBacteria:
        return GetRandomValueFromWeightedTable(defeatingBacteria);
      case ChatEvent.RunningIntoBlockade:
        return GetRandomValueFromWeightedTable(runningIntoBlockade);
      case ChatEvent.PassingBlockade:
        return GetRandomValueFromWeightedTable(passingBlockade);
      default:
        throw new KeyNotFoundException("Unimpliment ChatEvent");
    }
  }

  private string GetRandomValueFromWeightedTable(List<TextWeightTableEntry> table)
  {
    float totalWeight = 0.0f;
    foreach(TextWeightTableEntry entry in table)
    {
      totalWeight += entry.weight;
    }
    float roll = UnityEngine.Random.Range(0.0f, totalWeight);

    float tempWeight = 0.0f;
    foreach(TextWeightTableEntry entry in table)
    {
      tempWeight += entry.weight;
      if( roll <= tempWeight) return entry.text;
    }

    throw new ArgumentOutOfRangeException("Roll higher that the range. This should not happen");
  }

  public void TriggerChatEvent(ChatEvent eventType)
  {
    string content = GetStringFromEventTable(eventType);
    DisplayPlayerDialogue(content);
  }

  //The function to call to display dialogue as if it was said from the character.
  public void DisplayPlayerDialogue(string content)
  {
    ActivatePlayerDialogue();
    dialogueText.text = content;
  }

  //Reveals the player speech bubble, scales it, moves it to the player, and then positions it so it's not out of bounds.
  private void ActivatePlayerDialogue()
  {
    playerDialogue.SetActive(true);

    deactiveTime = Time.time + lengthOfTimer;
  }

  //Updates the dialogue box to match the current possition of the player
  private void UpdatePlayerDialogue()
  {
    if (Camera.current == null) //For some reason, Camera.current is null around 10% of the time. No clue why.
      return; //If the camera is null, then don't do any of the below calculations.


    Vector2 playerScreenPositionPixels = Camera.current.WorldToScreenPoint(player.transform.position);
    Vector2 playerScreenPositionPercentage = playerScreenPositionPixels / new Vector2(Camera.current.pixelWidth, Camera.current.pixelHeight);
    playerDialogue.GetComponent<RectTransform>().anchorMin = playerScreenPositionPercentage;
    playerDialogue.GetComponent<RectTransform>().anchorMax = playerScreenPositionPercentage;

    if (playerScreenPositionPixels.x + 500 >= Camera.current.pixelWidth && playerScreenPositionPixels.y - 400 <= 0)
    {
      playerDialogue.GetComponent<RectTransform>().localScale = new Vector2(-1, -1);
      dialogueText.gameObject.GetComponent<RectTransform>().localScale = new Vector2(-1, -1);
    }
    else if (playerScreenPositionPixels.x + 500 >= Camera.current.pixelWidth)
    {
      playerDialogue.GetComponent<RectTransform>().localScale = new Vector2(-1, 1);
      dialogueText.gameObject.GetComponent<RectTransform>().localScale = new Vector2(-1, 1);
    }
    else if (playerScreenPositionPixels.y - 400 <= 0)
    {
      playerDialogue.GetComponent<RectTransform>().localScale = new Vector2(1, -1);
      dialogueText.gameObject.GetComponent<RectTransform>().localScale = new Vector2(1, -1);
    }
    else
    {
      playerDialogue.GetComponent<RectTransform>().localScale = new Vector2(1, 1);
      dialogueText.gameObject.GetComponent<RectTransform>().localScale = new Vector2(1, 1);
    }
  }

  private void DeactivatePlayerDialogue()
  {
    playerDialogue.SetActive(false);
  }


  //The function to call to display any naration dialogue
  // public void DisplayParagraphDialogue(string content)
  // {

  // }

  // private void ActivateParagraphDialogue()
  // {

  // }

  // private void UpdateParagraphDialogue()
  // {

  // }

  // private void DeactivateParagraphDialogue()
  // {

  // }

  public void SetPlayer(GameObject _player)
  {
    player = _player;
    PlayerCasualChatTrigger casualChatTrigger = player.GetComponent<PlayerCasualChatTrigger>();
    if (casualChatTrigger != null)
    {
      casualChatTrigger.onCasualChatTrigger.AddListener( delegate { TriggerChatEvent(ChatEvent.CasualChat); } );
    }
  }

  void Update()
  {
    //If the image was already deactivated, then it knows to not call Deactivate...() again.
    if(playerDialogue.activeSelf)
    {
      if (Time.time > deactiveTime)
      {
        DeactivatePlayerDialogue();
      } else {
        UpdatePlayerDialogue();
      }
    }

    // if (playerTimer > 0)
    //   UpdateParagraphDialogue();
    // else if (playerDialogue.activeSelf)
    //   DeactivateParagraphDialogue();
  }
}

[Serializable]
public class TextWeightTableEntry
{
  public float weight;
  public string text;
}
