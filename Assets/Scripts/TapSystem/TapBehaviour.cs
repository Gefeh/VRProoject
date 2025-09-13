using JetBrains.Annotations;
using UnityEngine;

public class TapBehaviour : MonoBehaviour
{
    [SerializeField] GlassBehaviour glass;
    [SerializeField] KegBehaviour keg;

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
}
