using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "SkyboxData",menuName = "Skybox")]
public class SkyboxData : ScriptableObject
{
    public List<Material> skyBoxMaterials = new List<Material>();
    public List<bool> unlockedSkybox = new List<bool>();
    public List<int> skyboxPrice = new List<int>();
}
