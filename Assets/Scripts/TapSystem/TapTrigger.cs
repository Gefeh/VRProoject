using UnityEngine;

public class TapTrigger : MonoBehaviour
{
    [SerializeField] private TapBehaviour tapBehaviour;

    private void Awake()
    {
        if (tapBehaviour == null)
        {
            Debug.LogError("TapBehaviour reference not set on TapTrigger!", this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<GlassBehaviour>(out GlassBehaviour glass))
        {
            tapBehaviour.SelectGlass(glass);

            glass.RegisterWithTap(tapBehaviour);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<GlassBehaviour>(out GlassBehaviour glass))
        {
            tapBehaviour.RemoveGlass();

            glass.UnregisterAllTapEvents();
        }
    }
}