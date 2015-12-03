using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    // Used to move the player with the move method invoked by the input class.
    private Transform tf;

    // I want to prevent the player rolling down stairs so I use this to set the X and Z velocity to 0
    // when the player isn't moving.
    private Rigidbody rb;

    // Can't have the player jump infinitely. Used by the jump method to prevent that.
    [SerializeField]
    private bool touchingGround = false;

    // Jump height, yet to decide if this is changable during runtime.
    [SerializeField]
    private int jumpHeight = 5;

    // Both walkSpeed and sprintSpeed are better the lower they get, so walkSpeed is 30 and sprintSpeed is 15.
    [SerializeField]
    private int walkSpeed = 30;

    [SerializeField]
    private int sprintSpeed = 15;

    // Because the constructor sets it to walkSpeed I leave it empty.
    private int currentSpeed;

    // Asks for a Camera Component on purpose, this way I can't accidently
    // select the wrong object in case I need to set it again.
    [SerializeField]
    private Camera _gameCamera;

    // This is mainly used by the class to move the camera, is taken out of the variable set above.
    private Transform gameCamera;

	void Start () {
        // Incase I did forget to set the camera, notify me in a friendly way rather than Unity's.
        if (_gameCamera == null) {
            print("No camera assigned to Player!");
        } else {
            gameCamera = _gameCamera.gameObject.GetComponent<Transform>();
        }

        // Because I want the walkSpeed to be changable in the editor I set it here.
        currentSpeed = walkSpeed;

        // Get my Transform and Rigidbody components.
        tf = this.GetComponent<Transform>();
        rb = this.GetComponent<Rigidbody>();
	}

    void Update() {
        // Move the camera with the player body, slightly raised.
        gameCamera.position = tf.position + new Vector3(0, 0.57f, 0);
    }

    // Check if the player is touching the ground.
    void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "Ground") {
            touchingGround = true;
        }
    }

    // If the player jumped I don't want him to be able to repeat that. So check if no collision with the ground is found.
    void OnCollisionLeave(Collision other) {
        if (other.gameObject.tag == "Ground") {
            touchingGround = false;
        }
    }

    /// <summary>
    /// Used by input classes to make the player move.
    /// </summary>
    /// <param name="forward">Should I move forward?</param>
    /// <param name="right">Should I move right?</param>
    /// <param name="backward">Should I move backward?</param>
    /// <param name="left">Should I move left?</param>
    // Invoked solely outside of this class, by input classes.
    public void move(bool forward, bool right, bool backward, bool left) {
        // I use currentSpeed to allow sprinting.
        if (forward) {
            tf.position += tf.forward / currentSpeed;
        }
        if (backward) {
            tf.position -= tf.forward / currentSpeed;
        }
        if (left) {
            tf.position -= tf.right / currentSpeed;
        }
        if (right) {
            tf.position += tf.right / currentSpeed;
        }

        // If the player gives no input. Set X and Z velocity to 0 to prevent rolling off slopes/stairs.
        if(forward == false && right == false && backward == false && left == false) {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }
    }

    /// <summary>
    /// Make me look at some thing. Vector2 given is the rotation.
    /// </summary>
    /// <param name="pos"></param>
    // Used solely outside this class by input classes to make me look at things.
    public void look(Vector2 pos) {
        // Can't have the player rotate his camera upside down, that would be neck breaking.
        if (pos.y > 50) {
            pos.y = 50;
        } else if (pos.y < -50) {
            pos.y = -50;
        }

        // Edit rotation based on the positions given to the method, may look odd but that's how Euler functions.
        // Also had to do a slight workaround with the X axis because it would snap back into place if edited like I
        // originally intended.
        gameCamera.rotation = Quaternion.Euler(new Vector3(pos.y, pos.x, 0));
        tf.rotation = Quaternion.Euler(new Vector3(0, pos.x, 0));
    }

    /// <summary>
    /// Make me jump a preset jumpHeight.
    /// </summary>
    // Another movement method invoked outside this class only by input classes.
    public void jump() {
        if (touchingGround) {
            rb.AddForce(new Vector3(0, jumpHeight, 0),ForceMode.Impulse);
            touchingGround = false;
        }
    }

    /// <summary>
    /// Change my speed depending on the boolean.
    /// </summary>
    /// <param name="sprinting">Am I sprinting?</param>
    public void movementSpeed(bool sprinting) {
        // Just to make sure we aren't constantly setting the value.
        if (sprinting) {
            // I am sprinting! Set my speed accordingly.
            if (currentSpeed != sprintSpeed) {
                currentSpeed = sprintSpeed;
            }
        } else {
            // I am walking, set my speed accordingly.
            if (currentSpeed != walkSpeed) {
                currentSpeed = walkSpeed;
            }
        }
    }
}
