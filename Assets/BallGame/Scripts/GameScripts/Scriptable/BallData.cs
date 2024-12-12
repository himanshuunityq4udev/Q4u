using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "BallData", menuName = "Ball")]
public class BallData : ScriptableObject
{
    public List<GameObject> balls = new List<GameObject>();
    public List<bool> unlockedBalls = new List<bool>();
    public List<int> ballPrice = new List<int>();
}