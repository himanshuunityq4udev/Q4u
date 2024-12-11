using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerController : MonoBehaviour
{
    //´¸×ÓµÄ½Å±¾
    [SerializeField]
    private Vector3 _direction = new Vector3(0,0,0);
    [SerializeField]
    private float _speed = 30;
    [SerializeField]
    private float _time = 3;

    private float _timer = 0;

    private void FixedUpdate()
    {
        if (_timer < _time)
        {
            _timer += Time.deltaTime;
        }
        else {
            _direction *= -1;
            _timer = 0;
        }
        transform.Rotate(_direction,_speed * Time.deltaTime);
    }

}
