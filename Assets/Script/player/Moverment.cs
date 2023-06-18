using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moverment : MonoBehaviour
{
    public float movementSpeed = 5f; // Speed of movement
    public float rotationSpeed = 100f; // Speed of rotation

    private void Update()
    {
        // Get input from keyboard for movement
        float moveInput = Input.GetAxis("Vertical");
        float rotateInput = Input.GetAxis("Horizontal");

        // Move the object forward or backward
        transform.Translate(Vector3.forward * moveInput * movementSpeed * Time.deltaTime);

        // Rotate the object left or right
        transform.Rotate(Vector3.up * rotateInput * rotationSpeed * Time.deltaTime);
    }

}

