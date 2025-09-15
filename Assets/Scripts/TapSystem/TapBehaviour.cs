using JetBrains.Annotations;
using UnityEngine;

public class TapBehaviour : MonoBehaviour
{
    [SerializeField] GlassBehaviour glass;
    [SerializeField] KegBehaviour keg;
    [SerializeField] GameObject handle;

    public bool glassAvailable;
    public bool kegAvailable;
    public bool handleTurned = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (handle.transform.rotation.eulerAngles.x >= 85)
        {
            handleTurned = true;
        }
        else
        {
            handleTurned = false;
        }

        if (keg.isEmpty || keg == null)
        {
            kegAvailable = false;
        }
        else
        {
            kegAvailable = true;
        }

        if (glass == null)
        {
            glassAvailable = false;
        }
        else
        {
            glassAvailable = true;
        }

        if (glassAvailable && kegAvailable && handleTurned)
        {
            glass.Fill();
            keg.Empty();
        }
    }

    public void SelectGlass(GlassBehaviour selectedGlass)
    {
        glass = selectedGlass;
    }

    public void RemoveGlass()
    {
        glass = null;
    }
}
