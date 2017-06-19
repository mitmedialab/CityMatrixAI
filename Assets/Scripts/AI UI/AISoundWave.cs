using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AISoundWave : MonoBehaviour
{
    // credit
    // based on
    // 
    // edited by Yan Zhang <ryanz@mit.edu>

    #region Variables
    public float Width = 0.075f;
    public float Spacing = 10.0f;
    public float Gain = 70.0f;
    public float MoveLeft = 200.0f;
    public float MoveDown = 100.0f;
    public int sampleSize = 512;
    public GameObject SoundWaveBarPrefab;

    private Transform tf;
    private Transform[] visualTransforms;

    #endregion
    

    #region MonoBehaviour methods

    void Start()
    {
        tf = transform;

        visualTransforms = new Transform[sampleSize / 2];

        GameObject tempPlane;

        for (int ii = 0; ii < sampleSize / 4; ii++)
        { //cut the upper frequencies

            // left to right
            tempPlane = Instantiate(SoundWaveBarPrefab, tf);
            tempPlane.transform.localScale = new Vector3(Width, 1.0f, Width);
            tempPlane.transform.localPosition = new Vector3(ii * Spacing - MoveLeft, -MoveDown, 0.0f);

            visualTransforms[ii] = tempPlane.GetComponent<Transform>();
            visualTransforms[ii].parent = tf;

            // right to left
            tempPlane = Instantiate(SoundWaveBarPrefab, tf);
            tempPlane.transform.localScale = new Vector3(Width, 1.0f, Width);
            tempPlane.transform.localPosition = new Vector3(-ii * Spacing + MoveLeft, -MoveDown, 0.0f);

            visualTransforms[ii + sampleSize / 4] = tempPlane.GetComponent<Transform>();
            visualTransforms[ii + sampleSize / 4].parent = tf;

            //tempPlane.GetComponent<Renderer>().material.color = Color.white;
        }
    }

    void Update()
    {
        float[] spectrum = new float[sampleSize];

        AudioListener.GetSpectrumData(spectrum, 0, FFTWindow.Rectangular);

        for (int ii = 0; ii < visualTransforms.Length / 2; ii++)
        {
            // left to right
            visualTransforms[ii].localScale = new Vector3(Width, 1.0f, Mathf.Max(spectrum[ii] * Gain, Width));

            // right to left
            visualTransforms[ii + visualTransforms.Length / 2].localScale = new Vector3(Width, 1.0f, Mathf.Max(spectrum[ii] * Gain, Width));
        }
    }

    #endregion

    /*
    void Update()
    {
        float[] spectrum = new float[sampleSize];

        AudioListener.GetSpectrumData(spectrum, 0, FFTWindow.Rectangular);

        for (int i = 1; i < spectrum.Length - 1; i++)
        {
            Vector3 start = new Vector3(Mathf.Log(i - 1) * 1.75f - 5.5f, spectrum[i - 1] * 50.0f - 1.6f, 10.0f);
            Vector3 end = new Vector3(Mathf.Log(i) * 1.75f - 5.5f, spectrum[i] * 50.0f - 1.6f, 10.0f);
            //Debug.DrawLine(start, end, Color.white);
            DrawLine(start, end, Color.white);
        }
    }

    void DrawLine(Vector3 start, Vector3 end, Color color, float width = 0.02f, float duration = 0.033f)
    {
        // credit
        // http://answers.unity3d.com/questions/8338/how-to-draw-a-line-using-script.html
        // https://docs.unity3d.com/Manual/class-LineRenderer.html
        GameObject myLine = new GameObject();
        myLine.transform.position = start;
        myLine.AddComponent<LineRenderer>();
        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        lr.material = new Material(Shader.Find("Particles/Alpha Blended Premultiply"));
        lr.startColor = color;
        lr.endColor = color; ;
        lr.startWidth = width;
        lr.endWidth = width;
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
        GameObject.Destroy(myLine, duration);
    }
    */

    /*
    // another draw line in UI
    // credit
    // http://answers.unity3d.com/questions/865927/draw-a-2d-line-in-the-new-ui.html
    Vector3 differenceVector = pointB - pointA;

    imageRectTransform.sizeDelta = new Vector2(differenceVector.magnitude, lineWidth);
    imageRectTransfom.pivot = new Vector2(0, 0.5f);
    imageRectTransform.position = pointA;
    float angle = Mathf.Atan2(differenceVector.y, differenceVector.x) * Mathf.Rad2Deg;
    imageRectTransform.Rotation = Quaternion.Euler(0,0, angle);
    */
}