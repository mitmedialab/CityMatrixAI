﻿using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;
using System;
using UnityEngine.UI.Extensions;
using cakeslice;
using System.Collections.Generic;

public class CityDataCtrl : MonoBehaviour
{

    public VirtualCityModel cityModel;
    public string JsonURL;

    //private JSONCityMatrix oldData; RZ 170615
    private BuildingModel[,] city;

    public UnityEngine.UI.Extensions.RadarPolygon chart;

    public GameObject radarChartOrange;

    public bool showAISuggestion = false;
    public bool highlightAI = true;

    //private StreamReader sReader;
    //private string readJson;

    private static string udpString;  //  A new Static variable to hold our score.

    //RZ 170615 expose data to AIStepCtrl
    public JSONCityMatrixMLAI data;
    public int AIStep = -1;
    public int animBlink = 0;

    public GameObject mainView;

    // Use this for initialization
    void Start () {
        //StartCoroutine("Initialize");
        city = cityModel.GetCity();
        //sReader = new StreamReader("C:\\Users\\RYAN\\Dropbox (MIT)\01_Work\\MAS\\06_Fall 2016\\CityMatrix\\01_Software\\Processing\\Colortizer\\all.json");
        //sReader = new StreamReader("C:/Users/RYAN/Dropbox (MIT)/01_Work/MAS/06_Fall 2016/CityMatrix/01_Software/Processing/Colortizer/all.json");

        /* RZ 170615 try not do parsing json in the start, not necessary
        //udpString = UDPReceive.udpString;  //  Update our score continuously.
        //udpString = ((UDPReceive)this.GetComponent("UDPReceive")).udpString;
        udpString = GetComponent<UDPReceive>().udpString;
        //Debug.Log(udpString);
        //JSONCityMatrix data = JsonUtility.FromJson<JSONCityMatrix>(udpString);
        data = JsonUtility.FromJson<JSONCityMatrixMLAI>(udpString);
        //Debug.Log(data.predict);

        foreach (JSONBuilding b in data.predict.grid)
        {
            b.Correct(15, 15);
            city[b.x, b.y].JSONUpdate(b);
        }
        //BuildingDataCtrl.instance.UpdateDensities(data.objects.density);
        

        this.oldData = data.predict;
        */
    }

