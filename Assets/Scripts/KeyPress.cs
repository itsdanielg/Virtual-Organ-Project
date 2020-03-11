using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPress : MonoBehaviour {
    
    public KeyCode keyCode;
    private float keyDownPos;
    private Vector3 oldPos;

    void Start() {
        keyDownPos = 0.1f;
        oldPos = transform.localPosition;
    }

    void Update () {
        if (Input.GetKeyDown(keyCode)) {
            Vector3 newPos = oldPos;
            newPos.y -= keyDownPos;
            transform.localPosition = newPos;
        }
        if (Input.GetKeyUp(keyCode)) {
            transform.localPosition = oldPos;
        }
    }

}
