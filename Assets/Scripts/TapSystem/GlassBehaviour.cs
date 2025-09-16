using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class GlassBehaviour : MonoBehaviour
{
    [Tooltip("The current fill level of the glass.")]
    public int currentVolume = 0;
    [Tooltip("The maximum volume before the glass is considered full.")]
    public int maxVolume = 600;

    public bool isOverflowing => currentVolume > maxVolume;

    private Renderer _renderer;
    private MaterialPropertyBlock _propBlock;
    private static readonly int ColorID = Shader.PropertyToID("_BaseColor");

    void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _propBlock = new MaterialPropertyBlock();
    }

    void Start()
    {
        UpdateColor();
    }

    void Update()
    {
         
         if (Input.GetKey(KeyCode.Space))
         {
             Fill();
         }
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
        }

        _propBlock.SetColor(ColorID, targetColor);
        
        _renderer.SetPropertyBlock(_propBlock);
    }
}