using UnityEngine;

public class GameSettingsLoader : MonoBehaviour
{
    public AudioSource gameAudioSource;
    public Renderer playerMarbleRenderer;

    private void Start()
    {
        ApplySavedMusic();
        ApplySavedSkin();
    }

    void ApplySavedMusic()
    {
        if (PlayerPrefs.HasKey("SelectedSong"))
        {
            string songName = PlayerPrefs.GetString("SelectedSong");
            AudioClip clip = Resources.Load<AudioClip>("Music/" + songName);
            if (clip != null)
            {
                gameAudioSource.clip = clip;
                gameAudioSource.Play();
            }
        }
    }

    void ApplySavedSkin()
    {
        if (PlayerPrefs.HasKey("SelectedSkin"))
        {
            string skinName = PlayerPrefs.GetString("SelectedSkin");
            Material mat = Resources.Load<Material>("Skins/" + skinName);
            if (mat != null)
            {
                playerMarbleRenderer.material = mat;
            }
        }
    }
}
