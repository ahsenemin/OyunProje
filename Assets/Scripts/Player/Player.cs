using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    private Animator anim;
    private Vector3 input;
    private Rigidbody rb;

    private Vector3 lastPosition; // 💡 Önceki pozisyonu tutmak için

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        lastPosition = transform.position;
    }

    private void Update()
    {
        Vector2 inputVector = Vector2.zero;
        if (Input.GetKey(KeyCode.W)) inputVector.x += 1;
        if (Input.GetKey(KeyCode.S)) inputVector.x -= 1;
        if (Input.GetKey(KeyCode.A)) inputVector.y += 1;
        if (Input.GetKey(KeyCode.D)) inputVector.y -= 1;

        inputVector = inputVector.normalized;
        input = new Vector3(inputVector.x, 0f, inputVector.y);
    }

    private void FixedUpdate()
    {
        Vector3 moveDirection = input;

        if (moveDirection != Vector3.zero)
        {
            rb.MovePosition(transform.position + moveDirection * moveSpeed * Time.fixedDeltaTime);

            float rotationSpeed = 15f;
            transform.forward = Vector3.Slerp(transform.forward, moveDirection, Time.fixedDeltaTime * rotationSpeed);
        }

        UpdateAnimations(); // 💡 Hareketin gerçekten gerçekleşip gerçekleşmediğini burada kontrol edeceğiz

        lastPosition = transform.position; // 🔁 Pozisyonu güncelle
    }

    private void UpdateAnimations()
    {
        // 💡 Gerçek pozisyon farkı kontrolü
        float distanceMoved = Vector3.Distance(transform.position, lastPosition);
        bool isMoving = distanceMoved > 0.001f;

        anim.SetBool("isMoving", isMoving);
    }
}

