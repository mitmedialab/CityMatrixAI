using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CityMatrixRadarChart : MonoBehaviour {
    
    // history ctrl
    [Range(0, 2)]
    public int showHistoryStep = 2;

    // shared
    public GameObject centerRefPt;
    public GameObject[] endRefPts;

    // current
    public GameObject[] movingRefPts;
    [Range(0, 1f)]
    public float[] metrics;
    public GameObject currentLineDensityDiversity;
    public GameObject currentLineEnergyTrafficSolar;
    public GameObject currentDashLineDiversityEnergy;
    public GameObject currentDashLineSolarDensity;
    public GameObject currentFillDensityDiversity;
    public GameObject currentFillEnergyTraffic;
    public GameObject currentFillTrafficSolar;
    public GameObject currentFillDiversityEnergy;
    public GameObject currentFillSolarDensity;

    //private float[] prev = new float[5];
    private Vector3[] movingPts = new Vector3[5];

    // suggested
    public GameObject[] suggestedMovingRefPts;
    [Range(0, 1f)]
    public float[] metricsSuggested;
    public GameObject suggestedLineDensityDiversity;
    public GameObject suggestedLineEnergyTrafficSolar;
    public GameObject suggestedDashLineDiversityEnergy;
    public GameObject suggestedDashLineSolarDensity;

    private float[] prevSuggested = new float[5];
    private Vector3[] movingPtsSuggested = new Vector3[5];

    // history0
    public GameObject history0LineDensityDiversity;
    public GameObject history0LineEnergyTrafficSolar;
    public GameObject history0DashLineDiversityEnergy;
    public GameObject history0DashLineSolarDensity;

    private float[] metricsHistory0 = new float[5];
    private Vector3[] movingPtsHistory0 = new Vector3[5];

    // history1
    public GameObject history1LineDensityDiversity;
    public GameObject history1LineEnergyTrafficSolar;
    public GameObject history1DashLineDiversityEnergy;
    public GameObject history1DashLineSolarDensity;

    private float[] metricsHistory1 = new float[5];
    private Vector3[] movingPtsHistory1 = new Vector3[5];


    void Start()
    {
        metricsHistory0 = (float[])metrics.Clone();
        metricsHistory1 = (float[])metrics.Clone();
    }


    void Update() {

        // how many steps of the history do we show
        if (showHistoryStep == 1)
        {
            // history 0
            history0LineDensityDiversity.SetActive(true);
            history0LineEnergyTrafficSolar.SetActive(true);
            history0DashLineDiversityEnergy.SetActive(true);
            history0DashLineSolarDensity.SetActive(true);
            // history 1
            history1LineDensityDiversity.SetActive(false);
            history1LineEnergyTrafficSolar.SetActive(false);
            history1DashLineDiversityEnergy.SetActive(false);
            history1DashLineSolarDensity.SetActive(false);
        }
        else if (showHistoryStep == 2)
        {
            // history 0
            history0LineDensityDiversity.SetActive(true);
            history0LineEnergyTrafficSolar.SetActive(true);
            history0DashLineDiversityEnergy.SetActive(true);
            history0DashLineSolarDensity.SetActive(true);
            // history 1
            history1LineDensityDiversity.SetActive(true);
            history1LineEnergyTrafficSolar.SetActive(true);
            history1DashLineDiversityEnergy.SetActive(true);
            history1DashLineSolarDensity.SetActive(true);
        }
        else
        {
            // history 0
            history0LineDensityDiversity.SetActive(false);
            history0LineEnergyTrafficSolar.SetActive(false);
            history0DashLineDiversityEnergy.SetActive(false);
            history0DashLineSolarDensity.SetActive(false);
            // history 1
            history1LineDensityDiversity.SetActive(false);
            history1LineEnergyTrafficSolar.SetActive(false);
            history1DashLineDiversityEnergy.SetActive(false);
            history1DashLineSolarDensity.SetActive(false);
        }

        // when there's an update
        if (!metrics.SequenceEqual(metricsHistory0))
        {
            
            // CURRENT
            // update moving ref pts' locations
            for (int i = 0; i < metrics.Length; i++)
            {
                Vector3 start = centerRefPt.transform.position;
                Vector3 end = endRefPts[i].transform.position;
                Vector3 v = (end - start) * metrics[i];
                Vector3 p = start + v;
                movingPts[i] = p;
                // moving ref pt gameobjects for fill mesh
                movingRefPts[i].transform.position = p;
            }

            // update lines
            currentLineDensityDiversity.GetComponent<LineRenderer>().SetPosition(0, movingRefPts[0].transform.position);
            currentLineDensityDiversity.GetComponent<LineRenderer>().SetPosition(1, movingPts[1]);
            currentLineEnergyTrafficSolar.GetComponent<LineRenderer>().SetPosition(0, movingPts[2]);
            currentLineEnergyTrafficSolar.GetComponent<LineRenderer>().SetPosition(1, movingPts[3]);
            currentLineEnergyTrafficSolar.GetComponent<LineRenderer>().SetPosition(2, movingPts[4]);

            // update dash lines
            currentDashLineDiversityEnergy.GetComponent<LineRenderer>().SetPosition(0, movingPts[1]);
            currentDashLineDiversityEnergy.GetComponent<LineRenderer>().SetPosition(1, movingPts[2]);
            currentDashLineSolarDensity.GetComponent<LineRenderer>().SetPosition(0, movingPts[4]);
            currentDashLineSolarDensity.GetComponent<LineRenderer>().SetPosition(1, movingPts[0]);


            // HISTORY0
            // update moving ref pts' locations
            for (int i = 0; i < metricsHistory0.Length; i++)
            {
                Vector3 start = centerRefPt.transform.position;
                Vector3 end = endRefPts[i].transform.position;
                Vector3 v = (end - start) * metricsHistory0[i];
                Vector3 p = start + v;
                movingPtsHistory0[i] = p;
            }
            //Debug.Log(movingPtsHistory0[0]);

            // update lines
            history0LineDensityDiversity.GetComponent<LineRenderer>().SetPosition(0, movingPtsHistory0[0]);
            history0LineDensityDiversity.GetComponent<LineRenderer>().SetPosition(1, movingPtsHistory0[1]);
            history0LineEnergyTrafficSolar.GetComponent<LineRenderer>().SetPosition(0, movingPtsHistory0[2]);
            history0LineEnergyTrafficSolar.GetComponent<LineRenderer>().SetPosition(1, movingPtsHistory0[3]);
            history0LineEnergyTrafficSolar.GetComponent<LineRenderer>().SetPosition(2, movingPtsHistory0[4]);

            // update dash lines
            history0DashLineDiversityEnergy.GetComponent<LineRenderer>().SetPosition(0, movingPtsHistory0[1]);
            history0DashLineDiversityEnergy.GetComponent<LineRenderer>().SetPosition(1, movingPtsHistory0[2]);
            history0DashLineSolarDensity.GetComponent<LineRenderer>().SetPosition(0, movingPtsHistory0[4]);
            history0DashLineSolarDensity.GetComponent<LineRenderer>().SetPosition(1, movingPtsHistory0[0]);


            // HISTORY1
            // update moving ref pts' locations
            for (int i = 0; i < metricsHistory1.Length; i++)
            {
                Vector3 start = centerRefPt.transform.position;
                Vector3 end = endRefPts[i].transform.position;
                Vector3 v = (end - start) * metricsHistory1[i];
                Vector3 p = start + v;
                movingPtsHistory1[i] = p;
            }
            //Debug.Log(movingPtsHistory1[0]);

            // update lines
            history1LineDensityDiversity.GetComponent<LineRenderer>().SetPosition(0, movingPtsHistory1[0]);
            history1LineDensityDiversity.GetComponent<LineRenderer>().SetPosition(1, movingPtsHistory1[1]);
            history1LineEnergyTrafficSolar.GetComponent<LineRenderer>().SetPosition(0, movingPtsHistory1[2]);
            history1LineEnergyTrafficSolar.GetComponent<LineRenderer>().SetPosition(1, movingPtsHistory1[3]);
            history1LineEnergyTrafficSolar.GetComponent<LineRenderer>().SetPosition(2, movingPtsHistory1[4]);

            // update dash lines
            history1DashLineDiversityEnergy.GetComponent<LineRenderer>().SetPosition(0, movingPtsHistory1[1]);
            history1DashLineDiversityEnergy.GetComponent<LineRenderer>().SetPosition(1, movingPtsHistory1[2]);
            history1DashLineSolarDensity.GetComponent<LineRenderer>().SetPosition(0, movingPtsHistory1[4]);
            history1DashLineSolarDensity.GetComponent<LineRenderer>().SetPosition(1, movingPtsHistory1[0]);


            // UPDATE HISTORY MATRICS
            metricsHistory1 = (float[])metricsHistory0.Clone();
            metricsHistory0 = (float[])metrics.Clone();

        }


        // SUGGESTED
        if (!metricsSuggested.SequenceEqual(prevSuggested) || prevSuggested == null)
        {
            prevSuggested = (float[])metricsSuggested.Clone();

            // update moving ref pts' locations
            for (int i = 0; i < metricsSuggested.Length; i++)
            {
                Vector3 start = centerRefPt.transform.position;
                Vector3 end = endRefPts[i].transform.position;
                Vector3 v = (end - start) * metricsSuggested[i];
                Vector3 p = start + v + transform.TransformVector(new Vector3(0.0f, 0.0f, 1.0f));
                movingPtsSuggested[i] = p;
                // moving ref pt gameobjects for fill mesh
                suggestedMovingRefPts[i].transform.position = p;
            }

            // update lines
            suggestedLineDensityDiversity.GetComponent<LineRenderer>().SetPosition(0, movingPtsSuggested[0]);
            suggestedLineDensityDiversity.GetComponent<LineRenderer>().SetPosition(1, movingPtsSuggested[1]);
            suggestedLineEnergyTrafficSolar.GetComponent<LineRenderer>().SetPosition(0, movingPtsSuggested[2]);
            suggestedLineEnergyTrafficSolar.GetComponent<LineRenderer>().SetPosition(1, movingPtsSuggested[3]);
            suggestedLineEnergyTrafficSolar.GetComponent<LineRenderer>().SetPosition(2, movingPtsSuggested[4]);

            // update dash lines
            suggestedDashLineDiversityEnergy.GetComponent<LineRenderer>().SetPosition(0, movingPtsSuggested[1]);
            suggestedDashLineDiversityEnergy.GetComponent<LineRenderer>().SetPosition(1, movingPtsSuggested[2]);
            suggestedDashLineSolarDensity.GetComponent<LineRenderer>().SetPosition(0, movingPtsSuggested[4]);
            suggestedDashLineSolarDensity.GetComponent<LineRenderer>().SetPosition(1, movingPtsSuggested[0]);

        }

    }

    /*
    void OnValidate()
    {

        // current
        if (!metrics.SequenceEqual(prev))
        {
            prev = (float[])metrics.Clone();

            // update moving ref pts' locations
            for (int i = 0; i < metrics.Length; i++)
            {
                Vector3 start = centerRefPt.transform.position;
                Vector3 end = endRefPts[i].transform.position;
                Vector3 v = (end - start) * metrics[i];
                Vector3 p = start + v;
                movingPts[i] = p;
                // moving ref pt gameobjects for fill mesh
                movingRefPts[i].transform.position = p;
            }

            // update current lines
            currentLineDensityDiversity.GetComponent<LineRenderer>().SetPosition(0, movingPts[0]);
            currentLineDensityDiversity.GetComponent<LineRenderer>().SetPosition(1, movingPts[1]);
            currentLineEnergyTrafficSolar.GetComponent<LineRenderer>().SetPosition(0, movingPts[2]);
            currentLineEnergyTrafficSolar.GetComponent<LineRenderer>().SetPosition(1, movingPts[3]);
            currentLineEnergyTrafficSolar.GetComponent<LineRenderer>().SetPosition(2, movingPts[4]);

            // update current dash lines
            currentDashLineDiversityEnergy.GetComponent<LineRenderer>().SetPosition(0, movingPts[1]);
            currentDashLineDiversityEnergy.GetComponent<LineRenderer>().SetPosition(1, movingPts[2]);
            currentDashLineSolarDensity.GetComponent<LineRenderer>().SetPosition(0, movingPts[4]);
            currentDashLineSolarDensity.GetComponent<LineRenderer>().SetPosition(1, movingPts[0]);

            // RZ 170702 not working yet, need to initiate mesh first
            // update fill GameObjects
            //currentFillDensityDiversity.GetComponent<MeshTriangle>().Update();
            //currentFillEnergyTraffic.GetComponent<MeshTriangle>().Update();
            //currentFillTrafficSolar.GetComponent<MeshTriangle>().Update();
            

        }

        // suggested
        if (!metricsSuggested.SequenceEqual(prevSuggested))
        {
            prevSuggested = (float[])metricsSuggested.Clone();

            // update moving ref pts' locations
            for (int i = 0; i < metricsSuggested.Length; i++)
            {
                Vector3 start = centerRefPt.transform.position;
                Vector3 end = endRefPts[i].transform.position;
                Vector3 v = (end - start) * metricsSuggested[i];
                Vector3 p = start + v + transform.TransformVector(new Vector3(0.0f, 0.0f, 1.0f));
                movingPtsSuggested[i] = p;
                // moving ref pt gameobjects for fill mesh
                suggestedMovingRefPts[i].transform.position = p;
            }

            // update current lines
            suggestedLineDensityDiversity.GetComponent<LineRenderer>().SetPosition(0, movingPtsSuggested[0]);
            suggestedLineDensityDiversity.GetComponent<LineRenderer>().SetPosition(1, movingPtsSuggested[1]);
            suggestedLineEnergyTrafficSolar.GetComponent<LineRenderer>().SetPosition(0, movingPtsSuggested[2]);
            suggestedLineEnergyTrafficSolar.GetComponent<LineRenderer>().SetPosition(1, movingPtsSuggested[3]);
            suggestedLineEnergyTrafficSolar.GetComponent<LineRenderer>().SetPosition(2, movingPtsSuggested[4]);

            // update current dash lines
            suggestedDashLineDiversityEnergy.GetComponent<LineRenderer>().SetPosition(0, movingPtsSuggested[1]);
            suggestedDashLineDiversityEnergy.GetComponent<LineRenderer>().SetPosition(1, movingPtsSuggested[2]);
            suggestedDashLineSolarDensity.GetComponent<LineRenderer>().SetPosition(0, movingPtsSuggested[4]);
            suggestedDashLineSolarDensity.GetComponent<LineRenderer>().SetPosition(1, movingPtsSuggested[0]);

        }

    }
    */
}
