using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class SunRotationAObject : MonoBehaviour
{
    public Transform objectToRotate; // Reference to the object you want to rotate- the sun
    public Transform keyPosition; // Reference to the object you want to view
    public float rotationSpeed = 10f; // Speed of rotation
    private void Update()
    {


        
        RotateObject();
    }
    private void RotateObject()
    {
        // Rotate the object around the keyPosition point using the x-axis
        objectToRotate.RotateAround(keyPosition.position, Vector3.left, rotationSpeed * Time.deltaTime);
    }

}

