using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace cakeslice
{
    public static class BuildingHighlighter
    {

        public static void AddHighlight(GameObject b, int color)
        { 
            if(b.GetComponent<MeshRenderer>() != null)
            {

                var comp = b.AddComponent<Outline>();
                comp.color = color;
                comp.eraseRenderer = false;
            }
            foreach(Transform c in b.transform)
            {
                AddHighlight(c.gameObject, color);
            }
        }

        public static void RemoveHighlight(GameObject b)
        {
            if (b.GetComponent<Outline>() != null)
            {
                b.GetComponent<Outline>().eraseRenderer = true;
                b.GetComponent<Outline>().enabled = false; //RZ 170622
            }
            foreach(Transform c in b.transform)
            {
                RemoveHighlight(c.gameObject);
            }
        }
    }
}