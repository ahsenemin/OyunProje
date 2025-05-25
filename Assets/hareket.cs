using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [Tooltip("Speed of the ground movement.")]
    public float movementSpeed = 2f;

    [Tooltip("Maximum distance the ground moves back and forth.")]
    public float movementDistance = 5f;

    private Vector3 initialPosition;

    void Start()
    {
        // Store the initial position of the object
        initialPosition = transform.position;
    }

    void Update()
    {
        // Calculate the new Z position using PingPong for smooth back-and-forth movement
        float zOffset = Mathf.PingPong(Time.time * movementSpeed, movementDistance * 2) - movementDistance;
        transform.position = initialPosition + new Vector3(0, 0, zOffset);
    }
}
