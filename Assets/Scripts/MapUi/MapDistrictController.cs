using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MapDistrictController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public int districtIndex;
    private District curDistrict;
    private static Color32 blue = new Color32(0, 128, 225, 200);
    private static Color32 red = new Color32(255, 51, 51, 200);
    public MapDetailController mapDetailController;

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
        mapDetailController.ShowDetails(curDistrict, transform.position);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mapDetailController.HideDetails();
    }

}
