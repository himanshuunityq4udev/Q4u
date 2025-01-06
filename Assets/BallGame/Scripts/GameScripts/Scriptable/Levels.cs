using UnityEngine;
using System.Collections.Generic;


[CreateAssetMenu(fileName = "LevelData", menuName = "Level")]
public class Levels : ScriptableObject
{
    public List<GameObject> gameLevels = new List<GameObject>();
}