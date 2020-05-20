using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SongOne : MonoBehaviour {

    public GameObject organGameObject;
    public TextMeshProUGUI currentText;
    private Transform organ;
    private int keyCount;
    private int currentScore;
    private bool songFinished;
    private Color keyToPressColor;

    // Start is called before the first frame update
    void Start() {
        organ = organGameObject.transform;
        keyCount = 0;
        currentScore = 0;
        songFinished = false;
        keyToPressColor = Color.red;
    }

    // Update is called once per frame
    void Update() {

        Transform organRow = null;
        Transform organOctave = null;
        Transform organKey = null;

        switch (keyCount) {
            case 0:
                organRow = organ.GetChild(1);
                organOctave = organRow.GetChild(3);
                organKey = organOctave.GetChild(0);
                break;
            case 1:
                organRow = organ.GetChild(1);
                organOctave = organRow.GetChild(3);
                organKey = organOctave.GetChild(0);
                break;
            case 2:
                organRow = organ.GetChild(1);
                organOctave = organRow.GetChild(3);
                organKey = organOctave.GetChild(2);
                break;
            case 3:
                organRow = organ.GetChild(1);
                organOctave = organRow.GetChild(3);
                organKey = organOctave.GetChild(0);
                break;
            case 4:
                organRow = organ.GetChild(1);
                organOctave = organRow.GetChild(3);
                organKey = organOctave.GetChild(5);
                break;
            case 5:
                organRow = organ.GetChild(1);
                organOctave = organRow.GetChild(3);
                organKey = organOctave.GetChild(4);
                break;
            case 6:
                organRow = organ.GetChild(1);
                organOctave = organRow.GetChild(3);
                organKey = organOctave.GetChild(0);
                break;
            case 7:
                organRow = organ.GetChild(1);
                organOctave = organRow.GetChild(3);
                organKey = organOctave.GetChild(0);
                break;
            case 8:
                organRow = organ.GetChild(1);
                organOctave = organRow.GetChild(3);
                organKey = organOctave.GetChild(2);
                break;
            case 9:
                organRow = organ.GetChild(1);
                organOctave = organRow.GetChild(3);
                organKey = organOctave.GetChild(0);
                break;
            case 10:
                organRow = organ.GetChild(1);
                organOctave = organRow.GetChild(3);
                organKey = organOctave.GetChild(7);
                break;
            case 11:
                organRow = organ.GetChild(1);
                organOctave = organRow.GetChild(3);
                organKey = organOctave.GetChild(5);
                break;
            case 12:
                organRow = organ.GetChild(1);
                organOctave = organRow.GetChild(3);
                organKey = organOctave.GetChild(0);
                break;
            case 13:
                organRow = organ.GetChild(1);
                organOctave = organRow.GetChild(3);
                organKey = organOctave.GetChild(0);
                break;
            case 14:
                organRow = organ.GetChild(1);
                organOctave = organRow.GetChild(4);
                organKey = organOctave.GetChild(0);
                break;
            case 15:
                organRow = organ.GetChild(1);
                organOctave = organRow.GetChild(3);
                organKey = organOctave.GetChild(9);
                break;
            case 16:
                organRow = organ.GetChild(1);
                organOctave = organRow.GetChild(3);
                organKey = organOctave.GetChild(5);
                break;
            case 17:
                organRow = organ.GetChild(1);
                organOctave = organRow.GetChild(3);
                organKey = organOctave.GetChild(5);
                break;
            case 18:
                organRow = organ.GetChild(1);
                organOctave = organRow.GetChild(3);
                organKey = organOctave.GetChild(4);
                break;
            case 19:
                organRow = organ.GetChild(1);
                organOctave = organRow.GetChild(3);
                organKey = organOctave.GetChild(2);
                break;
            case 20:
                organRow = organ.GetChild(1);
                organOctave = organRow.GetChild(3);
                organKey = organOctave.GetChild(10);
                break;
            case 21:
                organRow = organ.GetChild(1);
                organOctave = organRow.GetChild(3);
                organKey = organOctave.GetChild(10);
                break;
            case 22:
                organRow = organ.GetChild(1);
                organOctave = organRow.GetChild(3);
                organKey = organOctave.GetChild(9);
                break;
            case 23:
                organRow = organ.GetChild(1);
                organOctave = organRow.GetChild(3);
                organKey = organOctave.GetChild(5);
                break;
            case 24:
                organRow = organ.GetChild(1);
                organOctave = organRow.GetChild(3);
                organKey = organOctave.GetChild(7);
                break;
            case 25:
                organRow = organ.GetChild(1);
                organOctave = organRow.GetChild(3);
                organKey = organOctave.GetChild(5);
                break;
            default:
                organRow = null;
                organOctave = null;
                organKey = null;
                songFinished = true;
                break;
        }

        currentText.text = "Score: " + currentScore;

        if (!songFinished) {
            Renderer keyRend = organKey.GetComponent<Renderer>();
            keyRend.material.color = keyToPressColor;
            float yPos = organKey.localPosition.y;
            if (yPos < 0 || (yPos > 0 && yPos < 0.1)) {
                keyCount++;
                currentScore += 20;
            }
        }

    }
}
