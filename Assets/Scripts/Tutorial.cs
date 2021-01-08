using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public List<GameObject> tutorials;
    private int next, level;
    private Tracker tracker;
    private MapButton mapButton;
    private GameObject speechUI;
    private GameObject greyBG;
    
    public void Start() {
        next = 1;
    }
    
    public void Init(int level, Tracker tracker, GameObject greyBG) {
        this.level = level;
        this.tracker = tracker;
        this.greyBG = greyBG;
    }
    
    public void Init(int level, Tracker tracker, GameObject greyBG, MapButton mapButton, GameObject speechUI) {
        Init(level, tracker, greyBG);
        this.mapButton = mapButton;
        this.speechUI = speechUI;
    }
    
    public void Update() {
        if (Input.GetMouseButtonDown(0) && !greyBG.activeSelf) {
            tutorials[next - 1].SetActive(false);
            
            if (next < tutorials.Count) {
                tutorials[next].SetActive(true);
                
                if (level == 2) {
                    switch (next) {
                        case 3:
                            mapButton.Activate();
                            break;
                        case 4:
                            mapButton.Activate();
                            speechUI.SetActive(true);
                            break;
                        case 6:
                            speechUI.SetActive(false);
                            break;
                    }
                }
                
                next++;
            } else {
                tracker.removeTutorial();
            }
        }
    }
}