using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class SkyboxChanger : MonoBehaviour
{
    public List<Material> skyBoxMaterials = new List<Material>();

    // PlayerPrefs key for storing the selected skybox index
    private const string SkyboxPrefKey = "SelectedSkybox";

    private int CurrentMaterial = 0;
    private Material previousSkybox;

    private void Start()
    {
        // Load the saved skybox index and apply it
        int skyboxIndex = PlayerPrefs.GetInt(SkyboxPrefKey, 0); // Default to the first skybox
        CurrentMaterial = skyboxIndex;
        ApplySkybox();
    }

    // Update is called once per frame
    public void ChangeSkyBoxMaterial(bool next)
    {
        // Save the current skybox before changing it
        previousSkybox = RenderSettings.skybox;

        if (next)
        {
            // Move to the next material
            if (CurrentMaterial < skyBoxMaterials.Count - 1)
            {
                CurrentMaterial++;
                Debug.Log("Next: " + CurrentMaterial);
            }
            else
            {
                CurrentMaterial = 0;
                Debug.Log("Next: Reset to 0");
            }
        }
        else
        {
            // Move to the previous material
            if (CurrentMaterial > 0)
            {
                CurrentMaterial--;
                Debug.Log("Previous: " + CurrentMaterial);
            }
            else
            {
                CurrentMaterial = skyBoxMaterials.Count - 1;
                Debug.Log("Previous: Reset to " + CurrentMaterial);
            }
        }

        // Apply the selected skybox material
        ApplySkybox();
    }

    // Function to apply the skybox
    private void ApplySkybox()
    {
        if (CurrentMaterial >= 0 && CurrentMaterial < skyBoxMaterials.Count)
        {
            RenderSettings.skybox = skyBoxMaterials[CurrentMaterial];
            Debug.Log("Previous Skybox: " + (previousSkybox != null ? previousSkybox.name : "None"));
            DynamicGI.UpdateEnvironment(); // Ensure lighting is updated
            Debug.Log("Current Skybox: " + RenderSettings.skybox.name);

            // Save the selected skybox index for persistence
            PlayerPrefs.SetInt(SkyboxPrefKey, CurrentMaterial);
            PlayerPrefs.Save();
        }
    }

    // Function to manually change the skybox based on index
    public void ChangeSkybox(int index)
    {
        if (index >= 0 && index < skyBoxMaterials.Count)
        {
            // Set the new current material index
            CurrentMaterial = index;
            ApplySkybox();
        }
    }
}


//if(CurrentMaterial < skyBoxMaterials.Count-1)
//{
//    CurrentMaterial++;
//    Debug.Log("call" + CurrentMaterial);
//}
//else
//{
//    CurrentMaterial = 0;
//    Debug.Log("Call 0" + CurrentMaterial);
//}
//RenderSettings.skybox = skyBoxMaterials[CurrentMaterial];