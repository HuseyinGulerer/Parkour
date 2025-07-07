using UnityEngine;

public class PanelController : MonoBehaviour
{
    public GameObject startPanel;
    public GameObject gameUIandObjects;
    public GameObject menuButton;  // Menü butonu buraya
    public GameObject finishPanel;

    public void CloseStartPanel()
    {
        if (startPanel != null)
            startPanel.SetActive(false);

        if (gameUIandObjects != null)
            gameUIandObjects.SetActive(true);

        if (menuButton != null)
            menuButton.SetActive(true);  // Menü butonunu göster
    }
    public void CloseFinishPanel()
    {
        if (finishPanel != null)
            finishPanel.SetActive(false);
    }
}
