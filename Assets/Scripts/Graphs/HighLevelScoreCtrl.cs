using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class HighLevelScoreCtrl : MonoBehaviour {
    
    //public GameObject orange;
    public CityMatrixRadarChart CMRadarChart;
    public Text innovationPotentialScore;
    public Text resourceEfficiencyScore;
    public Text totalScore;
    public Text innovationPotentialScoreSuggested;
    public Text resourceEfficiencyScoreSuggested;
    public Text totalScoreSuggested;
    public GameObject currentScores;
    public GameObject suggestedScores;
    public GameObject arrows;
    public CityDataCtrl cityDataCtrl;
    public Slider balance;
    public Text balanceText1;
    public Text balanceText2;

    void Update () {

        // visibility control
        if (cityDataCtrl.showAISuggestion)
        {
            //currentScores.transform.localPosition = new Vector3(-40.0f, 0.0f, 0.0f);
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
        float IPScore = (CMRadarChart.metrics[0] + CMRadarChart.metrics[1]) / 2.0f * 100.0f;
        innovationPotentialScore.text = string.Format("{0:0}", IPScore);

        float REScore = (CMRadarChart.metrics[2] + CMRadarChart.metrics[3]
            + CMRadarChart.metrics[4]) / 3.0f * 100.0f;
        resourceEfficiencyScore.text = string.Format("{0:0}", REScore);

        float TScore = IPScore * (1 - balance.value) + REScore * balance.value;
        totalScore.text = string.Format("{0:0}", TScore);

        balanceText1.text = string.Format("{0:0}", (1 - balance.value)*100.0f) + "%";
        balanceText2.text = string.Format("{0:0}", balance.value * 100.0f) + "%";

        // suggested
        float IPScoreSuggested = (CMRadarChart.metricsSuggested[0] + CMRadarChart.metricsSuggested[1]) / 2.0f * 100.0f;
        float IPDelta = IPScoreSuggested - IPScore;
        string IPDeltaStr;
        if (IPDelta >= 0.0f)
        {
            IPDeltaStr = "+" + string.Format("{0:0}", IPDelta);
        }
        else
        {
            IPDeltaStr = "-" + string.Format("{0:0}", - IPDelta);
        }
        innovationPotentialScoreSuggested.text = IPDeltaStr;

        float REScoreSuggested = (CMRadarChart.metricsSuggested[2] + CMRadarChart.metricsSuggested[3]
            + CMRadarChart.metricsSuggested[4]) / 3.0f * 100.0f;
        float REDelta = REScoreSuggested - REScore;
        string REDeltaStr;
        if (REDelta >= 0.0f)
        {
            REDeltaStr = "+" + string.Format("{0:0}", REDelta);
        }
        else
        {
            REDeltaStr = "-" + string.Format("{0:0}", -REDelta);
        }
        resourceEfficiencyScoreSuggested.text = REDeltaStr;


        float TSuggested = IPScoreSuggested * (1 - balance.value) + REScoreSuggested * balance.value;
        float TDelta = TSuggested - TScore;
        string TDeltaStr;
        if (TDelta >= 0.0f)
        {
            TDeltaStr = "+" + string.Format("{0:0}", TDelta);
        }
        else
        {
            TDeltaStr = "-" + string.Format("{0:0}", -TDelta);
        }
        totalScoreSuggested.text = TDeltaStr;
    }
}
