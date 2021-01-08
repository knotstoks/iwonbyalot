using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapDetailController : MonoBehaviour
{
    public Text districtName;
    public Text forVotesDisplay;
    public Text againstVotesDisplay;

    public void ShowDetails(District district, Vector2 pos)
    {
        gameObject.SetActive(true);
        gameObject.transform.position = pos;
        forVotesDisplay.text = district.forVotesCount.ToString();
        againstVotesDisplay.text = (district.totalVotesCount - district.forVotesCount).ToString();
        districtName.text = district.name;
    }

    public void HideDetails()
    {
        gameObject.SetActive(false);
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
