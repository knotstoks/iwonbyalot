using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapButton : MonoBehaviour
{
    public GameObject map;
    public void Start() {
        map = GameObject.FindWithTag("Map");
        Activate();
    }

    public void Activate() {
        map.GetComponent<MapController>().InteractMap();
    }
}
