using UnityEngine;

public class TargetCollision : MonoBehaviour
{
    [SerializeField] private string targetTag = "Enemy";

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(targetTag))
        {
            Destroy(collision.gameObject);
        }
    }
}
