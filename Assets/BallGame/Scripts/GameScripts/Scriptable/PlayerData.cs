using UnityEngine;
[CreateAssetMenu(fileName = "PlayerData", menuName = "Player")]

public class PlayerData : ScriptableObject
{

    public float swipeDistanceThreshold = 10;  // Minimum distance for a swipe in units
    public float swipeTimeThreshold = 0.1f;
    public float m_MaxAngularVelocity = 45;

    public int life = 4;
    public int totalLife = 4;
    public Vector3 respawnPosition = new Vector3(0, 1, 0);
   // public Vector3 direction = Vector3.forward;
    public int revivalAmount = 200;
}
