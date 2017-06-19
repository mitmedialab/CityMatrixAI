using UnityEngine;
using System.Collections;

public class Display2 : MonoBehaviour {

    public bool secondDisplay = false;

	// Use this for initialization
	void Awake () {
        if (secondDisplay)
        {
            Display.displays[1].Activate();
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
