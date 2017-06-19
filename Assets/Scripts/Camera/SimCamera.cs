using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimCamera : MonoBehaviour {

    public Transform focus;

	// Use this for initialization
	void Start () {
		
	}
	
    void OnPreRender()
    {
        this.transform.LookAt(focus);
    }

    // Update is called once per frame
    void Update () {

    }
}
