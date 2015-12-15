using UnityEngine;
using System.Collections;

public class RotateMe : MonoBehaviour {

    [SerializeField]
    Vector3 rotateBy = new Vector3(0, 0, 0);

	void FixedUpdate () {
        transform.Rotate(rotateBy);
	}
}
