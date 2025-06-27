using UnityEditor.Build.Content;
using UnityEngine;

public class PlayerDead : MonoBehaviour
{
    private GameManager gameManager;
    public bool isDead = false;

    private PlayerController playerController;

    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    public void Die()
    {
        if (isDead) return;
        isDead = true;

        if (gameManager != null)
        {
            gameManager.IncreaseDeadCount();
            playerController.Respawn();
        }
        gameObject.SetActive(false);
    }

    public void ResetDead()
    {
        isDead = false;
    }

    void Update()
    {

    }
}
