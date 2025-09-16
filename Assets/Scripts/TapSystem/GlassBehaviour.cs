using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

[RequireComponent(typeof(Renderer))]
public class GlassBehaviour : MonoBehaviour
{
    [Tooltip("The current fill level of the glass.")]
    public int currentVolume = 0;
    [Tooltip("The maximum volume before the glass is considered full.")]
    public int maxVolume = 600;

    public bool isOverflowing => currentVolume > maxVolume;
    public bool ready = false;

    private XRGrabInteractable _grabInteractable;

    private Renderer _renderer;
    private MaterialPropertyBlock _propBlock;
    private static readonly int ColorID = Shader.PropertyToID("_BaseColor");

    void Awake()
    {
        _grabInteractable = GetComponent<XRGrabInteractable>();
        _renderer = GetComponent<Renderer>();
        _propBlock = new MaterialPropertyBlock();
    }

    public void RegisterWithTap(TapBehaviour tap)
    {
        if (tap == null) return;

        _grabInteractable.selectExited.AddListener((args) => tap.RemoveGlass());

        Debug.Log("Glass events registered with tap: " + tap.name);
    }

    public void UnregisterAllTapEvents()
    {
        _grabInteractable.selectExited.RemoveAllListeners();
        Debug.Log("Glass events unregistered.");
    }

    void Start()
    {
        UpdateColor();
    }

    void Update()
    {

    }

    public void Fill()
    {
        currentVolume++;
        UpdateColor();

        if (isOverflowing)
        {
            Debug.Log("Glass is overflowing!");
        }
    }

    /// <summary>
    /// Calculates the correct color based on volume and applies it using a MaterialPropertyBlock.
    /// </summary>
    public void UpdateColor()
    {
        _renderer.GetPropertyBlock(_propBlock);

        Color targetColor;
        if (isOverflowing)
        {
            targetColor = Color.red;
        }
        else if (currentVolume <= 0)
        {
            targetColor = Color.white;
        }
        else if (currentVolume > 0 && currentVolume <= maxVolume / 2)
        {
            targetColor = Color.yellow;
        }
        else
        {
            targetColor = Color.green;
            ready = true;
        }

        _propBlock.SetColor(ColorID, targetColor);
        
        _renderer.SetPropertyBlock(_propBlock);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && ready)
        {
            other.GetComponentInParent<Crocodile>().Satisfy((currentVolume/maxVolume)*100);
            Destroy(this.gameObject);
        }
    }
}
