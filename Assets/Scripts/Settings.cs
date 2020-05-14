using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Settings : MonoBehaviour {

    public static int row;
    public TextMeshProUGUI currentRowText;

    public GameObject organ;
    public Material blackKeyMaterial;
    public Material whiteKeyMaterial;

    public Color highlightWhite;
    public Color highlightBlack;

    public Color selectedWhite;
    public Color selectedBlack;

    private float highlightWhiteR = 102.0f/255.0f;
    private float highlightWhiteG = 102.0f/255.0f;
    private float highlightWhiteB = 0.0f/255.0f;

    private float highlightBlackR = 0.0f/255.0f;
    private float highlightBlackG = 102.0f/255.0f;
    private float highlightBlackB = 0.0f/255.0f;

    private float selectedWhiteR = 255.0f/255.0f;
    private float selectedWhiteG = 255.0f/255.0f;
    private float selectedWhiteB = 0.0f/255.0f;
    
    private float selectedBlackR = 0.0f/255.0f;
    private float selectedBlackG = 255.0f/255.0f;
    private float selectedBlackB = 0.0f/255.0f;

    // black = 1C110F
    // white = FFD99A

    public List<int> selectedKeys;
    public List<Transform> selectedKeyTransforms;
    public const int MAXKEYS = 85;
    public int currentKeyCounter = 0;
    public const int KEYS_TO_PRESS = 17; // MAX IS 17
    public bool updateList;

    private float keyDownPos;
    private Vector3 oldPos;
    private List<Vector3> oldPosList;
    private List<KeyCode> keyCodes;
    private List<KeyCode> selectedKeyCodes;

    private bool[] keyPressed;

    void Start() {

        row = 1;
        highlightWhite = new Color(highlightWhiteR, highlightWhiteG, highlightWhiteB, 1.0f);
        highlightBlack = new Color(highlightBlackR, highlightBlackG, highlightBlackB, 1.0f);
        selectedWhite = new Color(selectedWhiteR, selectedWhiteG, selectedWhiteB, 1.0f);
        selectedBlack = new Color(selectedBlackR, selectedBlackG, selectedBlackB, 1.0f);
        selectedKeys = new List<int>();
        selectedKeyTransforms = new List<Transform>();
        updateList = false;
        keyDownPos = 0.1f;
        oldPosList = new List<Vector3>();
        keyCodes = new List<KeyCode>(){
            KeyCode.Alpha1,
            KeyCode.Q,
            KeyCode.Alpha2,
            KeyCode.W,
            KeyCode.Alpha3,
            KeyCode.E,
            KeyCode.Alpha4,
            KeyCode.R,
            KeyCode.Alpha5,
            KeyCode.T,
            KeyCode.Alpha6,
            KeyCode.Y,
            KeyCode.Alpha7,
            KeyCode.U,
            KeyCode.Alpha8,
            KeyCode.I,
            KeyCode.Alpha9,
            KeyCode.O,
            KeyCode.Alpha0,
            KeyCode.P,
            KeyCode.Minus
        };
        selectedKeyCodes = new List<KeyCode>();

        keyPressed = new bool[KEYS_TO_PRESS];
        for (var i = 0; i < KEYS_TO_PRESS; i++) {
            keyPressed[i] = false;
            selectedKeys.Add(i);
        }
        
    }
    
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1, LoadSceneMode.Single);
            return;
        }
        
        if (CameraController.SITTING_STATE) {
            currentRowText.text = "Current State: Sitting";
            if (Input.GetKeyDown(KeyCode.UpArrow) && !areKeysPressed()) {
                row++;
                if (row > 7) {
                    row = 1;
                }
                // LIMIT CHECK
                if (row == 3) {
                    if (selectedKeys[0] < 7) {
                        resetSelected(7, 7 + KEYS_TO_PRESS);
                    }
                    else if (selectedKeys[KEYS_TO_PRESS-1] > 79) {
                        resetSelected(80 - KEYS_TO_PRESS, 80);
                    }
                }
                else if (row > 3) {
                    if (selectedKeys[0] < 24) {
                        resetSelected(24, 24 + KEYS_TO_PRESS);
                    }
                    else if (selectedKeys[KEYS_TO_PRESS-1] > MAXKEYS - 1) {
                        resetSelected(MAXKEYS - KEYS_TO_PRESS, MAXKEYS);
                    }
                }
                updateList = true;
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) && !areKeysPressed()) {
                row--;
                if (row < 1) {
                    row = 7;
                }
                // LIMIT CHECK
                if (row == 3) {
                    if (selectedKeys[0] < 7) {
                        resetSelected(7, 7 + KEYS_TO_PRESS);
                    }
                    else if (selectedKeys[KEYS_TO_PRESS-1] > 79) {
                        resetSelected(80 - KEYS_TO_PRESS, 80);
                    }
                }
                else if (row > 3) {
                    if (selectedKeys[0] < 24) {
                        resetSelected(24, 24 + KEYS_TO_PRESS);
                    }
                    else if (selectedKeys[KEYS_TO_PRESS-1] > MAXKEYS - 1) {
                        resetSelected(MAXKEYS - KEYS_TO_PRESS, MAXKEYS);
                    }
                }
                updateList = true;
            }
            if (Input.GetKey(KeyCode.LeftArrow) && !areKeysPressed()) {
                
                if (selectedKeys[0] > 0) {
                    for (int i = 0; i < KEYS_TO_PRESS; i++) {
                        selectedKeys[i] -= 1;
                    }
                    // LIMIT CHECK
                    if (row == 3) {
                        if (selectedKeys[0] < 7) {
                            resetSelected(7, 7 + KEYS_TO_PRESS);
                        }
                        else if (selectedKeys[KEYS_TO_PRESS-1] > 79) {
                            resetSelected(80 - KEYS_TO_PRESS, 80);
                        }
                    }
                    else if (row > 3) {
                        if (selectedKeys[0] < 24) {
                            resetSelected(24, 24 + KEYS_TO_PRESS);
                        }
                        else if (selectedKeys[KEYS_TO_PRESS-1] > MAXKEYS - 1) {
                            resetSelected(MAXKEYS - KEYS_TO_PRESS, MAXKEYS);
                        }
                    }
                    updateList = true;
                }
            }
            else if (Input.GetKey(KeyCode.RightArrow) && !areKeysPressed()) {
                if (selectedKeys[KEYS_TO_PRESS-1] < MAXKEYS - 1) {
                    for (int i = 0; i < KEYS_TO_PRESS; i++) {
                        selectedKeys[i] += 1;
                    }
                    // LIMIT CHECK
                    if (row == 3) {
                        if (selectedKeys[0] < 7) {
                            resetSelected(7, 7 + KEYS_TO_PRESS);
                        }
                        else if (selectedKeys[KEYS_TO_PRESS-1] > 79) {
                            resetSelected(80 - KEYS_TO_PRESS, 80);
                        }
                    }
                    else if (row > 3) {
                        if (selectedKeys[0] < 24) {
                            resetSelected(24, 24 + KEYS_TO_PRESS);
                        }
                        else if (selectedKeys[KEYS_TO_PRESS-1] > MAXKEYS - 1) {
                            resetSelected(MAXKEYS - KEYS_TO_PRESS, MAXKEYS);
                        }
                    }
                    updateList = true;
                }
            }

            if (updateList) {
                selectedKeyTransforms.Clear();
                selectedKeyCodes.Clear();
                oldPosList.Clear();
                updateList = false;
            }
            updateKeys();
            updatePos();
        }
        else {
            currentRowText.text = "Current State: Standing";
        }
    }

    void updateKeys() {
        for (int i = 0; i < 7; i++) {
            Transform organRow = organ.transform.GetChild(i);
            if (row-1 == i) {
                currentKeyCounter = 0;
                foreach (Transform octave in organRow) {
                    // Lonely C Key
                    if (octave.childCount == 0) {
                        if (selectedKeys.Contains(currentKeyCounter)) {
                            if (selectedKeyTransforms.Count < KEYS_TO_PRESS) {
                                selectedKeyTransforms.Add(octave);
                            }
                        }
                        else {
                            Renderer keyRend = octave.GetComponent<Renderer>();
                            keyRend.material.color = highlightWhite;
                        }
                        continue;
                    }
                    foreach (Transform key in octave) {
                        if (key.gameObject.activeSelf) {
                            if (selectedKeys.Contains(currentKeyCounter)) {
                                if (selectedKeyTransforms.Count < KEYS_TO_PRESS) {
                                    selectedKeyTransforms.Add(key);
                                }
                            }
                            else {
                                string keyName = key.gameObject.name; 
                                Renderer keyRend = key.GetComponent<Renderer>();
                                if (keyName.EndsWith("#")) {
                                    keyRend.material.color = highlightBlack;
                                }
                                else {
                                    keyRend.material.color = highlightWhite;
                                }
                            }
                        }
                        currentKeyCounter++;
                    }
                }
            }
            else {
                foreach (Transform octave in organRow) {
                    // Lonely C Key
                    if (octave.childCount == 0) {
                        Renderer keyRend = octave.GetComponent<Renderer>();
                        keyRend.material.color = whiteKeyMaterial.color;
                        continue;
                    }
                    foreach (Transform key in octave) {
                        if (key.gameObject.activeSelf) {
                            string keyName = key.gameObject.name;
                            Renderer keyRend = key.GetComponent<Renderer>();
                            if (keyName.EndsWith("#")) {
                                keyRend.material.color = blackKeyMaterial.color;
                            }
                            else {
                                keyRend.material.color = whiteKeyMaterial.color;
                            }
                        }
                    }
                }
            }
        }

        int keyCodeCount = 0;
        for (int i = 0; i < KEYS_TO_PRESS; i++) {
            Transform key = selectedKeyTransforms[i];
            if (oldPosList.Count < KEYS_TO_PRESS) {
                oldPosList.Add(key.localPosition);
            }
            string keyName = key.gameObject.name;
            Renderer keyRend = key.GetComponent<Renderer>();
            if (keyName.EndsWith("#")) {
                keyRend.material.color = selectedBlack;
            }
            else {
                keyRend.material.color = selectedWhite;
            }
            if (selectedKeyCodes.Count < KEYS_TO_PRESS) {
                if (selectedKeyCodes.Count == 0) {
                    if (keyName.EndsWith("#")) {
                        selectedKeyCodes.Add(keyCodes[keyCodeCount]);
                        keyCodeCount++;
                    }
                    else {
                        selectedKeyCodes.Add(keyCodes[keyCodeCount+1]);
                        keyCodeCount += 2;
                    }
                }
                else {
                    selectedKeyCodes.Add(keyCodes[keyCodeCount]);
                    keyCodeCount++;
                }
                if (i + 1 < KEYS_TO_PRESS) {
                    Transform nextKey = selectedKeyTransforms[i+1];
                    if (!keyName.EndsWith("#") && !nextKey.gameObject.name.EndsWith("#")) {
                        keyCodeCount++;
                    }
                }
            }
            
        }
    }

    void updatePos () {
        for (int i = 0; i < KEYS_TO_PRESS; i++) {
            if (Input.GetKeyDown(selectedKeyCodes[i])) {
                Transform transform = selectedKeyTransforms[i];
                Vector3 newPos = oldPosList[i];
                newPos.y -= keyDownPos;
                transform.localPosition = newPos;
                // Play Sound
                var sound = transform.gameObject.GetComponent<AudioSource>();
                sound.time = 0.9f;
                sound.volume = PlayerPrefs.GetFloat("Volume")/100.0f;
                sound.Play();
                keyPressed[i] = true;
            }
            if (Input.GetKeyUp(selectedKeyCodes[i])) {
                Transform transform = selectedKeyTransforms[i];
                transform.localPosition = oldPosList[i];
                // Stop Sound
                var sound = transform.gameObject.GetComponent<AudioSource>();
                sound.Stop();
                keyPressed[i] = false;;
            }
        }
    }

    bool areKeysPressed() {
        for (var i = 0; i < keyPressed.Length; i++) {
            if (keyPressed[i]) {
                return true;
            }
        }
        return false;
    }

    void resetSelected(int lower, int upper) {
        int index = 0;
        for (int i = lower; i < upper; i++) {
            selectedKeys[index] = i;
            index++;
        }
    }

}
