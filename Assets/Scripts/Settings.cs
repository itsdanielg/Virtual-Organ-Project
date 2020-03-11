using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Settings : MonoBehaviour {

    public static int row;
    public TextMeshProUGUI currentRowText;
    
    void Start() {
        row = 1;
    }
    
    void Update() {
        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            row++;
            if (row > 7) {
                row = 1;
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow)) {
            row--;
            if (row < 1) {
                row = 7;
            }
        }
        currentRowText.text = "Current Row: " + row.ToString();
    }
}
