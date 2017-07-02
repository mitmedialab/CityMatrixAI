using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityMatrixRadarChart : MonoBehaviour {

    public float[] metrics;
    public GameObject centerRefPt;
    public GameObject[] endRefPts;
    public GameObject[] movingRefPts;
    public GameObject currentLineDensityDiversity;
    public GameObject currentLineEnergyTrafficSolar;
    public GameObject currentDashLineDiversityEnergy;
    public GameObject currentDashLineSolarDensity;

    void Update () {

        // update moving ref pts' locations
        for (int i = 0; i < metrics.Length; i++)
        {
            Vector3 start = centerRefPt.transform.position;
            Vector3 end = endRefPts[i].transform.position;
            Vector3 v = (end - start) * metrics[i];
            Vector3 p = start + v;
            movingRefPts[i].transform.position = p;
        }

        // update current lines
        currentLineDensityDiversity.GetComponent<LineRenderer>().SetPosition(0, movingRefPts[0].transform.position);
        currentLineDensityDiversity.GetComponent<LineRenderer>().SetPosition(1, movingRefPts[1].transform.position);
        currentLineEnergyTrafficSolar.GetComponent<LineRenderer>().SetPosition(0, movingRefPts[2].transform.position);
        currentLineEnergyTrafficSolar.GetComponent<LineRenderer>().SetPosition(1, movingRefPts[3].transform.position);
        currentLineEnergyTrafficSolar.GetComponent<LineRenderer>().SetPosition(2, movingRefPts[4].transform.position);

        // update current dash lines
        currentDashLineDiversityEnergy.GetComponent<LineRenderer>().SetPosition(0, movingRefPts[1].transform.position);
        currentDashLineDiversityEnergy.GetComponent<LineRenderer>().SetPosition(1, movingRefPts[2].transform.position);
        currentDashLineSolarDensity.GetComponent<LineRenderer>().SetPosition(0, movingRefPts[4].transform.position);
        currentDashLineSolarDensity.GetComponent<LineRenderer>().SetPosition(1, movingRefPts[0].transform.position);

    }
    
}
