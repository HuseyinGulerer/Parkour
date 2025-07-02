using UnityEngine;

public class GameStarter : MonoBehaviour
{
    public MonoBehaviour playerMovementScript;

    public void StartGame()
    {
        if (playerMovementScript != null)
            playerMovementScript.enabled = true;
    }
}
