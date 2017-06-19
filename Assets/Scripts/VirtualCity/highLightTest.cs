using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace cakeslice
{
    public class highLightTest : MonoBehaviour
    {
        void Start()
        {
            foreach (Transform child in transform)
            {
                child.gameObject.AddComponent<Outline>();
            }
        }
    }
}