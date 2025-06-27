using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;

    public Slider healthBar;
    public float playerHealth = 50;
    public float speed = 2f;

    private PlayerDead playerdead;

    bool isInLav = false;
    bool isInIce = false;
    bool isInThorn = false;
    bool isDead = false;

    private Vector3 moveDirection;

    private void Start()
    {
        healthBar.maxValue = playerHealth;
        healthBar.value = playerHealth;

        playerdead = GetComponent<PlayerDead>();
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal"); // A-D
        float vertical = Input.GetAxisRaw("Vertical");     // W-S

        // Hareket yönü oluþtur
        moveDirection = new Vector3(horizontal, 0f, vertical).normalized;

        //// Hareket ettir
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);

        //// Eðer hareket ediyorsa yönünü deðiþtir
        if (moveDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }

        if (isInLav)
        {
            playerHealth -= 10 * Time.deltaTime;
            healthBar.value = playerHealth;

            if (playerHealth <= 0)
            {
                playerdead.Die();
                Respawn();
            }
        }

        if (transform.position.y < 0)
        {
            isDead = true;
            playerdead.Die();
            StartCoroutine(RespawnWithDelay());
        }
    }

    public void Respawn()
    {
        gameObject.SetActive(true);
        isDead = false;
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
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Lav"))
        {
            isInLav = false;
            Debug.Log("Lavdan çýktýk");
        }
    }

    public IEnumerator RespawnWithDelay()
    {
        Debug.Log("Düþtü Yeniden Doðdu");
        yield return new WaitForSeconds(1f);
        Respawn();
    }
}
