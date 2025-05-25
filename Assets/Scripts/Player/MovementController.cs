/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] private float speed;
    //[SerializeField] private float sensitivity;

    private Rigidbody rb;
    private Animator anim;

    private Vector3 input;
    //private float mouseInput;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        UpdateAnimations();

        
    }

    private void UpdateAnimations()
    {

    }
    private void FixedUpdate()
    {
        rb.MovePosition(transform.position + transform.TransformDirection(input)*speed*Time.fixedDeltaTime);
        //rb.velocity += ((transform.forward * input.z) + (transform.right * input.x))*speed; kayarak gitsin istiyorsan a�
    }
}

*/