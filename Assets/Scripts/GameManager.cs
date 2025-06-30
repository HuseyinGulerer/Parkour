using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI deadCounter;
    public GameObject gameOverPanel;

    private int deadcount = 0;
    private bool isGameOver = false;

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void Start()
    {
        deadcount = 0;
        UpdateDeadCounterUI();

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }

        Time.timeScale = 1f;
    }

    public void IncreaseDeadCount()
    {
        if (isGameOver) return;

        deadcount++;
        UpdateDeadCounterUI();

        if (deadcount >= 3)
        {
            TriggerGameOver();
        }
    }

    void UpdateDeadCounterUI()
    {
        if (deadCounter != null)
        {
            deadCounter.text = "Dead Counter: " + deadcount.ToString();
        }
    }

    void TriggerGameOver()
    {
        isGameOver = true;

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        Time.timeScale = 0f;
    }

}