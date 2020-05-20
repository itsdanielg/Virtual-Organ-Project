using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    // Sitting = true; Standing/Walking = false
    public static bool SITTING_STATE;

    Vector2 rotation = new Vector2 (0, 0);

    public float walkingSpeed = 25.0f;
    private float translation;
    private float strafe;

    void Start() {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        SITTING_STATE = true;
    }

	void Update () {

        if (Input.GetKeyDown(KeyCode.F)) {
            if (Settings.currentScene == "Organ") {
                SITTING_STATE = !SITTING_STATE;
            }
        }

        if (!SITTING_STATE) {
            translation = Input.GetAxis("Vertical") * walkingSpeed * Time.deltaTime;
            strafe = Input.GetAxis("Horizontal") * walkingSpeed * Time.deltaTime;
            float yAxis = 0;
            if (Input.GetKey(KeyCode.Mouse0)) {
                yAxis = 0.5f;
            }
            else if (Input.GetKey(KeyCode.Mouse1)) {
                yAxis = -0.5f;
            }
            transform.Translate(strafe, yAxis, translation);
        }
        else {
            transform.localPosition = new Vector3(0, 0, 0);
        }

		rotation.y += Input.GetAxis("Mouse X");
		rotation.x += -Input.GetAxis("Mouse Y");
		transform.eulerAngles = (Vector2)rotation * PlayerPrefs.GetFloat("Sensitivity");
        
	}
    
}
