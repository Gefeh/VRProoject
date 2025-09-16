using System.Linq;
using UnityEngine;

public class TargetCollision : MonoBehaviour
{
    [SerializeField] private string[] targetTags;

    private void OnTriggerEnter(UnityEngine.Collider other)
    {
        if (targetTags.Contains(other.gameObject.tag))
        {
            Destroy(other.gameObject.GetComponentInParent<Crocodile>().gameObject);
        }
    }
}
