using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Slider healthBar;
    public float playerHealth = 50f;

    private bool isInLav = false;
    private bool isInThorn = false;
    private bool isInDamage = false;
    private bool isInHealth = false;

    private PlayerDead playerDead;

    private GameObject[] damagePlanes;
    private GameObject[] healthPlanes;

    private void Start()
    {
        healthBar.maxValue = playerHealth;
        healthBar.value = playerHealth;

        playerDead = GetComponent<PlayerDead>();

        damagePlanes = GameObject.FindGameObjectsWithTag("Damage");
        healthPlanes = GameObject.FindGameObjectsWithTag("Health");

        foreach (GameObject plane in damagePlanes)
            plane.SetActive(false);

        foreach (GameObject plane in healthPlanes)
            plane.SetActive(false);
    }

    private void Update()
    {
        if (isInLav && !playerDead.isDead && playerHealth > 0)
        {
            playerHealth -= 10 * Time.deltaTime;
            UpdateHealthBar();
            CheckDeath();
        }

        if (isInThorn && !playerDead.isDead)
        {
            playerHealth -= 3f * Time.deltaTime;
            UpdateHealthBar();
            CheckDeath();
        }

        if (isInDamage && !playerDead.isDead)
        {
            playerHealth -= 7 * Time.deltaTime;
            UpdateHealthBar();
            CheckDeath();
        }

        if (isInHealth && !playerDead.isDead)
        {
            playerHealth += 3f * Time.deltaTime;
            if (playerHealth > healthBar.maxValue)
                playerHealth = healthBar.maxValue;

            UpdateHealthBar();
        }
    }
    public void ActivateLevel2Planes()
    {
        foreach (GameObject plane in damagePlanes)
            plane.SetActive(true);

        foreach (GameObject plane in healthPlanes)
            plane.SetActive(true);
    }

    public void Respawn()
    {
        gameObject.SetActive(true);    // Oyun objesini aç
        playerHealth = healthBar.maxValue; // tam doldur
        UpdateHealthBar();
        transform.position = new Vector3(0, 1, 0);

        isInLav = false;
        isInDamage = false;
        isInThorn = false;
        isInHealth = false;

        playerDead.ResetDead();

        CheckDeath();
    }

    public void StartRespawnDelay()
    {
        Debug.Log("Respawn baþlatýlýyor");
        StartCoroutine(RespawnWithDelay());
    }

    private IEnumerator RespawnWithDelay()
    {
        yield return new WaitForSeconds(1f);
        Respawn();
    }

    private void UpdateHealthBar()
    {
        healthBar.value = playerHealth;
    }

    private void CheckDeath()
    {
        if (playerHealth <= 0)
            playerDead.Die();
    }

    // Bool set/get metotlarý PlayerStatusTriggers scriptinden kontrol için.
    public void SetInLav(bool val) => isInLav = val;
    public void SetInThorn(bool val) => isInThorn = val;
    public void SetInDamage(bool val) => isInDamage = val;
    public void SetInHealth(bool val) => isInHealth = val;
}
