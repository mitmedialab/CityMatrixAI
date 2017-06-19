using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIComponentOnOffCtrl : MonoBehaviour {
    
    public GameObject[] masks;
    public GameObject[] highlights;

    public int intAIStep = -1;
    public int prevIntAIStep = -1;

    void Start () {
        resetAll();
    }

    void Update ()
    {
        intAIStep = GetComponent<AIStepCtrl>().intAIStep;

        if (intAIStep != prevIntAIStep)
        {
            resetAll();

            if (intAIStep == 6)
            {
                // (highlight: 3D city model)
                masks[0].SetActive(false); // mask city 3d
                highlights[0].SetActive(true); // hightlight city 3d
            }
            else if (intAIStep == 8)
            {
                // (highlight: radar-chart)
                masks[1].SetActive(false); // mask radar-chart upper
                masks[2].SetActive(false); // maskradar-chart lower
                highlights[1].SetActive(true); // hightlight radar-chart all
            }
            else if (intAIStep == 9)
            {
                // (highlight: radar-chart upper part)
                masks[1].SetActive(false); // mask radar-chart upper
                highlights[2].SetActive(true); // hightlight radar-chart upper
            }
            else if (intAIStep == 10)
            {
                // (highlight: radar-chart lower part)
                masks[2].SetActive(false); // mask radar-chart lower
                highlights[3].SetActive(true); // hightlight radar-chart lower
            }
            else if (intAIStep == 12)
            {
                // (highlight: radar-chart "Population Density" part)
                masks[1].SetActive(false); // mask radar-chart upper
                masks[2].SetActive(false); // maskradar-chart lower
                highlights[4].SetActive(true); // hightlight radar-chart axis 1
            }
            else if (intAIStep == 13)
            {
                // (highlight: radar-chart "Population Diversity" part)
                masks[1].SetActive(false); // mask radar-chart upper
                masks[2].SetActive(false); // maskradar-chart lower
                highlights[5].SetActive(true); // hightlight radar-chart axis 2
            }
            else if (intAIStep == 14)
            {
                // (highlight: radar-chart "Energy and Cost Efficiency" part)
                masks[1].SetActive(false); // mask radar-chart upper
                masks[2].SetActive(false); // maskradar-chart lower
                highlights[6].SetActive(true); // hightlight radar-chart axis 3
            }
            else if (intAIStep == 15)
            {
                // (highlight: radar-chart "Traffic Performance" part)
                masks[1].SetActive(false); // mask radar-chart upper
                masks[2].SetActive(false); // maskradar-chart lower
                highlights[7].SetActive(true); // hightlight radar-chart axis 4
            }
            else if (intAIStep == 16)
            {
                // (highlight: radar-chart "Solar Access Performance" part)
                masks[1].SetActive(false); // mask radar-chart upper
                masks[2].SetActive(false); // maskradar-chart lower
                highlights[8].SetActive(true); // hightlight radar-chart axis 5
            }
            else if (intAIStep == 17)
            {
                // (mask: none; highlight: none)
                maskNone();
            }
            else if (intAIStep > 17)
            {
                // (mask: none; highlight: none)
                maskNone();
            }

            prevIntAIStep = intAIStep;
        }
    }

    void resetAll()
    {
        foreach (GameObject mask in masks)
        {
            mask.SetActive(true);
        }
        foreach (GameObject highlight in highlights)
        {
            highlight.SetActive(false);
        }
    }

    void maskNone()
    {
        foreach (GameObject mask in masks)
        {
            mask.SetActive(false);
        }
        foreach (GameObject highlight in highlights)
        {
            highlight.SetActive(false);
        }
    }
}
