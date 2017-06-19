using UnityEngine;
using System.Collections;

public class MirrorCamera : MonoBehaviour {

    public Transform frameTopLeft;
    public Transform frameBottomLeft;
    public Transform frameTopRight;
    public Transform frameBottomRight;

    public Vector2 monitorDimensions;
    public Transform focusPoint;

    public bool drawNearCone, drawFrustum;

    private Camera cam;

    void Awake()
    {

    }

	// Use this for initialization
	void Start () {
        this.cam = this.GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 old = this.transform.position;
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        float depthical = Input.GetAxis("Depthical");
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        Vector3 delta = new Vector3(horizontal, vertical, depthical);

        this.transform.Translate(new Vector3(mouseX, mouseY, depthical));
        //this.transform.position = old + delta;
    }

    void LateUpdate()
    {
        //this.transform.LookAt(this.focusPoint);
        this.UpdateProjectionMatrix();
    }

    /// <summary>
    /// Source: hpjohn on http://forum.unity3d.com/threads/using-projection-matrix-to-create-holographic-effect.291123/
    /// </summary>
    void UpdateProjectionMatrix()
    {
        Vector3 pa, pb, pc, pd;
        pa = frameBottomLeft.position; //Bottom-Left
        pb = frameBottomRight.position; //Bottom-Right
        pc = frameTopLeft.position; //Top-Left
        pd = frameTopRight.position; //Top-Right

        Vector3 pe = this.cam.transform.position;// eye position

        Vector3 vr = (pb - pa).normalized; // right axis of screen
        Vector3 vu = (pc - pa).normalized; // up axis of screen
        Vector3 vn = Vector3.Cross(vr, vu).normalized; // normal vector of screen

        Vector3 va = pa - pe; // from pe to pa
        Vector3 vb = pb - pe; // from pe to pb
        Vector3 vc = pc - pe; // from pe to pc
        Vector3 vd = pd - pe; // from pe to pd

        float n = -this.focusPoint.InverseTransformPoint(this.cam.transform.position).z; // distance to the near clip plane (screen)
        float f = this.cam.farClipPlane; // distance of far clipping plane
        float d = Vector3.Dot(va, vn); // distance from eye to screen
        float l = Vector3.Dot(vr, va) * n / d; // distance to left screen edge from the 'center'
        float r = Vector3.Dot(vr, vb) * n / d; // distance to right screen edge from 'center'
        float b = Vector3.Dot(vu, va) * n / d; // distance to bottom screen edge from 'center'
        float t = Vector3.Dot(vu, vc) * n / d; // distance to top screen edge from 'center'

        Matrix4x4 p = new Matrix4x4(); // Projection matrix
        p[0, 0] = 2.0f * n / (r - l);
        p[0, 2] = (r + l) / (r - l);
        p[1, 1] = 2.0f * n / (t - b);
        p[1, 2] = (t + b) / (t - b);
        p[2, 2] = (f + n) / (n - f);
        p[2, 3] = 2.0f * f * n / (n - f);
        p[3, 2] = -1.0f;

        this.cam.projectionMatrix = p; // Assign matrix to camera

        if (drawNearCone)
        { //Draw lines from the camera to the corners f the screen
            Debug.DrawRay(this.cam.transform.position, va, Color.blue);
            Debug.DrawRay(this.cam.transform.position, vb, Color.blue);
            Debug.DrawRay(this.cam.transform.position, vc, Color.blue);
            Debug.DrawRay(this.cam.transform.position, vd, Color.blue);
        }

        if (drawFrustum) DrawFrustum(this.cam); //Draw actual camera frustum

    }

    /// <summary>
    /// Source: hpjohn on http://forum.unity3d.com/threads/using-projection-matrix-to-create-holographic-effect.291123/
    /// </summary>
    Vector3 ThreePlaneIntersection(Plane p1, Plane p2, Plane p3)
    { //get the intersection point of 3 planes
        return ((-p1.distance * Vector3.Cross(p2.normal, p3.normal)) +
                (-p2.distance * Vector3.Cross(p3.normal, p1.normal)) +
                (-p3.distance * Vector3.Cross(p1.normal, p2.normal))) /
            (Vector3.Dot(p1.normal, Vector3.Cross(p2.normal, p3.normal)));
    }

