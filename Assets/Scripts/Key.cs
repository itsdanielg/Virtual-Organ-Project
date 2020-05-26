using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Key : MonoBehaviour {

    public bool isAutoplay;

    public bool isPressed;
    public bool isPlaying;
    public bool isSelected;
    public bool isBackground;
    public float timeSelected;
    public float keyTime;
    
    private float keyDownPos;

    private Vector3 originalPos;
    private Color keySelectedColor;
    private Color keyPressedColor;

    private bool isGame;

    void Start() {
        isAutoplay = PlayerPrefs.GetInt("Autoplay") == 1 ? true : false;
        isPressed = false;
        isPlaying = false;
        keyDownPos = 0.1f;
        originalPos = transform.localPosition;
        keySelectedColor = Color.red;
        keyPressedColor = new Color(255.0f/255.0f, 165.0f/255.0f, 0, 1.0f);
        if (Settings.currentScene == "Game") {
            isGame = true;
        }
        else {
            isGame = false;
        }
        isSelected = false;
        isBackground = false;
    }

    void Update() {
        if (isGame) {
            if (isSelected) {
                updateRender(0);
                timeSelected += Time.deltaTime;
                if (isAutoplay || isBackground) {
                    isPressed = true;
                }
            }
            else {
                timeSelected = 0;
                if (isAutoplay || isBackground) {
                    isPressed = false;
                }
            }
        }
        if (isPressed) {
            if (!isPlaying) {
                isPlaying = true;
                playSound();
                pressTransform();
            }
            updateRender(1);
            if (isGame) {
                if (isSelected) {
                    if (!isBackground) {
                        Game.currentScore += 3;
                    }
                }
                else {
                    if (!isBackground) {
                        Game.currentScore -= 1;
                    }
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

    void updateRender(int val) {
        Renderer keyRend = transform.GetComponent<Renderer>();
        if (val == 0) {
            keyRend.material.color = keySelectedColor;
        }
        else {
            keyRend.material.color = keyPressedColor;
        }
    }

}
