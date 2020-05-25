using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Key : MonoBehaviour {

    public bool isPressed;
    public bool isPlaying;
    public bool isSelected;
    
    private float keyDownPos;

    private Vector3 originalPos;
    private Color keyPressedColor;

    private bool isGame;

    void Start() {
        isPressed = false;
        isPlaying = false;
        keyDownPos = 0.1f;
        originalPos = transform.localPosition;
        keyPressedColor = new Color(255.0f/255.0f, 165.0f/255.0f, 0, 1.0f);
        if (Settings.currentScene == "Game") {
            isGame = true;
        }
        else {
            isGame = false;
        }
        isSelected = false;
    }

    void Update() {
        if (isPressed) {
            if (!isPlaying) {
                isPlaying = true;
                playSound();
                pressTransform();
            }
            updateRender();
            if (isGame) {
                if (isSelected) {
                    Game.currentScore += 3;
                }
                else {
                    Game.currentScore -= 1;
                }
            }
        }
        else {
            isPlaying = false;
            stopSound();
            liftTransform();
        }
    }

    void playSound() {
        var sound = transform.gameObject.GetComponent<AudioSource>();
        sound.time = 0.9f;
        sound.volume = PlayerPrefs.GetFloat("Volume")/100.0f;
        sound.Play();
    }

    void stopSound() {
        var sound = transform.gameObject.GetComponent<AudioSource>();
        sound.Stop();
    }

    void pressTransform() {
        Vector3 newPos = originalPos;
        newPos.y -= keyDownPos;
        transform.localPosition = newPos;
    }

    void liftTransform() {
        transform.localPosition = originalPos;
    }

    void updateRender() {
        Renderer keyRend = transform.GetComponent<Renderer>();
        keyRend.material.color = keyPressedColor;
    }

}
