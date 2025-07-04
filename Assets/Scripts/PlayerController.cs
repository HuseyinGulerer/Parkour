using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float rotationSpeed = 7f;
    public float jumpForce = 20f;
    public float bridgeJumpy = 15f;
    public float jumpForceValue = 75f;

    private Vector3 moveDirection;
    private Rigidbody rb;
    private PlayerDead playerDead;
    private bool isGrounded = true;

    private bool isInIce = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerDead = GetComponent<PlayerDead>();
    }

    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector3(horizontal, 0f, vertical).normalized;

        float currentSpeed = isInIce ? 150f : moveSpeed;

        if (moveDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }

        transform.Translate(moveDirection * currentSpeed * Time.deltaTime, Space.World);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }

        if (transform.position.y < 0 && !playerDead.isDead)
        {
            playerDead.Die();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ice"))
        {
            isInIce = true;
        }

        if (collision.contacts[0].normal.y > 0.5f)
        {
            isGrounded = true;
        }

        if (collision.gameObject.CompareTag("Jump"))
        {
            rb.AddForce(Vector3.up * jumpForceValue, ForceMode.Impulse);
        }
        if (collision.gameObject.CompareTag("BridgeJumpy"))
        {
            rb.AddForce(Vector3.up * bridgeJumpy, ForceMode.Impulse);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ice"))
        {
            isInIce = false;
        }
    }

}
