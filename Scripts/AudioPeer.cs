using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(AudioSource))]
public class AudioPeer : MonoBehaviour
{
    private AudioSource _audioSource;

    public static float[] _samplesLeft = new float[512];
    public static float[] _samplesRight = new float[512];

    public static float[] _freqBand = new float[8];
    public static float[] _bandBuffer = new float[8];
    private float[] _bufferDecrease = new float[8];

    private float[] _freqBandHighest = new float[8];
    public static float[] _audioBand = new float[8];
    public static float[] _audioBandBuffer = new float[8];

    public static float _Amplitude, _AmplitudeBuffer;
    private float _AmplitudeHighest;

    public float _audioProfile;

    public enum _channel { Stereo, Left, Right };
    public _channel channel = new _channel();

    [Header("Available Songs")]
    public List<AudioClip> availableSongs; // Assign all available songs in Inspector
    private Dictionary<string, AudioClip> songDictionary = new Dictionary<string, AudioClip>();

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();

        // Populate dictionary with song names
        foreach (AudioClip clip in availableSongs)
        {
            if (clip != null)
            {
                songDictionary[clip.name] = clip;
            }
        }

        // Load the selected song from PlayerPrefs
        string selectedSong = PlayerPrefs.GetString("SelectedSong", "");
        if (songDictionary.ContainsKey(selectedSong))
        {
            _audioSource.clip = songDictionary[selectedSong];
        }
        else if (availableSongs.Count > 0)
        {
            _audioSource.clip = availableSongs[0]; // Default to first song if none selected
        }

        // Play the song if available
        if (_audioSource.clip != null)
        {
            _audioSource.loop = true; // Ensure the song loops
            _audioSource.Play();
            Debug.Log("Now Playing: " + _audioSource.clip.name);
        }
        else
        {
            Debug.LogWarning("AudioPeer: No valid AudioClip found! Ensure songs are assigned.");
        }

        AudioProfile(_audioProfile);
    }

    void Update()
    {
        if (_audioSource.isPlaying)
        {
            GetSpectrumAudioSource();
            MakeFrequencyBands();
            BandBuffer();
            CreateAudioBands();
            GetAmplitude();
        }
    }

    void GetSpectrumAudioSource()
    {
        _audioSource.GetSpectrumData(_samplesLeft, 0, FFTWindow.Blackman);
        _audioSource.GetSpectrumData(_samplesRight, 1, FFTWindow.Blackman);
    }

    void AudioProfile(float audioProfile)
    {
        for (int i = 0; i < 8; i++)
        {
            _freqBandHighest[i] = audioProfile;
        }
    }

    void GetAmplitude()
    {
        float _CurrentAmplitude = 0;
        float _CurrentAmplitudeBuffer = 0;

        for (int i = 0; i < 8; i++)
        {
            _CurrentAmplitude += _audioBand[i];
            _CurrentAmplitudeBuffer += _audioBandBuffer[i];
        }

        if (_CurrentAmplitude > _AmplitudeHighest)
        {
            _AmplitudeHighest = _CurrentAmplitude;
        }

        _Amplitude = _CurrentAmplitude / _AmplitudeHighest;
        _AmplitudeBuffer = _CurrentAmplitudeBuffer / _AmplitudeHighest;
    }

    void CreateAudioBands()
    {
        for (int i = 0; i < 8; i++)
        {
            if (_freqBand[i] > _freqBandHighest[i])
            {
                _freqBandHighest[i] = _freqBand[i];
            }
            _audioBand[i] = (_freqBand[i] / _freqBandHighest[i]);
            _audioBandBuffer[i] = (_bandBuffer[i] / _freqBandHighest[i]);
        }
    }

    void BandBuffer()
    {
        for (int g = 0; g < 8; g++)
        {
            if (_freqBand[g] > _bandBuffer[g])
            {
                _bandBuffer[g] = _freqBand[g];
                _bufferDecrease[g] = 0.005f;
            }

            if (_freqBand[g] < _bandBuffer[g])
            {
                _bandBuffer[g] -= _bufferDecrease[g];
                _bufferDecrease[g] *= 1.2f;
            }
        }
    }

    void MakeFrequencyBands()
    {
        int count = 0;

        for (int i = 0; i < 8; i++)
        {
            float average = 0;
            int sampleCount = (int)Mathf.Pow(2, i) * 2;

            if (i == 7)
            {
                sampleCount += 2;
            }

            for (int j = 0; j < sampleCount; j++)
            {
                if (channel == _channel.Stereo)
                {
                    average += _samplesLeft[count] + _samplesRight[count] * (count + 1);
                }
                if (channel == _channel.Left)
                {
                    average += _samplesLeft[count] * (count + 1);
                }
                if (channel == _channel.Right)
                {
                    average += _samplesRight[count] * (count + 1);
                }

                count++;
            }

            average /= count;

            _freqBand[i] = average * 10;
        }
    }
}
