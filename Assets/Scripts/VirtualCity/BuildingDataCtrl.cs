using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class BuildingDataCtrl : MonoBehaviour
{
    public static BuildingDataCtrl instance = null;

    public int medDensity;
    public int highDensity;

    public int streetViewId;

    public GameObject changeIndicator;

    public GameObject[] residentialLowRise;
    public GameObject[] residentialMidRise;
    public GameObject[] residentialHighRise;
    public GameObject[] officeLowRise;
    public GameObject[] officeMidRise;
    public GameObject[] officeHighRise;
    public GameObject[] road;
    public GameObject[] park;
    public GameObject[] flat;

    public IdDataStruct[] buildingTypes;

    internal List<int> density = new List<int>();

    internal Dictionary<int, List<BuildingModel>> models = new Dictionary<int,List<BuildingModel>>();

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        } else
        {
            Destroy(this.gameObject);
        }
    }

    void AddModel(BuildingModel b)
    {
        if (!this.models.ContainsKey(b.Id))
        {
            this.models.Add(b.Id, new List<BuildingModel>());
        }
        foreach(List<BuildingModel> list in this.models.Values)
        {
            list.Remove(b);
        }
        this.models[b.Id].Add(b);
    }

    internal void UpdateBuildingModel(BuildingModel model)
    {
        int id = model.Id;
        IdDataStruct type = System.Array.Find(buildingTypes, a => a.ID == id);
        if (type != null)
        {
            model.Height = type.height;
            model.Width = type.width;
            float r = Random.Range(0f, 1f);
            model.FlatView = id == -1 ? (r > 0.25f ? park[Random.Range(0, park.Length - 1)] :
                    flat[Random.Range(0, flat.Length - 1)]) :
                    type.flatView;
            int dens = id != -1 && this.density.Count > id ? this.density[id] : -1;
            if (id != this.streetViewId) model.ReleaseStreetView();
            if (id == -1)
            {
                model.MeshView = r > 0.25f ? park[Random.Range(0, park.Length - 1)] :
                    flat[Random.Range(0, flat.Length - 1)];
            } else if(id == 6 || id == this.streetViewId)
            {
                model.MeshView = road[Random.Range(0, road.Length - 1)];
            } else if(dens <= this.medDensity)
            {
                model.MeshView = type.residential ?
                    residentialLowRise[Random.Range(0, residentialLowRise.Length - 1)] :
                    officeLowRise[Random.Range(0, officeLowRise.Length - 1)];
            } else if (dens <= this.highDensity)
            {
                model.MeshView = type.residential ?
                    residentialMidRise[Random.Range(0, residentialMidRise.Length - 1)] :
                    officeMidRise[Random.Range(0, officeMidRise.Length - 1)];
            } else
            {
                model.MeshView = type.residential ?
                    residentialHighRise[Random.Range(0, residentialHighRise.Length - 1)] :
                    officeHighRise[Random.Range(0, officeHighRise.Length - 1)];
            }
            if(id == this.streetViewId)
            {
                model.StreetView();
            }
        }
        model.ChangeIndicator = this.changeIndicator;
        this.AddModel(model);
    }

    internal void UpdateDensities(int[] densities) {
        for(int i = 0; i < this.density.Count(); i ++ )
        {
            if(this.density[i] != densities[i])
            {
                this.density[i] = densities[i];
                List<BuildingModel> list = new List<BuildingModel>(this.models[i]);
                if (list == null) continue;
                foreach (BuildingModel b in list)
                {
                    this.UpdateBuildingModel(b);
                }
            }
        }
        for(int i = this.density.Count(); i < densities.Length; i ++)
        {
            this.density.Add(densities[i]);
            List<BuildingModel> list = new List<BuildingModel>(this.models[i]);
            if (list == null) continue;
            foreach (BuildingModel b in list)
            {
                this.UpdateBuildingModel(b);
            }
        }
    }

    [System.Serializable]
    public class IdDataStruct
    {
        public int ID;
        public float height;
        public float width;
        public GameObject flatView;
        public bool residential;
    }
}

public class BuildingModel {

