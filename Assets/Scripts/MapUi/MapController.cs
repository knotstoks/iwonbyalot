using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public GameObject mapContainer; // Assign in inspector
    public GameObject mapOutline; // Assign in inspector
    public List<MapDistrictController> districtScripts;
    public List<District> testDistricts;

    private bool isShowing = false;

    public void ShowMap(List<District> districts)
    {
        //mapContainer.SetActive(true);
        mapOutline.SetActive(true);
        for (int i = 0; i < districtScripts.Count; i++)
        {
            districtScripts[i].ShowDistrict(districts[i]);
        }
    }

    public void HideMap()
    {
        //mapContainer.SetActive(false);
        mapOutline.SetActive(false);
        for (int i = 0; i < districtScripts.Count; i++)
        {
            districtScripts[i].HideDistrict();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        testDistricts = new List<District>();
        District dist1 = new District();
        District dist2 = new District();
        dist2.againstVotesCount = 11;
        testDistricts.Add(dist1);
        testDistricts.Add(dist2);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (!isShowing)
            {
                ShowMap(testDistricts);
            }
            else
            {
                HideMap();
            }
            isShowing = !isShowing;
        }
    }
}
