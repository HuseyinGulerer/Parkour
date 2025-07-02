using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    private Rigidbody rb;
    private bool hasFallen = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true; //plane düþmez kitlenir
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!hasFallen && collision.gameObject.CompareTag("Player"))
        {
            hasFallen = true;
            rb.isKinematic = false; //plane düþer
        }
    }
}
