using UnityEngine;
using System.Collections;

public class DetectPlayer : MonoBehaviour
{
    private Crocodile croc;
    void Awake()
    {
        croc = GetComponentInParent<Crocodile>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            croc.nearPlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            croc.nearPlayer = false;
        }
    }
}
