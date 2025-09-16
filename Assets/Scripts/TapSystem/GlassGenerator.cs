using UnityEngine;
using System.Collections;

public class GlassGenerator : MonoBehaviour
{
    [Header("Spawning")]
    [SerializeField] private GameObject glassPrefab;

    [Tooltip("The exact spot where the new glass will be instantiated. If left empty, it will spawn at this object's position.")]
    [SerializeField] private Transform spawnPoint;

    [Header("Cooldown")]
    [Tooltip("The time in seconds to wait before another glass can be spawned.")]
    [SerializeField] private float spawnCooldown = 1.0f;

    private bool canSpawn = true;

    /// <summary>
    /// This is the public method that will be called by the XR Interaction event.
    /// </summary>
    public void SpawnGlass()
    {
        if (!canSpawn || glassPrefab == null)
        {
            return;
        }

        Transform spawnLocation = spawnPoint != null ? spawnPoint : this.transform;

        Instantiate(glassPrefab, spawnLocation.position, spawnLocation.rotation);

        StartCoroutine(CooldownRoutine());
    }

    private IEnumerator CooldownRoutine()
    {
        canSpawn = false;
        yield return new WaitForSeconds(spawnCooldown);
        canSpawn = true;
    }
}