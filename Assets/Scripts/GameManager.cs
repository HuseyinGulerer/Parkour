using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI olumSayacý;
    [SerializeField] private GameObject gameOverPanel;

    private int deadcount = 0;
    private bool isGameOver = false;
    private AudioSource backgroundMusic;
    private AudioSource gameOverAudioSource;

    public int DeadCount => deadcount;

    private void Start()
    {
        backgroundMusic = GameObject.Find("BackgroundMusic").GetComponent<AudioSource>();
        gameOverAudioSource = GetComponent<AudioSource>();

        deadcount = 0;
        UpdateDeadCounterUI();

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        backgroundMusic.Stop();

        Time.timeScale = 1f;
    }

    public void StopBackgroundMusic()
    {
        backgroundMusic.Stop();
    }
    public void StartBackgroundMusic()
    {
        backgroundMusic.Play();
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void IncreaseDeadCount()
    {
        if (isGameOver) return;

        deadcount++;
        UpdateDeadCounterUI();

        if (deadcount >= 3)
            TriggerGameOver();
    }

    private void UpdateDeadCounterUI()
    {
        if (olumSayacý != null)
            olumSayacý.text = "Ölüm Sayacý: " + deadcount;
    }

    private void TriggerGameOver()
    {
        StopBackgroundMusic();
        gameOverAudioSource.Play();

        isGameOver = true;

        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        Time.timeScale = 0f;

    }

    public void StartPlayerRespawn(PlayerHealth playerHealth, float delay)
    {
        StartBackgroundMusic();
        StartCoroutine(RespawnCoroutine(playerHealth, delay));
    }

    private IEnumerator RespawnCoroutine(PlayerHealth playerHealth, float delay)
    {
        yield return new WaitForSeconds(delay);
        playerHealth.Respawn();
    }
}