    public VirtualCityModel parentModel;
    private int _id;
    public int Id {
        get {
            return _id; }
        set
        {
            bool a = value == _id;
            _id = value;
            if (!a)
            {
                BuildingDataCtrl.instance.UpdateBuildingModel(this);
                IndicateChange();
            }
            foreach (Building b in views) {
              b.gameObject.SetActive(true);
              if (_id == -2) {
                b.gameObject.SetActive(false);
              }
            }
        }
    }
    public int x;
    public int y;
    private float _height;
    public float Height {
        get { return _height; }
        set {
            _height = value;
            foreach (Building b in views)
            {
                b.Height = GetVirtualHeight();
            }
        }
    }
    private float _width;
    public float Width {
        get { return _width; }
        set { _width = value;
            foreach (Building b in views)
            {
                b.Height = GetVirtualHeight();
            }
        }
    }
    private int _rotation;
    public int Rotation
    {
        get { return _rotation; }
        set {
            bool a = value == _rotation;
            _rotation = value;
            if (!a)
            {
                foreach (Building b in views)
                {
                    b.Rotation = value;
                }
                IndicateChange();
            }
        }
    }
    private int _magnitude;
    public int Magnitude
    {
        get { return _magnitude; }
        set { _magnitude = value; }
    }
    private double[,] _heatMap;
    public double[,] HeatMap
    {
        get { return _heatMap; }
        set {
            bool a = value == _heatMap;
            _heatMap = value;
            if (!a)
            {
                foreach (Building b in views)
                {
                    b.Recolor(_colorRef, _heatMap);
                }
            }
        }
    }
    private double _colorRef;
    public double ColorRef
    {
        get { return _colorRef; }
        set {
            bool a = value == _colorRef;
            _colorRef = value;
            if (!a)
            {
                foreach (Building b in views)
                {
                    b.Recolor(_colorRef, _heatMap);
                }
            }
        }
    }

    private GameObject _flatView;
    public GameObject FlatView
    {
        get { return _flatView; }
        set { _flatView = value;
            foreach(Building b in views)
            {
                b.flatPrefab = value;
                b.UpdateView();
            }
        }
    }
    private GameObject _meshView;
    public GameObject MeshView
    {
        get { return _meshView; }
        set
        {
            _meshView = value;
            foreach (Building b in views)
            {
                b.meshPrefab = value;
                b.UpdateView();
            }
        }
    }
    private GameObject _changeIndicator;
    public GameObject ChangeIndicator
    {
        get { return _changeIndicator; }
        set
        {
            _changeIndicator = value;
            foreach (Building b in views)
            {
                b.changeIndicator = value;
            }
        }
    }


    private List<Building> views = new List<Building>();

    public BuildingModel()
    {
        this._id = -1;
        this.x = 0;
        this.y = 0;
        this._rotation = 0;

        this._height = 0.1f;
        this._width = 30;
        this._magnitude = 0;
        this._heatMap = new double[7, 7];
        BuildingDataCtrl.instance.UpdateBuildingModel(this);
    }

    public void AddView(Building b)
    {
        views.Add(b);
        b.Height = this.GetVirtualHeight();
        b.flatPrefab = this.FlatView;
        b.meshPrefab = this.MeshView;
        b.changeIndicator = this.ChangeIndicator;
        //b.Recolor(_colorRef, _heatMap);
        //b.Rotation = this.Rotation;
    }

    public void StreetView()
    {
        foreach(Building b in this.views)
        {
            b.StreetView();
        }
    }

    public void ReleaseStreetView()
    {
        foreach (Building b in this.views)
        {
            b.ReleaseStreetView();
        }
    }

    public float GetVirtualHeight()
    {
        return this.Height / this.Width;
    }

    public void JSONUpdate(JSONBuilding b)
    {
        this.Id = b.type;
        this.Rotation = b.rot;
        this.Magnitude = b.magnitude;
    }

    public BuildingModel Copy()
    {
        BuildingModel a = (BuildingModel)this.MemberwiseClone();
        a._heatMap = new double[7, 7];
        return a;
    }

    void IndicateChange()
    {
        foreach(Building b in this.views)
        {
            //b.IndicateChange();
            if (b.ViewType == Building.Type.MESH)
            {
                b.Focus();
            }
        }
    }
}
