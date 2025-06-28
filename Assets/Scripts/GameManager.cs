using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI deadCounter;
    public GameObject gameOverPanel;
    public GameObject levelCompletePanel; 

    private int deadcount = 0;
    private bool isGameOver = false;
    private bool isLevelComplete = false;

    public void RestartGame()
    {
        Debug.Log("RestartGame tetiklendi!");
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

        if (levelCompletePanel != null)
        {
            levelCompletePanel.SetActive(false);
        }

        Time.timeScale = 1f;
    }

    public void IncreaseDeadCount()
    {
        if (isGameOver || isLevelComplete) return;

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

    public void TriggerLevelComplete()
    {
        if (isLevelComplete) return;

        isLevelComplete = true;

        if (levelCompletePanel != null)
        {
            levelCompletePanel.SetActive(true);
        }

        Time.timeScale = 0f;
        Debug.Log("Level Completed!");
    }

    public void ContinueAfterLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}