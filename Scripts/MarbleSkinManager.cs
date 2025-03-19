using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class MarbleSkinManager : MonoBehaviour
{
    [Header("Assign Marble Skins")]
    public List<Material> availableSkins; // Assign marble materials in Inspector
    public TMP_Dropdown skinDropdown; // Assign the dropdown in Inspector
    public Renderer marblePreviewRenderer; // Assign the preview marble's Renderer

    private Dictionary<string, Material> skinDictionary = new Dictionary<string, Material>();

    void Start()
    {
        PopulateDropdown();
        skinDropdown.onValueChanged.AddListener(delegate { SaveSkinChoice(); });

        LoadSavedChoice();
    }

    void PopulateDropdown()
    {
        skinDropdown.ClearOptions();

        foreach (Material skin in availableSkins)
        {
            if (skin != null)
            {
                skinDictionary.Add(skin.name, skin);
                skinDropdown.options.Add(new TMP_Dropdown.OptionData(skin.name));
            }
        }

        skinDropdown.RefreshShownValue();
    }

    void SaveSkinChoice()
    {
        string selectedSkin = skinDropdown.options[skinDropdown.value].text;
        PlayerPrefs.SetString("SelectedSkin", selectedSkin);
        PlayerPrefs.Save();

        ApplyPreviewSkin(selectedSkin);
    }

    void ApplyPreviewSkin(string skinName)
    {
        if (skinDictionary.ContainsKey(skinName))
        {
            marblePreviewRenderer.material = skinDictionary[skinName];
        }
    }

    void LoadSavedChoice()
    {
        if (PlayerPrefs.HasKey("SelectedSkin"))
        {
            string savedSkin = PlayerPrefs.GetString("SelectedSkin");
            int savedIndex = skinDropdown.options.FindIndex(option => option.text == savedSkin);
            if (savedIndex >= 0)
            {
                skinDropdown.value = savedIndex;
                ApplyPreviewSkin(savedSkin);
            }
        }
    }
}
