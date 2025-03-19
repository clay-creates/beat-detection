using UnityEngine;

[RequireComponent (typeof (Light))]
public class LightOnAudio : MonoBehaviour
{
    public int _band;
    public float _minIntensity, _maxIntensity;
    private Light _light;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _light = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        _light.intensity = (AudioPeer._audioBandBuffer[_band] * (_maxIntensity - _minIntensity)) + _minIntensity;
    }
}
