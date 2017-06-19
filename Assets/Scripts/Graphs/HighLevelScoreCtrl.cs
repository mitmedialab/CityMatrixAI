using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class HighLevelScoreCtrl : MonoBehaviour {

	[Range(0, 1f)]
	public float[] values;

    public GameObject orange;
    public Text innovationPotentialScore;
    public Text resourceEfficiencyScore;

    void Update () {
        float IPScore = (orange.GetComponent<RadarChartCtrl>().values[0] 
            + orange.GetComponent<RadarChartCtrl>().values[1]) / 2.0f * 100.0f;
        innovationPotentialScore.text = string.Format("{0:0}", IPScore);

        float REScore = (orange.GetComponent<RadarChartCtrl>().values[2]
            + orange.GetComponent<RadarChartCtrl>().values[3]
            + orange.GetComponent<RadarChartCtrl>().values[4]) / 3.0f * 100.0f;
        resourceEfficiencyScore.text = string.Format("{0:0}", REScore);
    }
}
