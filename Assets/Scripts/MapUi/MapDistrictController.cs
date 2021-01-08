using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MapDistrictController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private District curDistrict;
    public Tracker track;
    public int districtIndex;
    private static Color32 blue = new Color32(0, 128, 225, 200);
    private static Color32 red = new Color32(255, 51, 51, 200);
    private static Color32 hover = new Color32(0, 200, 0, 200);
    public MapDetailController mapDetailController;
    public bool popoutEnable = true;

    public void setMini()
    {
        gameObject.GetComponent<Image>().raycastTarget = false;
    }

    public void ShowDistrict()
    {
        gameObject.SetActive(true);
    }

    public void UpdateDistrict(District district)
    {
        if (district.forVotesCount >= district.totalVotesCount / 2)
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
        mapDetailController.HideDetails();
    }

    void Awake()
    {
        gameObject.GetComponent<Image>().alphaHitTestMinimumThreshold = 0.4f;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(popoutEnable)
        {
            mapDetailController.ShowDetails(curDistrict, transform.position);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(popoutEnable)
        {
            mapDetailController.HideDetails();
        }
    }

    public void AssignDistrict()
    {
        track.SelectDistrict(districtIndex);
    }

    public void AssignTrack(Tracker track)
    {
        this.track = track;
        gameObject.GetComponent<Button>().onClick.AddListener(AssignDistrict);
        ColorBlock colorVar = gameObject.GetComponent<Button>().colors;
        colorVar.highlightedColor = hover;
        gameObject.GetComponent<Button>().colors = colorVar;
    }

}
