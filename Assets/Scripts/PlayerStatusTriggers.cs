using UnityEngine;

public class PlayerStatusTriggers : MonoBehaviour
{
    private PlayerHealth playerHealth;

    private void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Lav":
                playerHealth.SetInLav(true);
                break;
            case "Ice":
                // Ice durumu PlayerMovement’de var, burada iþleme gerek yok
                break;
            case "Thorn":
                playerHealth.SetInThorn(true);
                break;
            case "Health":
                playerHealth.SetInHealth(true);
                break;
            case "Damage":
                playerHealth.SetInDamage(true);
                break;
            case "Lv2Start":
                playerHealth.ActivateLevel2Planes();
                break;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Lav":
                playerHealth.SetInLav(false);
                break;
            case "Ice":
                // Ice durumu PlayerMovement’de var, burada iþleme gerek yok
                break;
            case "Thorn":
                playerHealth.SetInThorn(false);
                break;
            case "Health":
                playerHealth.SetInHealth(false);
                break;
            case "Damage":
                playerHealth.SetInDamage(false);
                break;
        }
    }

}
