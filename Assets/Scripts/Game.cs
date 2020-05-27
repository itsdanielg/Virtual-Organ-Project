using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Game : MonoBehaviour {
    
    public GameObject titleScreen;
    public GameObject ingameScreen;
    public TextMeshProUGUI currentText;
    public TextMeshProUGUI difficultyText;
    public TextMeshProUGUI gameModeText;
    public TextMeshProUGUI continueText;

    public static int difficulty;
    public static bool isAutoplay;
    public static int currentScore;

    private float startOffset;
    private float gameStartTime;
    private bool gameStart;
    private float songTime;
    private List<List<List<float>>> songArray;

    private float secPerBeat;
    private int currentNote;
    private float timeSinceLastNote;
    private float timeOfNote;
    private int remainingNotes;
    private List<Transform> playingNotes;
    private Transform currentKey;

    private bool songStarted;
    private bool songFinished;

    void Start() {
        difficulty = PlayerPrefs.GetInt("Difficulty");
        isAutoplay = PlayerPrefs.GetInt("Autoplay") == 1 ? true : false;
        currentScore = 0;
        startOffset = 2.0f;
        gameStart = false;
        gameStartTime = 0.0f;
        songTime = 0.0f;
        songArray = null;
        secPerBeat = 0.0f;
        songStarted = false;
        songFinished = false;
    }

    void Update() {
        
        if (songStarted) {
            currentText.text = "Score: " + currentScore;
            if (songFinished) {
                displayEnd();
                return;
            }
            if (!gameStart) {
                gameStartTime += Time.deltaTime;
                if (startOffset < gameStartTime) {
                    gameStart = true;
                    startGame();
                }
            }
            else {
                currentText.text = "Score: " + currentScore;
                updateGame();
            }
        }
        else {
            displayStart();
        }

    }

    void startGame() {
        songArray = SongInfo.keyArray;
        secPerBeat = 60.0f/SongInfo.tempo;
        currentNote = 0;
        remainingNotes = songArray.Count;
        playingNotes = new List<Transform>();

    }

    void updateGame() {

        songTime += Time.deltaTime;

        if (remainingNotes == 0 && playingNotes.Count == 0) {
            songFinished = true;
            return;
        }

        if (remainingNotes > 0) {
            // Check if current note is ready to be selected
            float timeMeasure = (songArray[currentNote][0][4] * 4  + songArray[currentNote][0][5]) * secPerBeat;
            if (songTime >= timeMeasure) {
                // Iterate through each key within current note/chord
                for (int j = 0; j < songArray[currentNote].Count; j++) {
                    int keyRow = (int)Math.Round(songArray[currentNote][j][0]);
                    int keyOctave = (int)Math.Round(songArray[currentNote][j][1]);
                    int keyNote = (int)Math.Round(songArray[currentNote][j][2]);
                    float keyTime = songArray[currentNote][j][3];
                    bool isBackground = songArray[currentNote][j][6] == 1 ? true : false;
                    
                    currentKey = transform.GetChild(keyRow).GetChild(keyOctave).GetChild(keyNote);
                    currentKey.gameObject.GetComponent<Key>().isSelected = true;
                    currentKey.gameObject.GetComponent<Key>().isBackground = isBackground;
                    currentKey.gameObject.GetComponent<Key>().timeSelected = 0.0f;
                    currentKey.gameObject.GetComponent<Key>().keyTime = keyTime;
                    playingNotes.Add(currentKey);
                }
                remainingNotes--;
                currentNote++;
            }
        }
        

        // Iterate through each note/chord currently playing
        int i = 0;
        while (i < playingNotes.Count) {
            bool isSelected = playingNotes[i].gameObject.GetComponent<Key>().isSelected;
            bool isBackground = playingNotes[i].gameObject.GetComponent<Key>().isBackground;
            float timeSelected = playingNotes[i].gameObject.GetComponent<Key>().timeSelected;
            float keyTime = playingNotes[i].gameObject.GetComponent<Key>().keyTime;
            float timeOfNote = keyTime * secPerBeat;
            // Remove note from queue if it's done playing
            if (timeSelected >= timeOfNote) {
                playingNotes[i].gameObject.GetComponent<Key>().isSelected = false;
                playingNotes.RemoveAt(i);
            }
            else {
                i++;
            }
        }

    }

    void displayStart() {
        string difficultyString = "Easy";
        if (difficulty == 1) {
            difficultyString = "Medium";
        }
        else if (difficulty == 2) {
            difficultyString = "Hard";
        }
        difficultyText.text = "Difficulty: " + difficultyString;
        if (isAutoplay) {
            gameModeText.text = "Game Mode: Autoplay";
        }
        else {
            gameModeText.text = "Game Mode: Playing";
        }
        if (Input.GetKeyDown(KeyCode.Space)) {
            titleScreen.SetActive(false);
            ingameScreen.SetActive(true);
            songStarted = true;
        }
    }
    void displayEnd() {
        difficultyText.text = "Final Score: " + currentScore;
        continueText.text = "Press M to return to the main menu...";
        gameModeText.gameObject.SetActive(false);
        titleScreen.SetActive(true);
        ingameScreen.SetActive(false);
    }

}
