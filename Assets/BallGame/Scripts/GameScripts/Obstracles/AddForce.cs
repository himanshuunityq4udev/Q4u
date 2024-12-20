using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AddForce : MonoBehaviour
{
    [SerializeField]
    private float _addForce = 200;
    [SerializeField]
    private bool IsRightAddSpeed = false;
    [SerializeField]
    private bool IsNegativeDirection = false;
    private Vector3 _direction;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the other object has a Rigidbody
        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogWarning($"Object {other.name} does not have a Rigidbody.");
            return;
        }

        // Determine force direction
        if (IsRightAddSpeed)
        {
            _direction = IsNegativeDirection ? Vector3.left * _addForce : Vector3.right * _addForce;
        }
        else
        {
            _direction = IsNegativeDirection ? Vector3.back * _addForce : Vector3.forward * _addForce;
        }

        // Apply force
        rb.AddForce(_direction, ForceMode.Impulse);
    }
}
