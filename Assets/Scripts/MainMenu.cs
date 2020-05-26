using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public AudioSource mainmenu;

    public static int difficulty;
    public static bool autoplay;

    void Start() {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        if (!PlayerPrefs.HasKey("Volume")) {
            PlayerPrefs.SetFloat("Volume", Variables.volume);
        }
        if (!PlayerPrefs.HasKey("Sensitivity")) {
            PlayerPrefs.SetFloat("Sensitivity", Variables.sensitivity);
        }
        mainmenu.volume = PlayerPrefs.GetFloat("Volume")/100.0f;
        mainmenu.Play();
        difficulty = 0;
        autoplay = false;
        PlayerPrefs.SetInt("Difficulty", difficulty);
        PlayerPrefs.SetInt("Autoplay", (autoplay ? 1 : 0));
    }

    public void setEasy(bool newValue) {
        if (newValue) {
            PlayerPrefs.SetInt("Difficulty", 0);
        }
        
    }

    public void setMedium(bool newValue) {
        if (newValue) {
            PlayerPrefs.SetInt("Difficulty", 1);
        }
        
    }

    public void setHard(bool newValue) {
        if (newValue) {
            PlayerPrefs.SetInt("Difficulty", 2);
        }
        
    }

    public void setAutoplay(bool newValue) {
        PlayerPrefs.SetInt("Autoplay", (newValue ? 1 : 0));
    }
    
    public void Play() {
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }

    public void Explore() {
        SceneManager.LoadScene("Organ", LoadSceneMode.Single);
    }

    public void Quit() {
        Application.Quit();
    }

}
