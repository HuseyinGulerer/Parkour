using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float rotationSpeed = 7f;
    public float jumpForce = 5f;

    public Slider healthBar;
    public float playerHealth = 50;

    private PlayerDead playerdead;

    private GameObject[] damagePlanes;
    private GameObject[] healthPlanes;

    bool isInLav = false;
    bool isInIce = false;
    bool isInThorn = false;
    bool isInDamage = false;
    bool isInHealth = false;

    private Vector3 moveDirection;
    private Rigidbody rb;
    private bool isGrounded = true;

    private void Start()
    {
        healthBar.maxValue = playerHealth;
        healthBar.value = playerHealth;

        playerdead = GetComponent<PlayerDead>();
        rb = GetComponent<Rigidbody>();

        damagePlanes = GameObject.FindGameObjectsWithTag("Damage");
        healthPlanes = GameObject.FindGameObjectsWithTag("Health");

        foreach (GameObject plane in damagePlanes)
        {
            plane.SetActive(false);
        }

        foreach (GameObject plane in healthPlanes)
        {
            plane.SetActive(false);
        }
    }

    public void ActivateLevel2Planes()
    {
        foreach (GameObject plane in damagePlanes)
        {
            plane.SetActive(true);
        }

        foreach (GameObject plane in healthPlanes)
        {
            plane.SetActive(true);
        }
    }


    void Update()
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

        if (isInLav)
        {
            playerHealth -= 10 * Time.deltaTime;
            healthBar.value = playerHealth;

            if (playerHealth <= 0)
            {
                playerdead.Die();
            }
        }

        if (transform.position.y < 0)
        {
            playerdead.Die();
        }

        if (isInThorn)
        {
            playerHealth -= 3 * Time.deltaTime;
            healthBar.value = playerHealth;

            if (playerHealth <= 0)
            {
                playerdead.Die();
            }
        }

        if (isInDamage)
        {
            playerHealth -= 7 * Time.deltaTime;
            healthBar.value = playerHealth;

            if (playerHealth <= 0)
            {
                playerdead.Die();
            }
        }

        if (isInHealth)
        {
            playerHealth += 3 * Time.deltaTime;
            healthBar.value = playerHealth;
        }

    }
    public void Respawn()
    {
        gameObject.SetActive(true);
        playerHealth = 50;
        healthBar.value = playerHealth;
        transform.position = new Vector3(0, 1, 0);

        PlayerDead deadscript = gameObject.GetComponent<PlayerDead>();
        if (deadscript != null)
        {
            deadscript.ResetDead();
        }
    }



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Lav"))
        {
            isInLav = true;
        }

        if (collision.gameObject.CompareTag("Ice"))
        {
            isInIce = true;
        }

        if (collision.gameObject.CompareTag("Thorn"))
        {
            isInThorn = true;
        }

        if (collision.contacts[0].normal.y > 0.5f)
        {
            isGrounded = true;
        }

        if (collision.gameObject.CompareTag("Lv2Start"))
        {
            ActivateLevel2Planes();
        }

        if (collision.gameObject.CompareTag("Health"))
        {
            isInHealth = true;
        }

        if (collision.gameObject.CompareTag("Damage"))
        {
            isInDamage = true;
        }

    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Lav"))
        {
            isInLav = false;
        }

        if (collision.gameObject.CompareTag("Ice"))
        {
            isInIce = false;
        }

        if (collision.gameObject.CompareTag("Thorn"))
        {
            isInThorn = false;
        }

        if (collision.gameObject.CompareTag("Health"))
        {
            isInHealth = false;
        }

        if (collision.gameObject.CompareTag("Damage"))
        {
            isInDamage = false;
        }

    }

    public void StartRespawnDelay()
    {
        StartCoroutine(RespawnWithDelay());
    }

    public IEnumerator RespawnWithDelay()
    {
        yield return new WaitForSeconds(1f);
        Respawn();
    }
}
