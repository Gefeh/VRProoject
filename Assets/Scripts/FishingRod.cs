using UnityEngine;
using UnityEngine.InputSystem; // works with new Input System (Meta Quest supported)

public class FishingRod : MonoBehaviour
{
    [Header("Rod Setup")]
    [Tooltip("The point on the rod where the line begins (usually tip of the rod).")]
    public Transform rodTip;

    [Header("Lure Setup")]
    [Tooltip("Prefab of the lure (cube placeholder).")]
    public GameObject lurePrefab;

    [Tooltip("Force applied to cast the lure forward.")]
    public float castForce = 5f;

    [Header("Line Renderer Settings")]
    public LineRenderer lineRenderer;

    // Internal
    private GameObject currentLure;
    private Rigidbody lureRb;

    void Start()
    {
        // Ensure LineRenderer is set up
        if (lineRenderer == null)
        {
            lineRenderer = gameObject.AddComponent<LineRenderer>();
        }

        lineRenderer.positionCount = 2;
        lineRenderer.startWidth = .01f;
        lineRenderer.endWidth = .01f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.black;
        lineRenderer.endColor = Color.black;

        lineRenderer.enabled = false; // hidden until lure is spawned
    }

    void Update()
    {
        // --- Cast input (currently SPACE for testing) ---
        if (currentLure == null && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            CastLure();
        }

        // --- Reel input (currently R for testing) ---
        if (currentLure != null && Keyboard.current.rKey.wasPressedThisFrame)
        {
            ReelIn();
        }

        // --- Line Rendering ---
        if (currentLure != null)
        {
            lineRenderer.enabled = true;
            lineRenderer.SetPosition(0, rodTip.position);
            lineRenderer.SetPosition(1, currentLure.transform.position);
        }
        else
        {
            lineRenderer.enabled = false;
        }
    }

    void CastLure()
    {
        // Spawn lure prefab
        currentLure = Instantiate(lurePrefab, rodTip.position, Quaternion.identity);
        lureRb = currentLure.GetComponent<Rigidbody>();
        if (lureRb == null)
        {
            lureRb = currentLure.AddComponent<Rigidbody>();
        }

        // Add forward force
        lureRb.AddForce(rodTip.forward * castForce, ForceMode.VelocityChange);
    }

    void ReelIn()
    {
        if (currentLure != null)
        {
            Destroy(currentLure);
            currentLure = null;
            lureRb = null;
            lineRenderer.enabled = false;
        }
    }
}
