using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DistenceToCheckPoint : MonoBehaviour
{
    [SerializeField] private Transform checkpoint; // The checkpoint's transform
   // [SerializeField] private TMP_Text distanceText; // Text to show distance
    [SerializeField] private Slider distanceSlider; // Slider to represent distance
   // [SerializeField] private float maxDistance = 100f; // Max distance for normalization

    private float initialDistance;


    private void Start()
    {
        // Calculate the initial distance
        initialDistance = (checkpoint.transform.position - transform.position).magnitude;

        distanceSlider.value = 1; // Full at the start
    }
    private void Update()
    {
        // Calculate the distance
        float currentDistance = (checkpoint.transform.position - transform.position).magnitude;

        // Update the slider value (normalized based on initial distance)
        distanceSlider.value = Mathf.Clamp01(1 - (currentDistance / initialDistance));

        // Update the distance text
       // distanceText.text = "Distance: " + currentDistance.ToString("f1") + " meters";
    }
}