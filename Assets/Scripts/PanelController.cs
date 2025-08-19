using UnityEngine;

public class PanelController : MonoBehaviour
{
    public GameObject startPanel;
    public GameObject gameUIandObjects;
    public GameObject menuButton;  
    public GameObject finishPanel;

    public void CloseStartPanel()
    {
        if (startPanel != null)
            startPanel.SetActive(false);

        if (gameUIandObjects != null)
            gameUIandObjects.SetActive(true);

        if (menuButton != null)
            menuButton.SetActive(true);  
    }
    public void CloseFinishPanel()
    {
        if (finishPanel != null)
            finishPanel.SetActive(false);
    }
}
