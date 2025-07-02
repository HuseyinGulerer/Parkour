using UnityEngine;

public class PanelController : MonoBehaviour
{
    public GameObject startPanel;
    public GameObject gameUIandObjects;
    public GameObject menuButton;  // Men� butonu buraya

    public void CloseStartPanel()
    {
        if (startPanel != null)
            startPanel.SetActive(false);

        if (gameUIandObjects != null)
            gameUIandObjects.SetActive(true);

        if (menuButton != null)
            menuButton.SetActive(true);  // Men� butonunu g�ster
    }
}
