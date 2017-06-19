using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MicrosoftTTS : MonoBehaviour {

    public Text textToSpeak;
    public Button speakButton;
    private int AIstep = 0;
    private float waitingTime;
    private float secPerChar = 0.06f;
    private List<string> sentencesToSpeak;

    void Start()
    {
        speakButton.onClick.AddListener(speakButtonOnClick);
    }

    void Update()
    {
        //if (Input.GetKeyDown("space"))
        //{
        //}
    }

    void speakButtonOnClick()
    {
        print("speakButtonOnClick, AIstep = " + AIstep);

        if (AIstep == 0)
        {
            sentencesToSpeak = new List<string>();
            sentencesToSpeak.Add("Welcome to CityMatrix, an urban dicision support system with AI assistant. ");
            sentencesToSpeak.Add("I am your AI assistant, RZ-14. ");
            stepSpeakAndButton(sentencesToSpeak);
        }
        else if (AIstep == 1)
        {
            sentencesToSpeak = new List<string>();
            sentencesToSpeak.Add("Please look at what is lit below on the table, this is the hypostic city we are going to work on. ");
            sentencesToSpeak.Add("Yes! we are going to use Lego bricks to build the city. ");
            stepSpeakAndButton(sentencesToSpeak);
        }
        else if (AIstep == 2)
        {
            sentencesToSpeak = new List<string>();
            sentencesToSpeak.Add("Now the highlighted are the six different types of buildings, including three residential and three office. ");
            sentencesToSpeak.Add("And each of them have small, medium, and large unit types, which will attract people of different age and income. ");
            stepSpeakAndButton(sentencesToSpeak);
        }
        else if (AIstep == 3)
        {
            sentencesToSpeak = new List<string>();
            sentencesToSpeak.Add("You can add, remove or re-arrange the Lego blocks to create your own city. ");
            sentencesToSpeak.Add("Also, you can put one of the Lego brick into the dock highlighted here to select all the building in the same type, ");
            sentencesToSpeak.Add("and then operate the slider highlighted here to change the height of the building. ");
            stepSpeakAndButton(sentencesToSpeak);
        }
        else if (AIstep == 4)
        {
            sentencesToSpeak = new List<string>();
            sentencesToSpeak.Add("Here on the screen, you will see a three-dimensional representation of the city you created. ");
            sentencesToSpeak.Add("Note that the system will calculate how many and what type of people your city will attract, highlighted here. ");
            sentencesToSpeak.Add("As well as the performance of city in six aspects: ");
            sentencesToSpeak.Add("popluation density, diversity, building cost, energy per person, traffic, and solar radiation. ");
            stepSpeakAndButton(sentencesToSpeak);
        }
        else if (AIstep == 5)
        {
            sentencesToSpeak = new List<string>();
            sentencesToSpeak.Add("Your goal is to creat an city which maxmize the performance of all the six aspects. ");
            stepSpeakAndButton(sentencesToSpeak);
        }
        

        AIstep++;
    }

    void stepSpeakAndButton(List<string> sentencesToSpeak)
    {
        // speak
        List<string> shiftSentences = sentencesToSpeak;
        shiftSentences.Insert(0, "");
        for (int i = 1; i <= shiftSentences.Count - 1; i++)
        {
            waitingTime = 0.0f;
            for (int j = 0; j <= i-1; j++)
            {
                waitingTime += (float)shiftSentences[j].Length * secPerChar;
            }
            StartCoroutine(waitAndSpeak(waitingTime, sentencesToSpeak[i]));
        }

        // button
        speakButton.gameObject.SetActive(false);
        speakButton.GetComponentInChildren<Text>().text = "NEXT";
        waitingTime += (float)sentencesToSpeak[sentencesToSpeak.Count - 1].Length * secPerChar;
        //Debug.Log("button waiting time: " + waitingTime);
        StartCoroutine(waitAndShowButton(waitingTime, speakButton.gameObject));
    }

    IEnumerator waitAndSpeak(float time, string text)
    {
        yield return new WaitForSeconds(time);
        textToSpeak.text = text;
        WindowsVoice.theVoice.speak(text);
    }

    IEnumerator waitAndShowButton(float time, GameObject button)
    {
        yield return new WaitForSeconds(time);
        button.SetActive(true);
    }
}