    Queue<GameObject> highlightsToClear = new Queue<GameObject>();
    Queue<GameObject> objectsToHighlight = new Queue<GameObject>();
    int i = 0;
    // Update is called once per frame
    void Update () {
        /*
        if (i % 30 == 0)
        {
            chart.value[0] = (float)Math.Sin(i);
        }
        i++;
        */

        if (GetComponent<UDPReceive>().fresh)
        {
            udpString = GetComponent<UDPReceive>().udpString;
            GetComponent<UDPReceive>().fresh = false;
        } else
        {
            return;
        }
        //Debug.Log(udpString);
        //JSONCityMatrix data = JsonUtility.FromJson<JSONCityMatrix>(udpString);
        data = JsonUtility.FromJson<JSONCityMatrixMLAI>(udpString);
        //Debug.Log(data.predict);
        if (data == null) return;

        //RZ 170615 AIStep and animBlink control
        AIStep = data.ai.objects.AIStep;
        animBlink = data.ai.objects.animBlink;

        //RZ 190621 show AI suggestion and highlight or not
        if (AIStep == 20)
        {
            showAISuggestion = true;
        }
        else
        {
            showAISuggestion = false;
        }
        if (showAISuggestion)
        {
            highlightAI = true;
        }
        else
        {
            animBlink = 0;
            highlightAI = false;
        }
        
        JSONCityMatrix mlOrAiCity;
        JSONCityMatrix otherCity;

        float[] mlOrAiScores;
        RadarPolygon rpOutline = radarChartOrange.transform.GetChild(0).GetComponent<RadarPolygon>();
        RadarPolygon rpFill = radarChartOrange.transform.GetChild(1).GetComponent<RadarPolygon>();
        if (animBlink == 0)
        {
            mlOrAiCity = data.predict;
            otherCity = data.ai;
            mlOrAiScores = data.predict.objects.scores;
            rpOutline.color = Color.red;
            rpFill.color = new Color(1.0f, 0.0f, 0.0f, 0.25f);
        }
        else
        {
            mlOrAiCity = data.ai;
            otherCity = data.predict;
            mlOrAiScores = data.ai.objects.scores;
            rpOutline.color = Color.green;
            rpFill.color = new Color(0.0f, 1.0f, 0.0f, 0.25f);
        }

        if (highlightAI == false)
        {
            rpOutline.color = new Color(240.0f / 256.0f, 170.0f / 256.0f, 0.0f, 1.0f);
            rpFill.color = new Color(240.0f / 256.0f, 170.0f / 256.0f, 0.0f, 0.25f);
        }

        var mainDens = mlOrAiCity.objects.densities;
        var otherDens = otherCity.objects.densities;
        for (int i = 0; i < mlOrAiCity.grid.Length; i++)
        {
            JSONBuilding a = mlOrAiCity.grid[i];
            a.Correct(15, 15);
            city[a.x, a.y].JSONUpdate(a);

            if(highlightAI)
            {
                JSONBuilding o = otherCity.grid[i];
                o.Correct(15, 15);
                if (a.type != o.type) Debug.Log(a);
                if (a.Changes(o) ||
                    (a.type != -1 && o.type != -1 && a.type < mainDens.Length && o.type < otherDens.Length && (mainDens[a.type] != otherDens[o.type])))
                {
                    foreach (var b in city[a.x, a.y].views)
                    {
                        if (b.ViewType == Building.Type.MESH)
                        {
                            objectsToHighlight.Enqueue(b.gameObject);
                            break;
                        }
                    }
                }
            }
        }
        // update densities
        BuildingDataCtrl.instance.UpdateDensities(mlOrAiCity.objects.densities);


        //RZ 17015 update radar chart values
        //radarChartOrange.GetComponent<RadarChartCtrl>().values[0] = mlOrAiCity.objects.popDensity;
        for (int i = 0; i< mlOrAiScores.Length; i++)
        {
            mlOrAiScores[i] = Mathf.Max(0.01f, Mathf.Min(1.0f, mlOrAiScores[i]));
        }
        radarChartOrange.GetComponent<RadarChartCtrl>().values = mlOrAiScores;


        //RZ clear up all prev highlight
        foreach (Transform child in mainView.transform)
        {
            GameObject ot = child.GetChild(1).GetChild(0).gameObject;
            Debug.Log(ot);
            BuildingHighlighter.RemoveHighlight(ot);
        }

        if (highlightAI)
        {
            for (int idx = 0; idx < highlightsToClear.Count; idx++)
            {
                BuildingHighlighter.RemoveHighlight(highlightsToClear.Dequeue());
            }


            for (int idx = 0; idx < objectsToHighlight.Count; idx++)
            {
                GameObject o = objectsToHighlight.Dequeue();
                BuildingHighlighter.AddHighlight(o, animBlink);
                highlightsToClear.Enqueue(o);
            }
        }

    }

