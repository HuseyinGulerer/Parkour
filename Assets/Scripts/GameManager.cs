using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI deadCounter;
    [SerializeField] private GameObject gameOverPanel;

    private int deadcount = 0;
    private bool isGameOver = false;


    public int DeadCount => deadcount;

    private void Start()
    {
        deadcount = 0;
        UpdateDeadCounterUI();

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        Time.timeScale = 1f;
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
        if (deadCounter != null)
            deadCounter.text = "Dead Counter: " + deadcount;
    }

    private void TriggerGameOver()
    {
        isGameOver = true;

        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        Time.timeScale = 0f;
    }

    public void StartPlayerRespawn(PlayerHealth playerHealth, float delay)
    {
        StartCoroutine(RespawnCoroutine(playerHealth, delay));
    }

    private IEnumerator RespawnCoroutine(PlayerHealth playerHealth, float delay)
    {
        yield return new WaitForSeconds(delay);
        playerHealth.Respawn();
    }
}