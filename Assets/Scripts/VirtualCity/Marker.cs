using UnityEngine;
using System.Collections;

public class Marker : MonoBehaviour {

    VirtualCityModel virtualCity;
    BuildingModel[,] city;
    int x = 0;
    int z = 0;
    float heightDelta = 10;

	// Use this for initialization
	void Start () {
        this.virtualCity = this.transform.parent.GetComponent<VirtualCityModel>();
        this.city = this.virtualCity.GetCity();
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetButtonDown("MarkerRight") && this.x < city.GetLength(0) - 1)
        {
            this.x += 1;
        } else if (Input.GetButtonDown("MarkerLeft") && this.x > 0)
        {
            this.x -= 1;
        }
        if (Input.GetButtonDown("MarkerForward") && this.z > 0)
        {
            this.z -= 1;
        }
        else if (Input.GetButtonDown("MarkerBack") && this.z < city.GetLength(1) - 1)
        {
            this.z += 1;
        }

        if(Input.GetButtonDown("MarkerUp"))
        {
            this.city[this.x, this.z].Height += heightDelta;
        } else if(Input.GetButtonDown("MarkerDown"))
        {
            this.city[this.x, this.z].Height += heightDelta;
        }

        //TODO fix
        Vector3 pos = new Vector3(this.x, this.z);
        //pos.y = city[x, z].data.GetVirtualHeight();
        this.transform.localPosition = pos;
	}
}
