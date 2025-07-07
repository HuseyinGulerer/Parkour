using UnityEngine;

public class FinishTrigger : MonoBehaviour
{
    [SerializeField] private GameObject FinishPanel;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FinishPanel.SetActive(true);
        }
    }
}
