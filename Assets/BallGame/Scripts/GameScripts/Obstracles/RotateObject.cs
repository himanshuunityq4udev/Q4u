using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour {

    public bool RandomRotation;
    public Vector3 _Axis;
    public float speed = 10.0f;
    
	void FixedUpdate ()
    {
        if (RandomRotation == true)
        {
            transform.Rotate(0, 100 * speed * Time.deltaTime, 0);
            transform.Rotate(40 * speed * Time.deltaTime, 0, 0);
        }
        else
        {
            transform.Rotate(_Axis*speed * Time.deltaTime);
        }
    }
}
