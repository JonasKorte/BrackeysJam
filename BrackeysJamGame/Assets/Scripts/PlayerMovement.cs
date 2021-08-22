using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    public float speed = 15f;
    public float sprintSpeed = 30f;

    bool isSprinting = false;
    float speedMultiplier = 2f;

    Vector2 input;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        input = Vector2.zero;
    }


    private void Update()
    {
        input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speedMultiplier = sprintSpeed;
            isSprinting = true;
        }
        else
        {
            speedMultiplier = speed;
            isSprinting = false;
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector3(input.x * speedMultiplier * Time.fixedDeltaTime, rb.velocity.y, input.y * speedMultiplier * Time.fixedDeltaTime);
    }
}
