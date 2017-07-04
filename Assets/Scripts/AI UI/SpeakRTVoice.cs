using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using Crosstales.RTVoice;

[RequireComponent(typeof(AudioSource))]
public class SpeakRTVoice : MonoBehaviour {

    public Text textToSpeak;
    public Button speakButton;
    public string txtFilePathAIScript = "Resources/Data/AI Script Texts/AI Script.txt";
    public List<string> linesAIScript;
    public int lastStepInt;
    public int AIStep = 0;
    public float secPerChar = 0.0675f;
    public float waitingTime;
    public List<string> sentencesToSpeak;
    public GameObject RTVoice;
    public GameObject debugText;
    //public string invokedSentenceToSpeak;

    void Start()
    {
        speakButton.onClick.AddListener(speakButtonOnClick);

        // read txt file to a string list
        linesAIScript = readTextFile(txtFilePathAIScript);

        // get last step int
        bool stepIntFound = true;
        int currentStepInt = 0;
        lastStepInt = 0;
        while (stepIntFound)
        {
            //Debug.Log("checking step int:" + currentStepInt);
            stepIntFound = false;
            for (int i = 0; i <= linesAIScript.Count - 1; i++)
            {
                if (linesAIScript[i].Contains(currentStepInt.ToString() + ".0"))
                {
                    stepIntFound = true;
                    //Debug.Log("found:" + currentStepInt.ToString() + ".0");
                    break;
                }
            }
            if (!stepIntFound)
            {
                //Debug.Log("not found!:" + currentStepInt.ToString() + ".0");
            }
            else
            {
                currentStepInt++;
                //Debug.Log("checked, next step int:" + currentStepInt);
            }
        }
        lastStepInt = currentStepInt - 1;
        //Debug.Log("last step int found: " + lastStepInt);
    }

    void Update()
    {
        //if (Input.GetKeyDown("space"))
        //{
        //}
    }

    void speakButtonOnClick()
    {
        // get AIStep from AIStepCtrl
        AIStep = GetComponent<AIStepCtrl>().intAIStep;
        //print("speakButtonOnClick, AIstep = " + AIStep);
        StopAllCoroutines();
        //CancelInvoke();
        
        if (AIStep <= lastStepInt)
        {
            sentencesToSpeak = new List<string>();

            // get last substep int and add found sub sentences to sentencesToSpeak
            bool subStepIntFound = true;
            int currentSubStepInt = 0;
            int lastSubStepInt = 0;
            // define search range to avoid find "0.2" in "10.2"
            int searchUpto = 0;
            if (AIStep < lastStepInt)
            {
                for (int i = 0; i <= linesAIScript.Count - 1; i++)
                {
                    if (linesAIScript[i].Contains((AIStep + 1).ToString() + ".0"))
                    {
                        searchUpto = i;
                        break;
                    }
                }
            }
            else
            {
                searchUpto = linesAIScript.Count - 1;
            }
            while (subStepIntFound)
            {
                //Debug.Log("checking sub step int:" + currentSubStepInt);
                subStepIntFound = false;
                for (int i = 0; i <= searchUpto - 1; i++)
                {
                    if (linesAIScript[i].Contains(AIStep.ToString() + "." + currentSubStepInt.ToString()))
                    {
                        subStepIntFound = true;
                        //Debug.Log("found:" + AIStep.ToString() + "." + currentSubStepInt.ToString());
                        sentencesToSpeak.Add(linesAIScript[i+1]);
                        break;
                    }
                }
                if (!subStepIntFound)
                {
                    //Debug.Log("not found!:" + AIStep.ToString() + "." + currentSubStepInt.ToString());
                }
                else
                {
                    currentSubStepInt++;
                    //Debug.Log("checked, next sub step int:" + currentSubStepInt);
                }
            }
            lastSubStepInt = currentSubStepInt - 1;
            //Debug.Log("last sub step int found: " + lastSubStepInt);

            // speak sub sentences
            stepSpeakAndButton(sentencesToSpeak);
        }

        //AIStep++; // now handled by AIStepCtrl
    }

    void stepSpeakAndButton(List<string> sentencesToSpeak)
    {
        // hide button and chang button text
        speakButton.gameObject.SetActive(false);
        speakButton.GetComponentInChildren<Text>().text = "NEXT";

        // speak
        List<string> shiftSentences = new List<string>(sentencesToSpeak);
        shiftSentences.Insert(0, "");
        for (int i = 1; i <= shiftSentences.Count - 1; i++)
        {
            waitingTime = 0.0f;
            for (int j = 0; j <= i - 1; j++)
            {
                waitingTime += (float)shiftSentences[j].Length * secPerChar;
            }
            StartCoroutine(waitAndSpeak(waitingTime, sentencesToSpeak[i-1]));
            //invokedSentenceToSpeak = sentencesToSpeak[i - 1];
            //Invoke("invokeSpeak", waitingTime);
        }

        // wait and show button
        waitingTime += (float)sentencesToSpeak[sentencesToSpeak.Count - 1].Length * secPerChar;
        //Debug.Log("button waiting time: " + waitingTime);
        StartCoroutine(waitAndShowButton(waitingTime, speakButton.gameObject));
        //Invoke("invokeShowButton", waitingTime);
    }

    private List<string> readTextFile(string file_path)
    {
        List<string> stringList = new List<string>();

        StreamReader inp_stm = new StreamReader(file_path);

        while (!inp_stm.EndOfStream)
        {
            string inp_ln = inp_stm.ReadLine();
            // Do Something with the input. 
            stringList.Add(inp_ln);
        }

        inp_stm.Close();

        return stringList;
    }

    IEnumerator waitAndSpeak(float time, string text)
    {
        yield return new WaitForSeconds(time);
        textToSpeak.text = text;
        RTVoice.GetComponent<LiveSpeaker>().Silence();
        RTVoice.GetComponent<LiveSpeaker>().Speak(text);
        debugText.GetComponent<Text>().text += "\n" + "RTVoice.GetComponent<LiveSpeaker>().Speak(\"" + text + "\");";
    }

    IEnumerator waitAndShowButton(float time, GameObject button)
    {
        yield return new WaitForSeconds(time);
        button.SetActive(true);
    }

    /*
    void invokeSpeak()
    {
        textToSpeak.text = invokedSentenceToSpeak;
        RTVoice.GetComponent<LiveSpeaker>().Silence();
        RTVoice.GetComponent<LiveSpeaker>().Speak(invokedSentenceToSpeak);
        debugText.GetComponent<Text>().text += "\n" + "RTVoice Invoke Speak: " + invokedSentenceToSpeak;
    }

    void invokeShowButton()
    {
        speakButton.gameObject.SetActive(true);
    }
    */
}
