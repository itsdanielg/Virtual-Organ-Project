using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public AudioSource mainmenu;

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
    }
    
    public void Play() {
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2, LoadSceneMode.Single);
    }

    public void Explore() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
    }

    public void Quit() {
        Application.Quit();
    }

}
