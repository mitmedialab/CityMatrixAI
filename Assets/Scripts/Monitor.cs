using UnityEngine;
using System.Collections;

public class Monitor : MonoBehaviour {

    public bool debug = false;
    public Color kinectColor = Color.green;
    public Color focusColor = Color.red;
    public Color frameColor = Color.blue;
    public float radius = 0.1f;
    public Vector3 otherPosition;
    Vector3 mainPosition;

	// Use this for initialization
	void Start () {
        this.mainPosition = this.transform.position;
        this.otherPosition = this.mainPosition + this.otherPosition;
	}

    void Update()
    {
        if(Input.GetButtonDown("SwitchView"))
        {
            if(this.transform.position == this.mainPosition)
            {
                this.transform.position = this.otherPosition;
            } else
            {
                this.transform.position = this.mainPosition;
            }
        }
    }
	
	// Update is called once per frame
	void OnDrawGizmos () {
        if (this.debug)
        {
            Gizmos.color = this.kinectColor;
            Gizmos.DrawSphere(this.transform.Find("Kinect").position, this.radius * 2);
            Gizmos.color = this.focusColor;
            Gizmos.DrawSphere(this.transform.Find("CameraFocus").position, this.radius * 2);
            Gizmos.color = this.frameColor;
            Transform prev = this.transform.Find("Frame").GetChild(3);
            foreach (Transform t in this.transform.Find("Frame"))
            {
                Gizmos.DrawSphere(t.position, this.radius);
                Gizmos.DrawLine(prev.position, t.position);
                prev = t;
            }
        }
    }
}
