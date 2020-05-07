using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    // Sitting = true; Standing/Walking = false
    public static bool SITTING_STATE;

    Vector2 rotation = new Vector2 (0, 0);
	public float speed = 1.5f;

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
            SITTING_STATE = !SITTING_STATE;
        }

        if (!SITTING_STATE) {
            translation = Input.GetAxis("Vertical") * walkingSpeed * Time.deltaTime;
            strafe = Input.GetAxis("Horizontal") * walkingSpeed * Time.deltaTime;
            transform.Translate(strafe, 0, translation);
        }
        else {
            transform.localPosition = new Vector3(0, 0, 0);
        }

		rotation.y += Input.GetAxis("Mouse X");
		rotation.x += -Input.GetAxis("Mouse Y");
		transform.eulerAngles = (Vector2)rotation * speed;
        
	}
    
}
