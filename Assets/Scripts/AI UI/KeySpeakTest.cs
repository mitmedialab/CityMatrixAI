using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Crosstales.RTVoice;
using UnityEngine.UI;

public class KeySpeakTest : MonoBehaviour {

    public GameObject RTVoice;
    public GameObject debugText;

	void Update ()
    {
        if (Input.GetKeyDown("space"))
        {
            string text = "Key Speak Test";
            RTVoice.GetComponent<LiveSpeaker>().Silence();
            RTVoice.GetComponent<LiveSpeaker>().Speak(text);
            debugText.GetComponent<Text>().text += "\n" + "RTVoice Live Speak: " + text;
        }
    }
}
