using NUnit.Framework.Interfaces;
using UnityEngine;

public class GlassBehaviour : MonoBehaviour
{
    public int currentVolume = 0;
    public int maxVolume;
    public bool isOverflowing = false;

    public Material material;

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

        ChangeColor();
    }

    public void Fill()
    {
        currentVolume++;
    }

    public void ChangeColor()
    {
        if (isOverflowing)
        {
            material.color = Color.red;
        } else
        {
            if (currentVolume == 0)
            {
                material.color = Color.white;
            }
            if (currentVolume > 0 && currentVolume < 300)
            {
                material.color = Color.yellow;
            }
            if (currentVolume > 300)
            {
                material.color = Color.green;
            }
        }
    }
}
