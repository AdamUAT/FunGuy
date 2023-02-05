using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class CutsceneManager : MonoBehaviour
{
    [SerializeField]
    bool typeText; 

    [SerializeField]
    bool autoNextLine; 

    [SerializeField]
    float timePerLine = 2.0f;

    [SerializeField]
    string nextScene;

    [SerializeField]
    List<string> textLines;

    [SerializeField]
    TextMeshProUGUI dialouge; 

    [SerializeField]
    GameObject pressToContinueText; 

    public PauseInfo pauseInfo;

    int index = 0;
    int currentLineIndex = -1;
    string currentLine = "";
    string actualText = "";
    float nextTime = float.MaxValue;

    // Start is called before the first frame update
    void Start()
    {
        if(autoNextLine)
        {
            pressToContinueText.SetActive(false);
        }
        NextLine();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)
            || Input.GetKeyDown(KeyCode.Return)
            || Input.GetKeyDown(KeyCode.E))
        {
            NextLine();
        }

        if (autoNextLine && Time.time >= nextTime)
        {
            NextLine();
        }
    }

    private void NextLine()
    {
        currentLineIndex++;

        // Show Press To Continue on last line
        if(currentLineIndex == textLines.Count - 1)
        {
            pressToContinueText.SetActive(true);
        }

        // If we have run out of lines go to next scene
        if(currentLineIndex >= textLines.Count)
        {
            SceneManager.LoadScene(nextScene);
            return;
        }

        currentLine = textLines[currentLineIndex];
        if(typeText)
        {   
            // Type out the text
            index = 0;
            actualText = "";
            nextTime = float.MaxValue;
            ReproduceText();
        } else {
            // Show Text directly
            dialouge.text = currentLine;
            SetNextTime();
        }

    }

    private void ReproduceText()
    {
        //if not readied all letters
        if (index < currentLine.Length)
        {
            //get one letter
            char letter = currentLine[index];

            //Actualize on screen
            dialouge.text = Write(letter);

            //set to go to the next
            index += 1;
            StartCoroutine(PauseBetweenChars(letter));

        } else {
            SetNextTime();
        }
    }

    private void SetNextTime()
    {
        if(currentLineIndex == textLines.Count - 1)
        {
            nextTime = float.MaxValue;
        } else {
            nextTime = Time.time + timePerLine;
        }
    }

    private string Write(char letter)
    {
        actualText += letter;
        return actualText;
    }

    private IEnumerator PauseBetweenChars(char letter)
    {
        switch (letter)
        {
            case '.':
                yield return new WaitForSeconds(pauseInfo.dotPause);
                ReproduceText();
                yield break;
            case ',':
                yield return new WaitForSeconds(pauseInfo.commaPause);
                ReproduceText();
                yield break;
            case ' ':
                yield return new WaitForSeconds(pauseInfo.spacePause);
                ReproduceText();
                yield break;
            default:
                yield return new WaitForSeconds(pauseInfo.normalPause);
                ReproduceText();
                yield break;
        }
    }
}


[Serializable]
public class PauseInfo
{
    public float dotPause;
    public float commaPause;
    public float spacePause;
    public float normalPause;
}