using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class SpeakRecordedVoice : MonoBehaviour {

    public Text textToSpeak;
    public Button speakButton;
    public AudioSource audioSource;
    public GameObject AIStepCtrl;

    public int AIstep = 0;
    public float waitingTime;
    public float secPerChar = 0.06f;
    public List<string> sentencesToSpeak;
    public List<AudioClip> recordedVoicesToPlay;

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
            recordedVoicesToPlay = new List<AudioClip>();
            recordedVoicesToPlay.Add(Resources.Load<AudioClip>("Sounds/_generatedAudio/0.0"));
            recordedVoicesToPlay.Add(Resources.Load<AudioClip>("Sounds/_generatedAudio/0.1"));
            stepSpeakAndButton(sentencesToSpeak, recordedVoicesToPlay);
        }
        else if (AIstep == 1)
        {
            sentencesToSpeak = new List<string>();
            sentencesToSpeak.Add("Please look at what is highlighted below on the table, this is the hypostic city we are going to work on. ");
            sentencesToSpeak.Add("Yes! we are going to use Lego bricks to build the city. ");
            recordedVoicesToPlay = new List<AudioClip>();
            recordedVoicesToPlay.Add(Resources.Load<AudioClip>("Sounds/_generatedAudio/1.0"));
            recordedVoicesToPlay.Add(Resources.Load<AudioClip>("Sounds/_generatedAudio/1.1"));
            stepSpeakAndButton(sentencesToSpeak, recordedVoicesToPlay);
        }
        else if (AIstep == 2)
        {
            sentencesToSpeak = new List<string>();
            sentencesToSpeak.Add("Now the highlighted are the six different types of buildings, including three residential and three office. ");
            sentencesToSpeak.Add("And each of them have small, medium, and large unit types, which will attract people of different age and income. ");
            recordedVoicesToPlay = new List<AudioClip>();
            recordedVoicesToPlay.Add(Resources.Load<AudioClip>("Sounds/_generatedAudio/2.0"));
            recordedVoicesToPlay.Add(Resources.Load<AudioClip>("Sounds/_generatedAudio/2.1"));
            stepSpeakAndButton(sentencesToSpeak, recordedVoicesToPlay);
        }
        else if (AIstep == 3)
        {
            sentencesToSpeak = new List<string>();
            sentencesToSpeak.Add("You can add, remove or re-arrange the Lego blocks to create your own city. ");
            sentencesToSpeak.Add("Also, you can put one of the Lego brick into the dock highlighted here to select all the building in the same type, ");
            sentencesToSpeak.Add("and then operate the slider highlighted here to change the height of the building. ");
            recordedVoicesToPlay = new List<AudioClip>();
            recordedVoicesToPlay.Add(Resources.Load<AudioClip>("Sounds/_generatedAudio/3.0"));
            recordedVoicesToPlay.Add(Resources.Load<AudioClip>("Sounds/_generatedAudio/3.1"));
            recordedVoicesToPlay.Add(Resources.Load<AudioClip>("Sounds/_generatedAudio/3.2"));
            stepSpeakAndButton(sentencesToSpeak, recordedVoicesToPlay);
        }
        else if (AIstep == 4)
        {
            sentencesToSpeak = new List<string>();
            sentencesToSpeak.Add("Here on the screen, you will see a three-dimensional representation of the city you created. ");
            sentencesToSpeak.Add("Note that the system will calculate how many and what type of people your city will attract, highlighted here. ");
            sentencesToSpeak.Add("As well as the performance of city in six aspects: ");
            sentencesToSpeak.Add("popluation density, diversity, building cost, energy per person, traffic, and solar radiation. ");
            recordedVoicesToPlay = new List<AudioClip>();
            recordedVoicesToPlay.Add(Resources.Load<AudioClip>("Sounds/_generatedAudio/4.0"));
            recordedVoicesToPlay.Add(Resources.Load<AudioClip>("Sounds/_generatedAudio/4.1"));
            recordedVoicesToPlay.Add(Resources.Load<AudioClip>("Sounds/_generatedAudio/4.2"));
            recordedVoicesToPlay.Add(Resources.Load<AudioClip>("Sounds/_generatedAudio/4.3"));
            stepSpeakAndButton(sentencesToSpeak, recordedVoicesToPlay);
        }
        else if (AIstep == 5)
        {
            sentencesToSpeak = new List<string>();
            sentencesToSpeak.Add("Your goal is to creat an city which maxmize the performance of all the six aspects. ");
            recordedVoicesToPlay = new List<AudioClip>();
            recordedVoicesToPlay.Add(Resources.Load<AudioClip>("Sounds/_generatedAudio/5.0"));
            stepSpeakAndButton(sentencesToSpeak, recordedVoicesToPlay);
        }


        AIstep++;
    }

    void stepSpeakAndButton(List<string> sentencesToSpeak, List<AudioClip> recordedVoicesToPlay)
    {
        /*
        // speak
        List<string> shiftSentences = sentencesToSpeak;
        shiftSentences.Insert(0, "");
        for (int i = 1; i <= shiftSentences.Count - 1; i++)
        {
            waitingTime = 0.0f;
            for (int j = 0; j <= i - 1; j++)
            {
                waitingTime += (float)shiftSentences[j].Length * secPerChar;
            }
            StartCoroutine(waitAndSpeak(waitingTime, sentencesToSpeak[i]));
        }
        */

        // play recorded voice
        // creat an float list which contains all audio clip length and add 0.0f in the front
        List<float> shiftVoiceClipsLength = new List<float>();
        shiftVoiceClipsLength.Add(0.0f);
        for (int i = 0; i <= recordedVoicesToPlay.Count - 1; i++)
        {
            shiftVoiceClipsLength.Add(recordedVoicesToPlay[i].length);
        }
        // loop each audio clip: wait for sum up of the audio clip lengths before it and play
        for (int i = 0; i <= recordedVoicesToPlay.Count - 1; i++)
        {
            waitingTime = 0.0f;
            for (int j = 0; j <= i; j++)
            {
                waitingTime += shiftVoiceClipsLength[j];
            }
            StartCoroutine(waitAndPlay(waitingTime, recordedVoicesToPlay[i], sentencesToSpeak[i]));
        }

        // button
        speakButton.gameObject.SetActive(false);
        speakButton.GetComponentInChildren<Text>().text = "NEXT";
        waitingTime += recordedVoicesToPlay[sentencesToSpeak.Count - 1].length;
        //Debug.Log("button waiting time: " + waitingTime);
        StartCoroutine(waitAndShowButton(waitingTime, speakButton.gameObject));
    }

    IEnumerator waitAndPlay(float time, AudioClip voice, string text)
    {
        yield return new WaitForSeconds(time);
        textToSpeak.text = text;
        //WindowsVoice.theVoice.speak(text);
        audioSource.PlayOneShot(voice, 1.0f);
    }

    IEnumerator waitAndShowButton(float time, GameObject button)
    {
        yield return new WaitForSeconds(time);
        button.SetActive(true);
    }
}
