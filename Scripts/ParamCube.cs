using UnityEngine;

public class ParamCube : MonoBehaviour
{
    public int _band;
    public float _startScale, _scaleMultiplier;
    public bool _useBuffer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_useBuffer)
        {
            transform.localScale = new Vector3(transform.localScale.x, (AudioPeer._bandBuffer[_band] * _scaleMultiplier) + _startScale, transform.localScale.z);
        }

        if (!_useBuffer)
        {
            transform.localScale = new Vector3(transform.localScale.x, (AudioPeer._freqBand[_band] * _scaleMultiplier) + _startScale, transform.localScale.z);
        }

    }
}
