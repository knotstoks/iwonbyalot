using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapDistrictController : MonoBehaviour
{
    public GameObject districtUi; // Assign in inspector
    private static Color32 blue = new Color32(0, 128, 225, 200);
    private static Color32 red = new Color32(255, 51, 51, 200);

    public void ShowDistrict(District district)
    {
        if (district.for_votes_count >= district.against_votes_count)
        {
            districtUi.GetComponent<Image>().color = blue;
        }
        else
        {
            districtUi.GetComponent<Image>().color = red;
        }
        districtUi.SetActive(true);
    }

    public void HideDistrict()
    {
        districtUi.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
