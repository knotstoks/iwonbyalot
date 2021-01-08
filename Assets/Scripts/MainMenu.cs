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
    public List<GameObject> diff_highlights;
    
    private int select_level;
    private int select_diff;
    
    // Start is called before the first frame update
    void Start()
    {
        select_level = -1;
        select_diff = 0;
    }

    // Update is called once per frame
    void Update()
    {
    }
    
    public void onLevelBtnClick(int level) {
        if (level != select_level) {
            select_level = level;
            position_text.GetComponent<Text>().text = "Position: " + level_datas[select_level].position;
            days_text.GetComponent<Text>().text = "Days: " + level_datas[select_level].days;
            new_btn.GetComponent<Button>().interactable = true;
        }
    }
    
    public void onDiffBtnClick(int diff) {
        if (diff != select_diff) {
            select_diff = diff;
            diff_highlights[select_diff].SetActive(true);
            diff_highlights[(select_diff + 1) % 2].SetActive(false);
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
        DataPassedToMainGame.level_data = level_datas[select_level];
        DataPassedToMainGame.diff = select_diff;
        DataPassedToMainGame.tutorial = (select_diff == 0);
        SceneManager.LoadScene(MainGame);
    }
}