    /*
    IEnumerator Initialize()
    {
        //yield return null;
        //WWW jsonPage = new WWW(this.JsonURL);
        //yield return jsonPage;
        //JSONCityMatrix data = JsonUtility.FromJson<JSONCityMatrix>(jsonPage.text);

        // read file, acturally reading and parsing works but the file cannot be open with 2 programs
        //readJson = sReader.ReadToEnd();
        //JSONCityMatrix data = JsonUtility.FromJson<JSONCityMatrix>(readJson);

        // from UDP
        yield return null;
        //WWW jsonPage = new WWW(this.JsonURL);
        yield return udpString;
        udpString = UDPReceive.udpString;  //  Update our score continuously.
        Debug.Log(udpString);
        JSONCityMatrix data = JsonUtility.FromJson<JSONCityMatrix>(udpString);


        foreach (JSONBuilding b in data.grid)
        {
            b.Correct(15, 15);
            city[b.x, b.y].JSONUpdate(b);
        }
        //BuildingDataCtrl.instance.UpdateDensities(data.objects.density);

        this.oldData = data;
        StartCoroutine("CheckForUpdates");
    }

    IEnumerator CheckForUpdates()
    {
        //WWW jsonPage = new WWW(this.JsonURL);
        //yield return jsonPage;
        //while (jsonPage.error != null)
        //{
        //    Debug.Log(jsonPage.error);
        //    jsonPage = new WWW(this.JsonURL);
        //    float t = Time.time;
        //    yield return jsonPage;
        //}
        //Debug.Log(Time.time - t);



        //JSONCityMatrix data = JsonUtility.FromJson<JSONCityMatrix>(jsonPage.text);


        // from UDP
        yield return udpString;
        udpString = UDPReceive.udpString;  //  Update our score continuously.
        Debug.Log(udpString);
        JSONCityMatrix data = JsonUtility.FromJson<JSONCityMatrix>(udpString);


        for (int i = 0; i < data.grid.Length; i ++)
        {
            JSONBuilding a = data.grid[i];
            a.Correct(15,15);
            city[a.x, a.y].JSONUpdate(a);
        }
        BuildingDataCtrl.instance.UpdateDensities(data.objects.density);
        StartCoroutine("CheckForUpdates");
    }
    */
}

//RZ170614
[Serializable]
public class JSONCityMatrixMLAI
{
    public JSONCityMatrix predict;
    public JSONCityMatrix ai;
}

[Serializable]
public class JSONCityMatrix
{
    public JSONBuilding[] grid;
    public JSONObjects objects;
    public int new_delta;
}

[Serializable]
public class JSONBuilding
{
    public int type;
    public int x;
    public int y;
    public int magnitude;
    public int rot;

    public override bool Equals(object obj)
    {
        JSONBuilding o = obj as JSONBuilding;
        return o != null &&
            this.type == o.type &&
            this.x == o.x &&
            this.y == o.y &&
            this.magnitude == o.magnitude &&
            this.rot == o.rot;
    }

    public override int GetHashCode()
    {
        return this.type.GetHashCode() *
            this.x.GetHashCode() *
            this.y.GetHashCode() *
            this.magnitude.GetHashCode() *
            this.rot.GetHashCode();
    }
    /// <summary>
    /// Normalizes this building into bottom-left origin coordinates
    /// </summary>
    public void Correct(int maxX, int maxY)
    {
        //x = maxX - x;
        y = maxY - y;
    }

    /// <summary>
    /// Returns true if this new JSONBuildingData alters the input buildingData a
    /// </summary>
    /// <param name="a"></param>
    /// <returns></returns>
    public bool Changes(BuildingModel a)
    {
        return this.x == a.x &&
            this.y == a.y &&
            (this.type != a.Id ||
            this.magnitude != a.Magnitude ||
            this.rot != a.Rotation);
    }
    public bool Changes(JSONBuilding a)
    {
        return this.x == a.x &&
            this.y == a.y &&
            (this.type != a.type ||
            this.magnitude != a.magnitude ||
            this.rot != a.rot);
    }
}

[Serializable]
public class JSONObjects
{
    public int pop_mid;
    public int toggle2;
    public int pop_old;
    public int[] densities;
    public int IDMax;
    public double slider1;
    public int toggle1;
    public int dockRotation;
    public int pop_young;
    public int gridIndex;
    public int dockID;
    public int toggle3;

    public float popDensity;

    //RZ 170615 get meta data from JSON sent by GH VIZ
    public int AIStep;
    public int animBlink;
    public float[] scores;
}
