using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpTrap : MonoBehaviour
{
    [SerializeField]
    private float _addForce = 50f;
    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<Rigidbody>().AddForce(Vector3.up * _addForce,ForceMode.Impulse);
    }
}
