using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public List<MapDistrictController> districtScripts;
    public GameObject mapDetailController;
    public bool mapDisplayed;
    //public List<District> testDistricts;

    //private bool isShowing = false;

    public void UpdateDistricts(List<District> districts)
    {
        for (int i = 0; i < districtScripts.Count; i++)
        {
            districtScripts[i].UpdateDistrict(districts[i]);
        }
    }

    public void InteractMap() {
        if(mapDisplayed)
        {
            mapDetailController.SetActive(false);
            gameObject.SetActive(false);
            mapDisplayed = false;
        }
        else
        {
            gameObject.SetActive(true);
            mapDisplayed = true;
        }
    }


    void Start()
    {
        mapDisplayed = false;
    }
    //{
    //    testDistricts = new List<District>();
    //    District dist1 = new District();
    //    District dist2 = new District();
    //    dist2.againstVotesCount = 11;
    //    testDistricts.Add(dist1);
    //    testDistricts.Add(dist2);
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.C))
    //    {
    //        if (!isShowing)
    //        {
    //            ShowMap(testDistricts);
    //        }
    //        else
    //        {
    //            HideMap();
    //        }
    //        isShowing = !isShowing;
    //    }
    //}
}
