using UnityEditor.Build.Content;
using UnityEngine;

public class PlayerDead : MonoBehaviour
{
    private GameManager gameManager;
    public bool isDead = false;

    private PlayerController playerController;

    void Start()
    {
        gameManager = GameObject.FindAnyObjectByType<GameManager>();
        playerController = GetComponent<PlayerController>();
    }

    public void Die()
    {
        if (isDead) return;
        isDead = true;

        if (gameManager != null)
        {
            gameManager.IncreaseDeadCount();
        }

        if (playerController != null)
        {
            playerController.StartRespawnDelay();
        }

    }
    public void ResetDead()
    {
        isDead = false;
    }

    void Update()
    {

    }
}
