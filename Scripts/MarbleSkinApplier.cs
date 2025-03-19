using UnityEngine;
using System.Collections.Generic;

public class MarbleSkinApplier : MonoBehaviour
{
    [Header("Available Skins")]
    public List<Material> availableSkins; // Assign same materials as in Main Menu
    private Dictionary<string, Material> skinDictionary = new Dictionary<string, Material>();

    private Renderer marbleRenderer;

    void Start()
    {
        marbleRenderer = GetComponent<Renderer>();

        // Populate skin dictionary
        foreach (Material skin in availableSkins)
        {
            if (skin != null)
            {
                skinDictionary[skin.name] = skin;
            }
        }

        // Load the selected skin from PlayerPrefs
        string selectedSkin = PlayerPrefs.GetString("SelectedSkin", "");
        if (skinDictionary.ContainsKey(selectedSkin))
        {
            ApplySkin(selectedSkin);
        }
        else if (availableSkins.Count > 0)
        {
            ApplySkin(availableSkins[0].name); // Default to first skin if none selected
        }
    }

    void ApplySkin(string skinName)
    {
        if (skinDictionary.ContainsKey(skinName))
        {
            marbleRenderer.material = skinDictionary[skinName];
            Debug.Log("Applied Marble Skin: " + skinName);
        }
    }
}
