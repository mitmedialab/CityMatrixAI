using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateChildText : MonoBehaviour {

    private Text textThis;
    private Text textChild;

    private void Start()
    {
        textThis = GetComponent<Text>();
        textChild = transform.GetChild(0).GetComponent<Text>();
    }

    void Update () {
        textChild.text = textThis.text;

    }
}
