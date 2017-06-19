using UnityEngine;
using System.Collections;

public class StreetViewCamera : MonoBehaviour {

    private Camera cam;

	// Use this for initialization
	void Start () {
        this.cam = this.GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    // EXAMPLE WITH this.cam UPSIDEDOWN
    void OnPreCull()
    {
        this.cam.ResetWorldToCameraMatrix();
        this.cam.ResetProjectionMatrix();
        this.cam.projectionMatrix = this.cam.projectionMatrix * Matrix4x4.Scale(new Vector3(-1, 1, 1));
    }

    void OnPreRender()
    {
        GL.SetRevertBackfacing(true);
    }

    void OnPostRender()
    {
        GL.SetRevertBackfacing(false);
    }

}
