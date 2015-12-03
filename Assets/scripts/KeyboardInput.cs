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

        // Get all movement booleans.
        left = Input.GetKey(KeyCode.A);
        right = Input.GetKey(KeyCode.D);
        forward = Input.GetKey(KeyCode.W);
        backward = Input.GetKey(KeyCode.S);

        // Check if the user is tapping/holding space, will make the player jump repeatedly if held.
        jump = Input.GetKey(KeyCode.Space);

        // Let the player move faster!
        sprint = Input.GetKeyDown(KeyCode.LeftShift);

        // To toggle mouse mode.
        if (Input.GetKeyDown(KeyCode.Escape)) {
            toggleMouseLock();
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
