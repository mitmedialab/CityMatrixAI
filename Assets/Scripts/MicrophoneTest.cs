using UnityEngine;
using System.Collections;

public class MicrophoneTest : MonoBehaviour
{
    void Start()
    {
        foreach (string device in Microphone.devices)
        {
            Debug.Log("Name: " + device);
        }
    }
}