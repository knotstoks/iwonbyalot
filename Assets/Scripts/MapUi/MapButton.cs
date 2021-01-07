using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapButton : MonoBehaviour
{
    public GameObject map;

    public void Init(GameObject map)
    {
        this.map = map;
        map.GetComponent<MapController>().HideMap();
    }

    public void Activate() {
        map.GetComponent<MapController>().InteractMap();
    }
}
