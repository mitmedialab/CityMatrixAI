using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightAnim : MonoBehaviour {

    public float speed = 2.0f;
    public float deltaScale = 0.1f;

    void Update()
    {
        float s = 1.0f + Mathf.Sin(Time.timeSinceLevelLoad * speed) * deltaScale;
        this.transform.localScale = new Vector3(s, s, s);
    }
    
}