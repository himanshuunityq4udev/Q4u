using UnityEngine;
using System.Collections.Generic;
using TMPro;
public class SkyboxChanger : MonoBehaviour
{
    [SerializeField] private GameObject selectButton;
    [SerializeField] private GameObject unlockButton;
    [SerializeField] private TMP_Text skyboxPriceText;

    [SerializeField] SkyboxData skyboxData;


    // PlayerPrefs key for storing the selected skybox index
    private const string SkyboxPrefKey = "SelectedSkybox";

    private int CurrentMaterial = 0;

   

    private Material previousSkybox;

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("SkyboxPrefKey"))
        {
            PlayerPrefs.SetInt("SkyboxPrefKey", 0);
        }
    }


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
            if (CurrentMaterial < skyboxData.skyBoxMaterials.Count - 1)
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
                CurrentMaterial = skyboxData.skyBoxMaterials.Count - 1;
                Debug.Log("Previous: Reset to " + CurrentMaterial);
            }
        }

        // Apply the selected skybox material(runtime)
        RenderSettings.skybox = skyboxData.skyBoxMaterials[CurrentMaterial];
        UpdateUI();
    }

    // Function to apply the skybox
    public void ApplySkybox()
    {
        if (CurrentMaterial >= 0 && CurrentMaterial < skyboxData.skyBoxMaterials.Count)
        {
            RenderSettings.skybox = skyboxData.skyBoxMaterials[CurrentMaterial];
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
        if (index >= 0 && index < skyboxData.skyBoxMaterials.Count)
        {
            // Set the new current material index
            CurrentMaterial = index;
            ApplySkybox();
        }
    }

    public void ResetSelectedSkyBox()
    {
        // Apply the selected skybox material(runtime)
        RenderSettings.skybox = previousSkybox;

    }


    private void UpdateUI()
    {
        if (skyboxData.unlockedSkybox[CurrentMaterial] == true)
        {
            selectButton.gameObject.SetActive(true);
            unlockButton.gameObject.SetActive(false);
        }
        else // If not, show the buy button and set the price
        {
            selectButton.gameObject.SetActive(false);
            unlockButton.gameObject.SetActive(true);
            skyboxPriceText.text = skyboxData.skyboxPrice[CurrentMaterial-1].ToString();
        }
    }


    public void UnlockSkybox()
    {
        if(MoneyManager.Instance.playerInfo.money >= skyboxData.skyboxPrice[CurrentMaterial - 1])
        {
            selectButton.gameObject.SetActive(true);
            unlockButton.gameObject.SetActive(false);
            skyboxData.unlockedSkybox[CurrentMaterial] = true;
            MoneyManager.Instance.SpendMoney(skyboxData.skyboxPrice[CurrentMaterial - 1]);
        }
        else
        {
            Debug.Log("dont have enough coins");
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