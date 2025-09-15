using System.Linq;
using UnityEngine;

public class TargetCollision : MonoBehaviour
{
    [SerializeField] private string[] targetTags;

    private void OnCollisionEnter(Collision collision)
    {
        if (targetTags.Contains(collision.gameObject.tag))
        {
            Destroy(collision.gameObject);
        }
    }
}
