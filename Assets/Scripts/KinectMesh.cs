using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinectMesh : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("KinectMesh"))
        {
            Debug.Log("Disabling/Enabling Kinect Mesh");
            foreach(Transform child in this.transform)
            {
                Debug.Log(child);
                child.gameObject.SetActive(!child.gameObject.activeSelf);
            }
        }
	}
}
