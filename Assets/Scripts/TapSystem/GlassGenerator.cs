using UnityEditor;
using UnityEngine;

public class GlassGenerator : MonoBehaviour
{
    [SerializeField] GameObject glassPrefab;

    public bool canSpawn = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        Instantiate(glassPrefab);
        canSpawn = false;
    }

    private void OnTriggerExit(Collider other)
    {
        canSpawn = true;
    }
}
