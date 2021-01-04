using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
	public GameObject grey_BG;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	public void OpenWindow(GameObject window) {
		grey_BG.SetActive(true);
		window.SetActive(true);
	}
	
	public void CloseWindow(GameObject window) {
		grey_BG.SetActive(false);
		window.SetActive(false);
	}
	
	public void Quit() {
		Application.Quit();
	}
}
