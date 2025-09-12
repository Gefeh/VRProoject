using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class CrocodileManager : MonoBehaviour
{
    [Header("Crocodiles")]
    [SerializeField] private GameObject _crocodilePrefab;
    [Range(1, 100)]
    public int numberOfCrocodilesAtStart = 10;
    [Tooltip("A live list of all crocodile instances created by this manager.")]
    [SerializeField] public List<Crocodile> spawnedCrocodiles = new List<Crocodile>();

    [Header("Spawn Points")]
    public List<Transform> spawnPoints = new List<Transform>();

    private Transform _spawnPointsParent;
    private Transform _crocodilesParent;

    [Header("Bar")]
    [SerializeField] private GameObject _bar;

    public GameObject Bar { get { return _bar; } private set { _bar = value;  } }
    public GameObject CrocodilePrefab { get { return _crocodilePrefab; } private set { _crocodilePrefab = value;  } }

    /// <summary>
    /// Awake is called before the first frame update, perfect for setup.
    /// </summary>
    void Awake()
    {
        _spawnPointsParent = FindOrCreateParent("SpawnPoints");
        _crocodilesParent = FindOrCreateParent("Crocodiles");
    }

    void Start()
    {
        if (_crocodilePrefab == null)
        {
            Debug.Log("Crocodile Prefab is not assigned. Aborting spawn.");
            return;
        }
        if (spawnPoints.Count == 0)
        {
            Debug.Log("No spawn points have been assigned.");
            return;
        }
        SpawnCrocodiles(numberOfCrocodilesAtStart);
    }

    private void SpawnCrocodiles(int amount)
    {
        int crocodilesToSpawn = Mathf.Min(amount, spawnPoints.Count);
        if (amount > spawnPoints.Count)
        {
            Debug.Log($"Attempting to spawn {amount}, but only {spawnPoints.Count} points are available. Spawning {spawnPoints.Count}.");
        }

        List<Transform> shuffledPoints = spawnPoints.OrderBy(x => Random.value).ToList();

        for (int i = 0; i < crocodilesToSpawn; i++)
        {
            Transform spawnPoint = shuffledPoints[i];
            if (spawnPoint != null)
            {
                GameObject newCrocodile = Instantiate(_crocodilePrefab, spawnPoint.position, spawnPoint.rotation);
                Crocodile croc = newCrocodile.GetComponent<Crocodile>();
                spawnedCrocodiles.Add(croc);
                croc.Initialize(this);
                newCrocodile.transform.SetParent(_crocodilesParent);
            }
        }
        Debug.Log($"Successfully spawned {crocodilesToSpawn} crocodiles.");
    }

    /// <summary>
    /// Helper method to find a child object by name or create it if it doesn't exist.
    /// </summary>
    private Transform FindOrCreateParent(string parentName)
    {
        Transform parentTransform = transform.Find(parentName);

        if (parentTransform == null)
        {
            GameObject parentObject = new GameObject(parentName);
            parentTransform = parentObject.transform;
            parentTransform.SetParent(this.transform);
            parentTransform.localPosition = Vector3.zero;
        }
        return parentTransform;
    }

    private void OnDrawGizmos()
    {
        if (spawnPoints.Count == 0) return;
        foreach (Transform point in spawnPoints)
        {
            if (point != null)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawSphere(point.position, 0.5f);
                Gizmos.color = Color.blue;
                Gizmos.DrawLine(point.position, point.position + point.forward * 2f);
            }
        }
    }
}