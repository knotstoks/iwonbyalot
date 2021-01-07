using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MapController : MonoBehaviour
{
    public List<MapDistrictController> districtScripts;
    public List<GameObject> districts;
    public GameObject mapDetailController;
    public bool isMapDisplayed = true;
    public Tracker track;
    private int timeslotIndex;
    //public List<District> testDistricts;

    //private bool isShowing = false;

    public void setMini()
    {
        for (int i = 0; i < districtScripts.Count; i++)
        {
            districtScripts[i].setMini();
        }
    }

    public void UpdateDistricts(List<District> districts)
    {

        for (int i = 0; i < districtScripts.Count; i++)
        {
            districtScripts[i].UpdateDistrict(districts[i]);
        }
    }

    

    public void DisableDistricts(Tracker track) 
    {
        for(int i = 0; i < districtScripts.Count;i++)
        {
            districtScripts[i].popoutEnable = false;
        }
        this.track = track;
    }

    public void Buttonify(){
        for(int i = 0; i < districtScripts.Count; i++)
        {
            Button but = districts[i].AddComponent<Button>() as Button;
            but.onClick.AddListener(delegate{AssignDistrict(i);});
        }
    }

    public void AssignDistrict(int districtIndex) {
        
        track.SelectDistrict(districtIndex);
    }

    public void InteractMap() {
        if(isMapDisplayed)
        {
            mapDetailController.SetActive(false);
            gameObject.SetActive(false);
            isMapDisplayed = false;
        }
        else
        {
            gameObject.SetActive(true);
            isMapDisplayed = true;
        }
    }

    public void HideMap() {
        mapDetailController.SetActive(false);
        gameObject.SetActive(false);
        isMapDisplayed = false;
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
