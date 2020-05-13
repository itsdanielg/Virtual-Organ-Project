using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Variables : MonoBehaviour {

    public AudioSource mainmenu;
    
    public Slider volumeSlider;
    public Slider sensitivitySlider;
    
    public static float volume = 50;
    public static float sensitivity = 1;

    void Start() {
        if (PlayerPrefs.HasKey("Volume")) {
            volume = PlayerPrefs.GetFloat("Volume");
            volumeSlider.value = volume;
        }
        else {
            PlayerPrefs.SetFloat("Volume", volume);
            volumeSlider.value = volume;
        }
        if (PlayerPrefs.HasKey("Sensitivity")) {
            volume = PlayerPrefs.GetFloat("Sensitivity");
            sensitivitySlider.value = sensitivity;
        }
        else {
            PlayerPrefs.SetFloat("Sensitivity", sensitivity);
            sensitivitySlider.value = sensitivity;
        }

    }

    public void updateVolume(float newVolume) {
        volume = newVolume;
        PlayerPrefs.SetFloat("Volume", volume);
        mainmenu.volume = volume/100.0f;
    }

    public void updateSensitivity(float newSensitivity) {
        sensitivity = newSensitivity;
        PlayerPrefs.SetFloat("Sensitivity", sensitivity);
    }
    
}
