using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string MainGame;
    
	public GameObject grey_BG, level_select, new_btn, continue_btn, days_text, position_text;
    
    public List<LevelData> level_datas;
    public List<GameObject> level_btns;
    private int selected;
    
    // Start is called before the first frame update
    void Start()
    {
        selected = -1;
    }

    // Update is called once per frame
    void Update()
    {
    }
    
    public void onLevelBtnClick(int level) {
        if (level != selected) {
            selected = level;
            position_text.GetComponent<Text>().text = "Position: " + level_datas[selected].position;
            days_text.GetComponent<Text>().text = "Days: " + level_datas[selected].days;
            new_btn.GetComponent<Button>().interactable = true;
        }
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
    
    public void SwitchToMainGame() {
        SceneManager.LoadScene(MainGame);
        DataPassedToMainGame.level_data = level_datas[selected];
    }
}
