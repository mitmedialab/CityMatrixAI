using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCamera : MonoBehaviour {

	public Transform focus;
	public float scrollMultiplier;

    Vector3 original;
    Vector3 focusOriginal;
	// Use this for initialization
	void Start () {
        original = transform.position;
        focusOriginal = focus.transform.position;
	}

	// Update is called once per frame
	void Update () {
		float x = Input.GetAxis("Mouse X");
		float y = Input.GetAxis("Mouse Y");
		float z = Input.GetAxis("Mouse ScrollWheel") * scrollMultiplier;
		Vector3 delta = new Vector3(x, y, 0);
        if (Input.GetButton("Fire1"))
        {
            this.transform.Translate(delta, Space.Self);
            this.transform.LookAt(focus);
            focus.eulerAngles = this.transform.eulerAngles;
        }
        else if (Input.GetButton("Fire2"))
        {
            this.transform.Translate(delta, Space.Self);
            focus.Translate(delta, Space.Self);
        }
        this.transform.Translate(new Vector3(0, 0, z), Space.Self);
        if (Input.GetButtonDown("Reset"))
        {
            this.transform.position = original;
            this.focus.position = focusOriginal;
            this.transform.LookAt(focus);
        }
	}

	void OnPreRender() {
	}
}
