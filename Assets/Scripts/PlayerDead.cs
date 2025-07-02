using UnityEngine;

public class PlayerDead : MonoBehaviour
{
    private GameManager gameManager;
    public bool isDead = false;

    private PlayerController playerController;
    private PlayerHealth playerHealth;
    private Renderer[] renderers;
    private Collider[] colliders;

    private void Start()
    {
        gameManager = GameObject.FindAnyObjectByType<GameManager>();
        playerController = GetComponent<PlayerController>();
        playerHealth = GetComponent<PlayerHealth>();

        renderers = GetComponentsInChildren<Renderer>();
        colliders = GetComponentsInChildren<Collider>();
    }

    public void Die()
    {
        if (isDead) return;
        isDead = true;

        if (gameManager != null)
            gameManager.IncreaseDeadCount();

        if (gameManager != null && playerHealth != null)
            gameManager.StartPlayerRespawn(playerHealth, 1f);

        SetActiveVisuals(false);
    }

    public void ResetDead()
    {
        isDead = false;
        SetActiveVisuals(true);
    }

    private void SetActiveVisuals(bool active)
    {
        foreach (var rend in renderers)
            rend.enabled = active;

        foreach (var col in colliders)
            col.enabled = active;

        if (playerController != null)
            playerController.enabled = active;
    }
}
