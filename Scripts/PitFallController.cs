using UnityEngine;

public class PitfallController : MonoBehaviour
{
    public GameObject[] pitfalls; // Assign the pitfall objects in the Inspector
    public int frequencyBand = 2; // Select which band controls the pitfall (0-7)
    public float activationThreshold = 0.5f; // Sensitivity to the beat

    private bool isPitfallActive = false;

    void Update()
    {
        if (AudioPeer._audioBandBuffer[frequencyBand] > activationThreshold)
        {
            if (!isPitfallActive)
            {
                TogglePitfall(true);
                isPitfallActive = true;
            }
        }
        else
        {
            if (isPitfallActive)
            {
                TogglePitfall(false);
                isPitfallActive = false;
            }
        }
    }

    void TogglePitfall(bool isActive)
    {
        foreach (GameObject pit in pitfalls)
        {
            pit.SetActive(!isActive); // If active, deactivate; if inactive, activate
        }
    }
}
