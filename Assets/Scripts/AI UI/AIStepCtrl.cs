using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AIStepCtrl : MonoBehaviour {
    
    public int intAIStep = -1;
    public int prevIntAIStep = -1;

    //RZ 170615 not using a seperate udp receiver anymore
    //public UDPReceive udpReceive;
    //private static string AIStepString;  //  A new Static variable to hold our score.

    public CityDataCtrl cityDataCtrl;
    
    void Start () {
        //intAIStep = -1;
    }
	
	// Update is called once per frame
	void Update ()
    {
        /* RZ 170615 not using a seperate udp receiver anymore
        AIStepString = udpReceive.AIStepString;
        //Debug.Log("AIStepString: " + AIStepString);

        // updated by UDP from RH CV
        if (AIStepString != "")
        {
            if (Convert.ToInt32(AIStepString) >= -1)
            {
                intAIStep = Convert.ToInt32(AIStepString);
            }
        }
        */

        //RZ 170615 recieved in the whole output city JSON
        intAIStep = cityDataCtrl.AIStep;

        // inAIStep updated from Python server > RH VIZ or Speech Recognition
        if (intAIStep != prevIntAIStep)
        {
            GetComponent<SpeakRTVoice>().SendMessage("speakButtonOnClick");
            prevIntAIStep = intAIStep;
        }

        //intAIStep = GetComponent<SpeakRecordedVoice>().AIstep;
    }
}
