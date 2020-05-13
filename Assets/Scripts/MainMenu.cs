using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public AudioSource mainmenu;

    void Start() {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        if (PlayerPrefs.HasKey("Volume")) {
            mainmenu.volume = PlayerPrefs.GetFloat("Volume")/100.0f;
            mainmenu.Play();
        }
        else {
            mainmenu.volume = 0.5f;
            mainmenu.Play();
        }
    }
    
    public void Play() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
    }

    public void Quit() {
        Application.Quit();
    }

}
