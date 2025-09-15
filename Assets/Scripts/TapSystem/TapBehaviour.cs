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
        Debug.Log(handle.transform.rotation.eulerAngles.x);
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
}
