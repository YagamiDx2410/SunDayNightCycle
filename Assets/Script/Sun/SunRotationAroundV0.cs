using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunRotationAroundV0 : MonoBehaviour
{
    public Transform objectToRotate; // Reference to the object you want to rotate
    public float rotationSpeed = 10f; // Speed of rotation

    private void Update()
    {
        RotateObject();
    }

    private void RotateObject()
    {
        // Rotate the object around the origin point (0, 0, 0) using the Y-axis
        objectToRotate.RotateAround(Vector3.zero, Vector3.left, rotationSpeed * Time.deltaTime);
    }
}

