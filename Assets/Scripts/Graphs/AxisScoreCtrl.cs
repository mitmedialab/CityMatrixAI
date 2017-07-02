using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class AxisScoreCtrl : MonoBehaviour {
    
    //public GameObject orange;
    public CityMatrixRadarChart CMRadarChart;
    public Text[] currentAxises;
    public Text[] suggestedAxises;
    public GameObject currentScores;
    public GameObject suggestedScores;
    public GameObject arrows;
    public CityDataCtrl cityDataCtrl;

    void Update () {

        // visibility control
        if (cityDataCtrl.showAISuggestion)
        {
            //currentScores.transform.localPosition = new Vector3(-20.0f, 0.0f, 0.0f);
            suggestedScores.SetActive(true);
            //arrows.SetActive(true);
        }
        else
        {
            //currentScores.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
            suggestedScores.SetActive(false);
            //arrows.SetActive(false);
        }

        // current
        for (int i = 0; i < currentAxises.Length; i ++)
        {
            currentAxises[i].text = string.Format("{0:0}", CMRadarChart.metrics[i] * 100f);
        }

        // suggested
        for (int i = 0; i < suggestedAxises.Length; i++)
        {
            float delta = (CMRadarChart.metricsSuggested[i] - CMRadarChart.metrics[i]) * 100f;
            string deltaStr;
            if (delta >= 0.0f)
            {
                deltaStr = "+" + string.Format("{0:0}", delta);
            }
            else
            {
                deltaStr = "-" + string.Format("{0:0}", - delta);
            }
            suggestedAxises[i].text = deltaStr;
        }

    }
}
