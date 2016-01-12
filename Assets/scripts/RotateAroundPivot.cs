using UnityEngine;
using System.Collections;

public class RotateAroundPivot : MonoBehaviour {

    [SerializeField]
    private Transform pivot;
    [SerializeField]
    private Vector3 rotation;

    void Update() {
        transform.RotateAround(pivot.position, rotation, 1);
    }
}
