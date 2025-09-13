using UnityEngine;

public class GlassBehaviour : MonoBehaviour
{
    public int currentVolume = 0;
    public int maxVolume;
    public bool isOverflowing = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentVolume > maxVolume)
        {
            isOverflowing = true;
        }

        if (isOverflowing)
        {
            //some sort of consequence for tapping badly
        }
    }

    public void Fill()
    {
        currentVolume++;
    }
}
