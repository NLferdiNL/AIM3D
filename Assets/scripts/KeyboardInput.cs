using UnityEngine;
using System.Collections;

public class KeyboardInput : MonoBehaviour {

    // To send the movement to.
    [SerializeField]
    private Player player;

    // Set all four movement booleans to false right away.
    [SerializeField]
    private bool forward, left, right, backward, jump, sprint = false;

    // What the player is currently looking at.
    // This Vector2 is used for rotations rather than positions, 
    // it gets set into rotation by the PlayerMovement class.
    private Vector2 lookAt = new Vector2(0, 0);

	void FixedUpdate () {

        // Check keystates.
        keyDown();
        keyUp();

        // Then send these to the player.
        player.move(forward, right, backward, left);

        // The sensetivity is locked at 4, it could be modified, but for now this works fine.
        lookAt.x += Input.GetAxis("Mouse X") * 4;
        lookAt.y +=  -Input.GetAxis("Mouse Y") * 4;

        // Send the lookAt Vector2 to the player to edit the camera.
        player.look(lookAt);

        // If the jump boolean is true, send that to the player too.
        if (jump) {
            player.jump();
        }

        // If the sprint boolean is true, send that to the player too.
        player.movementSpeed(sprint);
	}

    //Check for keys pressed.
    void keyDown() {
        // I used the axis not for velocity but instead just check if they're not 0 then above or below.
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");

        // If it's not 0 check if it's above or below and edit movement booleans accordingly.
        if (hor != 0) {
            if (hor > 0) {
                right = true;
            } else {
                //left = true;
            }
        }

        if (ver != 0) {
            if (ver > 0) {
                forward = true;
            } else {
                backward = true;
            }
        }

        // Check if the user is tapping/holding space, will make the player jump repeatedly if held.
        if (Input.GetKeyDown(KeyCode.Space)) {
            jump = true;
        }

        // Let the player move faster!
        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            sprint = true;
        }

        // To toggle mouse mode.
        if (Input.GetKeyDown(KeyCode.Escape)) {
            toggleMouseLock();
        }
    }

    void keyUp() {
        
        // Here I use these to check if they are 0.
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");

        // Check if hor and ver are 0 and edit movement booleans accordingly.
        if (hor == 0) {
            right = false;
            left = false;
        }

        if (ver == 0) {
            forward = false;
            backward = false;
        }

        // Make sure the player won't be stuck jumping infinitly.
        if (Input.GetKeyUp(KeyCode.Space)) {
            jump = false;
        }

        // Make sure the sprint isn't always on.
        if (Input.GetKeyUp(KeyCode.LeftShift)) {
            sprint = false;
        }
    }

    void toggleMouseLock() {
        if (Cursor.visible) {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            print("Mouse locked");
        } else {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            print("Mouse unlocked");
        }
    }
}
