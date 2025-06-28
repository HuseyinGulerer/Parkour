using TMPro;
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
