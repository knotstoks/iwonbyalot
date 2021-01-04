﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MapDistrictController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private District curDistrict;
    private static Color32 blue = new Color32(0, 128, 225, 200);
    private static Color32 red = new Color32(255, 51, 51, 200);
    public MapDetailController mapDetailController;

    public void ShowDistrict(District district)
    {
        gameObject.SetActive(true);
        if (district.forVotesCount >= district.againstVotesCount)
        {
            gameObject.GetComponent<Image>().color = blue;
        }
        else
        {
            gameObject.GetComponent<Image>().color = red;
        }
        curDistrict = district;
    }

    public void HideDistrict()
    {
        gameObject.SetActive(false);
    }

    void Awake()
    {
        gameObject.GetComponent<Image>().alphaHitTestMinimumThreshold = 0.4f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        mapDetailController.ShowDetails(curDistrict, transform.position);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mapDetailController.HideDetails();
    }

}
