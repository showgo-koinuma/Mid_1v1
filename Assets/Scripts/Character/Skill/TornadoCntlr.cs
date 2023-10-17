using UnityEngine;

public class TornadoCntlr : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Rigidbody rb))
        {
            rb.velocity = Vector3.up * 10;
            Debug.Log("hit");
        }
    }
}
