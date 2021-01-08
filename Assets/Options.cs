using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Options : MonoBehaviour
{
    public AudioSource music;
    public Slider music_vol;
    public Toggle skipToggle;
    public static float vol = 1;
    public static bool skipTutorial = false;
    // Start is called before the first frame update
    
    public void Start()
    {
        music_vol.value = vol;
        music.volume = music_vol.value;
        
        skipToggle.isOn = skipTutorial;
        
        music_vol.onValueChanged.AddListener(delegate {ChangeVol(); });
        skipToggle.onValueChanged.AddListener(delegate {ChangeToggle(); });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void ChangeVol() {
        music.volume = music_vol.value;
        vol = music_vol.value;
    }
    
    public void ChangeToggle() {
        skipTutorial = skipToggle.isOn;
    }
    
    public void BackToMainMenu() {
        SceneManager.LoadScene("Main Menu");    
    }
}
