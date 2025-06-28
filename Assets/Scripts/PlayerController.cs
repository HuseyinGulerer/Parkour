using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float rotationSpeed = 7f;

    public Slider healthBar;
    public float playerHealth = 50;

    private PlayerDead playerdead;

    bool isInLav = false;
    bool isInIce = false;
    bool isInThorn = false;

    private Vector3 moveDirection;

    private void Start()
    {
        healthBar.maxValue = playerHealth;
        healthBar.value = playerHealth;

        playerdead = GetComponent<PlayerDead>();
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
    }
    public void Respawn()
    {
        gameObject.SetActive(true);
        playerHealth = 50;
        healthBar.value = playerHealth;
        transform.position = new Vector3(0, 1, 0);
        Debug.Log("Yeniden Doðdu");
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
            Debug.Log("Lavýn içine girdik");
        }

        if (collision.gameObject.CompareTag("Ice"))
        {
            isInIce = true;
            Debug.Log("Buza girdik");
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Lav"))
        {
            isInLav = false;
            Debug.Log("Lavdan çýktýk");
        }

        if (collision.gameObject.CompareTag("Ice"))
        {
            isInIce = false;
            Debug.Log("Buzdan Çýktýk");
        }
    }

    public void StartRespawnDelay()
    {
        StartCoroutine(RespawnWithDelay());
    }

    public IEnumerator RespawnWithDelay()
    {
        Debug.Log("Düþtü Yeniden Doðdu");
        yield return new WaitForSeconds(1f);
        Respawn();
    }
}
