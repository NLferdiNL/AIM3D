using UnityEngine;
using System.Collections;

public class KeyboardInput : MonoBehaviour {

    // To send the movement to.
    [SerializeField]
    private RotateCamera rotateCam;

    // Set all four movement booleans to false right away.
    [SerializeField]
    private bool forward, left, right, backward = false;

	void FixedUpdate () {

        // Check keystates.
        keyDown();

        // Then send these to the player.
        rotateCam.rotate(forward, right, backward, left);
	}

    //Check for keys pressed.
    void keyDown() {

        // Get all movement booleans.
        left = Input.GetKey(KeyCode.A);
        right = Input.GetKey(KeyCode.D);
        forward = Input.GetKey(KeyCode.W);
        backward = Input.GetKey(KeyCode.S);
    }
}
