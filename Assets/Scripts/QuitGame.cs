using UnityEngine;

public class QuitGame : MonoBehaviour
{
    public void Quit()
    {
        Application.Quit();
        Debug.Log("Oyun kapatýlýyor..."); // Editörde çalýþmaz, sadece build'de çalýþýr
    }
}
