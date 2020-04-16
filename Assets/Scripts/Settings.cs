using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    // black = 1C110F
    // white = FFD99A

    public List<int> selectedKeys;
    public List<Transform> selectedKeyTransforms;
    public const int MAXKEYS = 85;
    public int currentKeyCounter = 0;
    public bool updateList;

    private float keyDownPos;
    private Vector3 oldPos;
    private List<Vector3> oldPosList;
    private List<KeyCode> keyCodes;
    private List<KeyCode> selectedKeyCodes;

    void Start() {

        row = 1;
        highlightWhite = whiteKeyMaterial.color;
        highlightWhite.b = 30.0f/255.0f;
        highlightBlack = blackKeyMaterial.color;
        highlightBlack.g = 50.0f/255.0f;
        selectedKeys = new List<int>(){0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11};
        selectedKeyTransforms = new List<Transform>();
        updateList = false;
        selectedWhite = whiteKeyMaterial.color;
        selectedBlack = blackKeyMaterial.color;

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
            KeyCode.Alpha8
        };
        selectedKeyCodes = new List<KeyCode>();
        
    }
    
    void Update() {
        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            row++;
            if (row > 7) {
                row = 1;
            }
            updateList = true;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow)) {
            row--;
            if (row < 1) {
                row = 7;
            }
            updateList = true;
        }
        if (Input.GetKey(KeyCode.LeftArrow)) {
            if (selectedKeys[0] > 0) {
                for (int i = 0; i < 12; i++) {
                    selectedKeys[i] -= 1;
                }
                updateList = true;
            }
        }
        else if (Input.GetKey(KeyCode.RightArrow)) {
            if (selectedKeys[11] < MAXKEYS) {
                for (int i = 0; i < 12; i++) {
                    selectedKeys[i] += 1;
                }
                updateList = true;
            }
        }
        currentRowText.text = "Current Row: " + row.ToString();

        if (updateList) {
            selectedKeyTransforms.Clear();
            selectedKeyCodes.Clear();
            oldPosList.Clear();
            updateList = false;
        }
        updateKeys();
        updatePos();
    }

    void updateKeys() {
        for (int i = 0; i < 7; i++) {
            Transform organRow = organ.transform.GetChild(i);
            if (row-1 == i) {
                currentKeyCounter = 0;
                foreach (Transform octave in organRow) {
                    if (!octave.gameObject.activeSelf) {
                        if (row == 3) {
                            currentKeyCounter++;
                        }
                        else {
                            currentKeyCounter += 12;
                        }
                        continue;
                    }
                    // Lonely C Key
                    if (octave.childCount == 0) {
                        if (selectedKeys.Contains(currentKeyCounter)) {
                            if (selectedKeyTransforms.Count < 12) {
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
                        if (selectedKeys.Contains(currentKeyCounter)) {
                            if (selectedKeyTransforms.Count < 12) {
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
                        currentKeyCounter++;
                    }
                }
            }
            else {
                foreach (Transform octave in organRow) {
                    if (!octave.gameObject.activeSelf) {
                        continue;
                    }
                    // Lonely C Key
                    if (octave.childCount == 0) {
                        Renderer keyRend = octave.GetComponent<Renderer>();
                        keyRend.material.color = whiteKeyMaterial.color;
                        continue;
                    }
                    foreach (Transform key in octave) {
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

        int keyCodeCount = 0;
        for (int i = 0; i < 12; i++) {
            Transform key = selectedKeyTransforms[i];
            if (oldPosList.Count < 12) {
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
            if (selectedKeyCodes.Count < 12) {
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
                if (i + 1 < 12) {
                    Transform nextKey = selectedKeyTransforms[i+1];
                    if (!keyName.EndsWith("#") && !nextKey.gameObject.name.EndsWith("#")) {
                        keyCodeCount++;
                    }
                }
            }
            
        }
    }

    void updatePos () {
        for (int i = 0; i < 12; i++) {
            if (Input.GetKeyDown(selectedKeyCodes[i])) {
                Transform transform = selectedKeyTransforms[i];
                Vector3 newPos = oldPosList[i];
                newPos.y -= keyDownPos;
                transform.localPosition = newPos;
            }
            if (Input.GetKeyUp(selectedKeyCodes[i])) {
                Transform transform = selectedKeyTransforms[i];
                transform.localPosition = oldPosList[i];
            }
        }
    }
}
