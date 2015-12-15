using UnityEngine;
using System.Collections;

public class RotateCamera : MonoBehaviour {
    Transform cam;
    [SerializeField]
    Transform pivot;
    [SerializeField]
    Vector3 vel;

    void Start() {
        cam = transform;
        vel = new Vector3();
    }

    public void rotate(bool forward, bool right, bool backward, bool left) {
        vel = new Vector3();
        if (forward) {
            vel += cam.right / 10;
        }
        if (backward) {
            vel -= cam.right / 10;
        }
        if (left) {
            vel += cam.up / 10;
        }
        if (right) {
            vel -= cam.up / 10;
        }

        if (!forward && !backward && !left && !right) {
            vel = new Vector3();
        }

        cam.RotateAround(pivot.position, vel, 1);
    }
}
