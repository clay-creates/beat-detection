using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class MusicManager : MonoBehaviour
{
    [Header("Assign Songs")]
    public List<AudioClip> songChoices;
    public TMP_Dropdown songDropdown;
    public Button playSampleButton;
    public AudioSource audioSource;

    private Dictionary<string, AudioClip> songDictionary = new Dictionary<string, AudioClip>();

    void Start()
    {
        PopulateDropdown();
        songDropdown.onValueChanged.AddListener(delegate { SaveMusicChoice(); });
        playSampleButton.onClick.AddListener(PlaySample);

        LoadSavedChoice();
    }

    void PopulateDropdown()
    {
        songDropdown.ClearOptions();
        foreach (AudioClip clip in songChoices)
        {
            if (clip != null)
            {
                songDictionary[clip.name] = clip;
                songDropdown.options.Add(new TMP_Dropdown.OptionData(clip.name));
            }
        }
        songDropdown.RefreshShownValue();
    }

    void PlaySample()
    {
        string selectedSong = songDropdown.options[songDropdown.value].text;
        if (songDictionary.ContainsKey(selectedSong))
        {
            audioSource.clip = songDictionary[selectedSong];
            audioSource.Play();
        }
    }

    void SaveMusicChoice()
    {
        string selectedSong = songDropdown.options[songDropdown.value].text;
        PlayerPrefs.SetString("SelectedSong", selectedSong);
        PlayerPrefs.Save();
    }

    void LoadSavedChoice()
    {
        if (PlayerPrefs.HasKey("SelectedSong"))
        {
            string savedSong = PlayerPrefs.GetString("SelectedSong");
            int savedIndex = songDropdown.options.FindIndex(option => option.text == savedSong);
            if (savedIndex >= 0)
            {
                songDropdown.value = savedIndex;
            }
        }
    }
}
