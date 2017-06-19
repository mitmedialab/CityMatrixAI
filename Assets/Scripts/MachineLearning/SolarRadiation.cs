using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;
using System.Linq;

public class SolarRadiation : MonoBehaviour {

    public TextAsset regressionFile;

    private double[,] coefficients;
    private int inputSize = 25;
    private int inputWidth = 5;
    private int outputSize = 1225;
    private int sensorsX = 7;

	// Use this for initialization
	void Start () {
        coefficients = LoadCoefficientData();
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    internal void updateBuilding(BuildingModel[,] city, BuildingModel newData)
    {
        Debug.Log("Hello");
        float newHeight = newData.Height;
        newData.HeatMap = city[newData.x, newData.y].HeatMap;
        newData.Height = city[newData.x, newData.y].Height;
        this.changeBuildingHeight(city, newData.x, newData.y, newHeight);
    }

    internal void changeBuildingHeight(BuildingModel[,] city, int x, int y, float newHeight)
    {
        BuildingModel[] block = new BuildingModel[inputSize];
        int counter = 0;
        for(int i = x - inputWidth / 2; i < x + inputWidth / 2 + 1; i ++)
        {
            for (int j = y - inputWidth / 2; j < y + inputWidth / 2 + 1; j++)
            {
                if (i < 0 || j < 0 || i >= city.GetLength(0) || j >= city.GetLength(1))
                {
                    BuildingModel tmp = new BuildingModel();
                    tmp.Height = 1;
                    //TODO HEIGHT CHANGE
                    block[counter] = tmp;
                } else
                {
                    block[counter] = city[i, j];
                }
                counter++;
            }
        }
        counter = 0;
        updateBlock(block, newHeight);
        block[inputSize / 2].Height = newHeight;
    }

    private void updateBlock(BuildingModel[] oldBlock, float newCenterHeight)
    {
        float[] heightMap = new float[inputSize];
        for(int i = 0; i < inputSize; i ++)
        {
            //TODO HEIGHT CHANGE
            heightMap[i] = (oldBlock[i].Height + 25) / 25;
        }
        double[] deltas = new double[outputSize];
        double[] addDeltas = PredictCentralRemoval(heightMap);
        //TODO HEIGHT CHANGE
        heightMap[inputSize / 2] = (newCenterHeight + 25) / 25;
        double[] subtractDeltas = PredictCentralRemoval(heightMap);
        for(int i = 0; i < outputSize; i ++)
        {
            deltas[i] = addDeltas[i] - subtractDeltas[i];
        }

        int counter = 0;
        for(int b = 0; b < inputSize; b ++)
        {
            double[,] heatMap = oldBlock[b].HeatMap;
            for(int i = 0; i < sensorsX; i ++)
            {
                for(int j = 0; j < sensorsX; j ++)
                {
                    heatMap[i, j] = heatMap[i, j] - deltas[counter];
                    counter++;
                }
            }
            oldBlock[b].HeatMap = heatMap;
        }
    }

    /// <summary>
    /// Loads the coefficient data and returns the array of coefficients as they are in the file
    /// </summary>
    /// <param name="path">File to read coefficients from</param>
    /// <returns>Array of coefficients</returns>
    double[,] LoadCoefficientData()
    {
        StreamReader inp = new StreamReader(
            new MemoryStream(
                Encoding.UTF8.GetBytes(regressionFile.text ?? "")));
        double[,] output = new double[outputSize, inputSize];

        int i = 0;
        while(!inp.EndOfStream)
        {
            string line = inp.ReadLine();
            string[] coeffs = line.Split(',');
            int j = 0;
            foreach(string coeff in coeffs)
            {
                double val = double.Parse(coeff, System.Globalization.NumberStyles.Float);
                output[i, j] = val;
                j++;
            }
            i++;
        }

        return output;
    }

    /// <summary>
    /// Runs the regression produced by the solar radiation ML algorithm on the input, as if the central building were demolished.
    /// </summary>
    /// <param name="heights">Heights of a 5x5 array of buildings.</param>
    /// <returns>Solar values of each sensor after the change.</returns>
    double[] PredictCentralRemoval(float[] heights)
    {
        if(heights.Length != inputSize)
        {
            throw new System.Exception("The solar radiation algorithm expects an array of building cells 35x35 (5x5 buildings of 7x7 cells)");
        }
        double[] output = new double[outputSize];

        for(int i = 0; i < coefficients.GetLength(0); i ++)
        {
            double delta = 0;
            for(int j = 0; j < coefficients.GetLength(1); j ++)
            {
                delta += heights[j] * coefficients[i, j];
            }
            output[i] = delta;
        }

        return output;
    }
}
