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
