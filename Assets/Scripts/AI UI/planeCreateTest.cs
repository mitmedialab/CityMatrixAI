using UnityEngine;

public class planeCreateTest : MonoBehaviour
{

    public GameObject SoundWaveBarPrefab;

    private Transform tf;
    private GameObject tempPlane;

    // Use this for initialization
    void Start()
    {
        tf = transform;
        tempPlane = Instantiate(SoundWaveBarPrefab, tf);
    }

    // Update is called once per frame
    void Update()
    {

    }
}