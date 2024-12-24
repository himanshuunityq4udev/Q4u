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
        if (!PlayerPrefs.HasKey(SkyboxPrefKey))
        {
            PlayerPrefs.SetInt(SkyboxPrefKey, 0);
        }
    }

    private void OnEnable()
    {
        ActionHelper.SelectSky += ApplySkybox;
        ActionHelper.UnlockSky += UnlockSkybox;
        ActionHelper.ResetSkySelection += ResetSelectedSkyBox;
    }

    private void OnDisable()
    {
        ActionHelper.SelectSky -= ApplySkybox;
        ActionHelper.UnlockSky -= UnlockSkybox;
        ActionHelper.ResetSkySelection -= ResetSelectedSkyBox;



    }

    private void Start()
    {
        ResetSelectedSkyBox();
    }

    // Update is called once per frame
    public void ChangeSkyBoxMaterial(bool next)
    {
       

        if (next)
        {
            // Move to the next material
            if (CurrentMaterial < skyboxData.skyBoxMaterials.Count - 1)
            {
                CurrentMaterial++;
            }
            else
            {
                CurrentMaterial = 0;
            }
        }
        else
        {
            // Move to the previous material
            if (CurrentMaterial > 0)
            {
                CurrentMaterial--;
            }
            else
            {
                CurrentMaterial = skyboxData.skyBoxMaterials.Count - 1;
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
         
            DynamicGI.UpdateEnvironment(); // Ensure lighting is updated
            // Save the selected skybox index for persistence
            PlayerPrefs.SetInt(SkyboxPrefKey, CurrentMaterial);
            PlayerPrefs.Save();
        }
    }

    public void ResetSelectedSkyBox()
    {
        // Get the selected skybox index for persistence
        CurrentMaterial = PlayerPrefs.GetInt(SkyboxPrefKey);
        // Apply the selected skybox material(runtime)
        RenderSettings.skybox = skyboxData.skyBoxMaterials[CurrentMaterial];
        DynamicGI.UpdateEnvironment(); // Ensure lighting is updated

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
            ActionHelper.DontHaveEnoughCoins?.Invoke();
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