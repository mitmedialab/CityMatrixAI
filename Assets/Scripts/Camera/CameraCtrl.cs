using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrl : MonoBehaviour {

	public enum CAM
    {
		MIRROR,SIM,MOUSE,FIXED
	}

	public CAM selected;
	public Transform mirrorCamera;
	public Transform simCamera;
    public Transform mouseCamera;
    public Transform fixedCamera;

    private int initiate = 0; 

    // Use this for initialization
    void Start ()
    {
    }

	// Update is called once per frame
	void Update ()
    {
        if (initiate == 0)
        {
            // initiate black skybox
            fixedCamera.GetComponent<Camera>().enabled = false;
            mirrorCamera.GetComponent<Camera>().enabled = false;
            mouseCamera.GetComponent<Camera>().enabled = true;
            simCamera.GetComponent<Camera>().enabled = false;
            selected = CAM.MOUSE;
            initiate++;
        }
        else if (initiate == 1)
        {
            // switch to fixed camera
            fixedCamera.GetComponent<Camera>().enabled = true;
            mirrorCamera.GetComponent<Camera>().enabled = false;
            mouseCamera.GetComponent<Camera>().enabled = false;
            simCamera.GetComponent<Camera>().enabled = false;
            selected = CAM.FIXED;
            initiate++;
        }

        if (Input.GetButtonDown("ChangeCam"))
        {
			if (selected == CAM.FIXED)
            {
                fixedCamera.GetComponent<Camera>().enabled = false;
                mirrorCamera.GetComponent<Camera>().enabled = true;
                mouseCamera.GetComponent<Camera>().enabled = false;
                simCamera.GetComponent<Camera>().enabled = false;
                selected = CAM.MIRROR;
			}
            else if (selected == CAM.MIRROR)
            {
                fixedCamera.GetComponent<Camera>().enabled = false;
                mirrorCamera.GetComponent<Camera>().enabled = false;
                mouseCamera.GetComponent<Camera>().enabled = true;
                simCamera.GetComponent<Camera>().enabled = false;
                selected = CAM.MOUSE;
            }
            else if (selected == CAM.MOUSE)
            {
                fixedCamera.GetComponent<Camera>().enabled = true;
                mirrorCamera.GetComponent<Camera>().enabled = false;
                mouseCamera.GetComponent<Camera>().enabled = false;
                simCamera.GetComponent<Camera>().enabled = false;
                selected = CAM.FIXED;
            }
        }
	}
}
