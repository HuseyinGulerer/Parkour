using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI deadCounter;
    private int deadcount = 0;


    public void IncreaseDeadCount()
    {
        deadcount++;
        deadCounter.text = "Dead Counter: " + deadcount.ToString();
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
