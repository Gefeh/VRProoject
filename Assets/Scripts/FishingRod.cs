using UnityEngine;
using UnityEngine.InputSystem;

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

    [Header("OpenXR Input")]
    [Tooltip("Right trigger action (OpenXR binding: /user/hand/right/input/trigger/value).")]
    public InputActionReference triggerAction;

    [Tooltip("Right A button action (OpenXR binding: /user/hand/right/input/a/click).")]
    public InputActionReference aButtonAction;

    // Internal
    private GameObject currentLure;
    private Rigidbody lureRb;
    private bool isHeld = false; // NEW: Tracks if the rod is held

    void OnEnable()
    {
        if (triggerAction != null)
            triggerAction.action.performed += OnTriggerPressed;

        if (aButtonAction != null)
            aButtonAction.action.performed += OnAButtonPressed;

        triggerAction?.action.Enable();
        aButtonAction?.action.Enable();
    }

    void OnDisable()
    {
        if (triggerAction != null)
            triggerAction.action.performed -= OnTriggerPressed;

        if (aButtonAction != null)
            aButtonAction.action.performed -= OnAButtonPressed;

        triggerAction?.action.Disable();
        aButtonAction?.action.Disable();
    }

    void Update()
    {
        // Line rendering
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

    private void OnTriggerPressed(InputAction.CallbackContext context)
    {
        if (!isHeld) return; // Only works when held
        if (currentLure == null)
            CastLure();
    }

    private void OnAButtonPressed(InputAction.CallbackContext context)
    {
        if (!isHeld) return; // Only works when held
        if (currentLure != null)
            ReelIn();
    }

    private void CastLure()
    {
        currentLure = Instantiate(lurePrefab, rodTip.position, Quaternion.identity);
        lureRb = currentLure.GetComponent<Rigidbody>();
        if (lureRb == null)
        {
            lureRb = currentLure.AddComponent<Rigidbody>();
        }

        lureRb.AddForce(rodTip.forward * castForce, ForceMode.VelocityChange);
    }

    private void ReelIn()
    {
        if (currentLure != null)
        {
            Destroy(currentLure);
            currentLure = null;
            lureRb = null;
            lineRenderer.enabled = false;
        }
    }

    // --- NEW METHODS ---
    // Call these from your grab system (e.g., XR Grab Interactable events)
    public void SetHeld(bool held)
    {
        isHeld = held;
    }
}

