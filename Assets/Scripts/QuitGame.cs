using UnityEngine;

public class QuitGame : MonoBehaviour
{
    public void Quit()
    {
        Application.Quit();
        Debug.Log("Oyun kapat�l�yor..."); // Edit�rde �al��maz, sadece build'de �al���r
    }
}
