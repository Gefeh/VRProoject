using UnityEngine;

public class KegBehaviour : MonoBehaviour
{
    public int maxVolume;
    public int currentVolume;
    public bool isEmpty = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentVolume = maxVolume;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentVolume == 0)
        {
            isEmpty = true;
        }
    }

    public void Empty()
    {
        if(currentVolume > 0)
        {
            currentVolume--;
        }
    }
}
