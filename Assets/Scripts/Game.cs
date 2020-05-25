using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Game : MonoBehaviour {
    
    public TextMeshProUGUI currentText;

    public static int difficulty;
    public static bool isAutoplay;
    public static int currentScore;
    

    private float startOffset;
    private bool gameStart;
    private int[,] songArray;

    private float secPerBeat;
    private int currentNote;
    private float timeSinceLastNote;
    private float timeOfNote;
    private Transform currentKey;
    private Color keyToPressColor;

    private bool songFinished;

    void Start() {
        currentScore = 0;
        startOffset = 2.0f;
        gameStart = false;
        songArray = null;
        secPerBeat = 0.0f;
        keyToPressColor = Color.red;
        songFinished = false;
    }

    void Update() {
        
        currentText.text = "Score: " + currentScore;

        if (songFinished) {
            displayEnd();
            return;
        }
        if (!gameStart) {
            if (startOffset < Time.timeSinceLevelLoad) {
                gameStart = true;
                startGame();
            }
        }
        else {
            updateGame();
            updateRender();
        }
    }

    void startGame() {
        currentNote = 0;
        timeSinceLastNote = 0;
        if (difficulty == 0) {
            songArray = SongOne.keyArray;
            secPerBeat = 60.0f/SongOne.tempo;
        }
        else if (difficulty == 1) {
            songArray = SongOne.keyArray;
            secPerBeat = 60.0f/SongOne.tempo;
        }
        else {
            songArray = SongOne.keyArray;
            secPerBeat = 60.0f/SongOne.tempo;
        }
        
        int keyRow = songArray[0, 0];
        int keyOctave = songArray[0, 1];
        int keyNote = songArray[0, 2];
        int keyTime = songArray[currentNote, 3];
        timeOfNote = keyTime * secPerBeat * 0.5f;
        currentKey = transform.GetChild(keyRow).GetChild(keyOctave).GetChild(keyNote);
        currentKey.gameObject.GetComponent<Key>().isSelected = true;
        currentKey.gameObject.GetComponent<Key>().isPressed = true;
    }

    void updateGame() {
        
        if (timeSinceLastNote < timeOfNote) {
            timeSinceLastNote += Time.deltaTime;
        }
        else {
            if (currentNote < SongOne.totalNotes - 1) {
                currentKey.gameObject.GetComponent<Key>().isSelected = false;
                currentKey.gameObject.GetComponent<Key>().isPressed = false;
                currentNote++;
                nextNote();
            }
            else {
                currentKey.gameObject.GetComponent<Key>().isSelected = false;
                currentKey.gameObject.GetComponent<Key>().isPressed = false;
                songFinished = true;
            }
        }

    }

    void updateRender() {
        Renderer keyRend = currentKey.GetComponent<Renderer>();
        keyRend.material.color = keyToPressColor;
    }

    void nextNote() {
        timeSinceLastNote = 0;
        int keyRow = songArray[currentNote, 0];
        int keyOctave = songArray[currentNote, 1];
        int keyNote = songArray[currentNote, 2];
        int keyTime = songArray[currentNote, 3];
        timeOfNote = keyTime * secPerBeat * 0.5f;
        currentKey = transform.GetChild(keyRow).GetChild(keyOctave).GetChild(keyNote);
        currentKey.gameObject.GetComponent<Key>().isSelected = true;
        currentKey.gameObject.GetComponent<Key>().isPressed = true;
    }

    void displayEnd() {

    }

}