    /// <summary>
    /// Source: hpjohn on http://forum.unity3d.com/threads/using-projection-matrix-to-create-holographic-effect.291123/
    /// </summary>
    void DrawFrustum(Camera cam)
    {
        Vector3[] nearCorners = new Vector3[4]; //Approx'd nearplane corners
        Vector3[] farCorners = new Vector3[4]; //Approx'd farplane corners
        Plane[] camPlanes = GeometryUtility.CalculateFrustumPlanes(cam); //get planes from matrix
        Plane temp = camPlanes[1]; camPlanes[1] = camPlanes[2]; camPlanes[2] = temp; //swap [1] and [2] so the order is better for the loop

        for (int i = 0; i < 4; i++)
        {
            nearCorners[i] = ThreePlaneIntersection(camPlanes[4], camPlanes[i], camPlanes[(i + 1) % 4]); //near corners on the created projection matrix
            farCorners[i] = ThreePlaneIntersection(camPlanes[5], camPlanes[i], camPlanes[(i + 1) % 4]); //far corners on the created projection matrix
        }

        for (int i = 0; i < 4; i++)
        {
            Debug.DrawLine(nearCorners[i], nearCorners[(i + 1) % 4], Color.red, Time.deltaTime, false); //near corners on the created projection matrix
            Debug.DrawLine(farCorners[i], farCorners[(i + 1) % 4], Color.red, Time.deltaTime, false); //far corners on the created projection matrix
            Debug.DrawLine(nearCorners[i], farCorners[i], Color.red, Time.deltaTime, false); //sides of the created projection matrix
        }
    }

    void UpdateProjectionMatrixExperimental()
    {
        Matrix4x4 world2Cam = this.cam.worldToCameraMatrix;
        Vector4 TL = world2Cam.MultiplyPoint(this.frameTopLeft.position);
        Vector4 BL = world2Cam.MultiplyPoint(this.frameBottomLeft.position);
        Vector4 TR = world2Cam.MultiplyPoint(this.frameTopRight.position);
        Vector4 BR = world2Cam.MultiplyPoint(this.frameBottomRight.position);

        TL.w = Random.Range(5, 50);
        BL.w = Random.Range(5, 50);
        TR.w = Random.Range(5, 50);
        BR.w = Random.Range(5, 50);

        Matrix4x4 frameMatrix = new Matrix4x4();
        frameMatrix.SetColumn(0, TL);
        frameMatrix.SetColumn(1, BL);
        frameMatrix.SetColumn(2, TR);
        frameMatrix.SetColumn(3, BR);
        Matrix4x4 invFrame = frameMatrix.inverse;

        Debug.Log("Start");
        Debug.Log(frameMatrix);
        Debug.Log(invFrame);
        invFrame.SetColumn(3, new Vector4(0, 0, 0, 0));

        Matrix4x4 CornerMap = new Matrix4x4();
        CornerMap.SetRow(0, new Vector4(-1, -1, 1, 1));
        CornerMap.SetRow(1, new Vector4(1, -1, 1, -1));
        CornerMap.SetRow(2, new Vector4(-1, -1, -1, -1));
        CornerMap.SetRow(3, new Vector4(1, 1, 1, 1));

        Matrix4x4 newProjection = CornerMap * invFrame;
        newProjection.SetColumn(3, new Vector4(1, 2, 3, 4));
        Debug.Log(newProjection);

        this.cam.projectionMatrix = newProjection;
    }

    float calculateFOV()
    {
        float distance = Vector3.Distance(this.transform.position, this.focusPoint.position);
        return 2.0f * Mathf.Atan(monitorDimensions.y * 0.5f / distance) * Mathf.Rad2Deg;
    }
}
