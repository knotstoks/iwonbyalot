using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public List<GameObject> tutorials;
    private int next;
    private Tracker tracker;
    
    public void Start() {
        next = 1;
    }
    
    public void Init(Tracker tracker) {
        this.tracker = tracker;
    }
    
    public void Update() {
        if (Input.GetMouseButtonDown(0)) {
            tutorials[next - 1].SetActive(false);
            if (next < tutorials.Count) {
                tutorials[next].SetActive(true);
                next++;
            } else {
                tracker.removeTutorial();
            }
        }
        
        if (Input.GetKeyDown(KeyCode.Escape)) {
            tutorials[next - 1].SetActive(false);
            tracker.removeTutorial();
        }
    }
}