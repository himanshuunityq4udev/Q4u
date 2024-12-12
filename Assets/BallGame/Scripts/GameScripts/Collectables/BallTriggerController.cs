using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Unity.Cinemachine;

public class BallTriggerController : MonoBehaviour
{
    
    Rigidbody _rb;

    private Vector3 _rebornPoint;

  //  private bool _isDeath = false;

  //  private int _ballType = 0;

    [SerializeField]
  //  private Material[] _ballMaterials;


    private Animator _anim;

    public void SetBallType(int type)
    {
       // transform.GetChild(0).GetComponent<Renderer>().material = _ballMaterials[type];

        //if (_ballType != type)
        //{
        //    if (type == 0)
        //    {
        //        _rb.mass = 4.0f;
        //        _rb.drag = 1.0f;
        //        _rb.angularDrag = 0.05f;
        //    }
        //    if (type == 1)
        //    {
        //        _rb.mass = 2.0f;
        //        _rb.drag = 0.1f;
        //        _rb.angularDrag = 0.05f;
        //    }
        //    if (type == 2)
        //    {
        //        _rb.mass = 8.0f;
        //        _rb.drag = 1.5f;
        //        _rb.drag = 0.1f;
        //    }
        //}
    }

    private void Start()
    {
        
        _rb = GetComponent<Rigidbody>();
    }


    public void SetRebornPoint(Vector3 res)
    {
        _rebornPoint = res;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Boxes") {
            foreach (Transform child in collision.transform.parent) {
               // child.GetComponent<Rigidbody>().AddExplosionForce(100, collision.contacts[0].point,5,3);
            }
        
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
          
            Debug.Log("Complete");
            transform.rotation = Quaternion.Euler(0, 0, 0);
            _rb.angularVelocity = Vector3.zero;
            _rb.linearVelocity = Vector3.zero;
        }
    }



    //public void RebornBall()
    //{
    //    this.transform.position = _rebornPoint;
    //    _rb.linearVelocity = Vector3.zero;
    //    _rb.constraints = RigidbodyConstraints.FreezeAll;
    //    _rb.constraints = RigidbodyConstraints.None;
    //}

    //void Start()
    //{
    //    _rb = GetComponent<Rigidbody>();

    //    _rebornPoint = _rb.position;
        
    //}

    void Update()
    {

        //if (_rb.position.y < 10.0f && !_isDeath)
        //{
        //    int hp = int.Parse(_hpText.text.Split(":")[1]);
        //    hp--;
        //    if (hp > 0)
        //    {

        //        GameObject.Find("Util").GetComponent<DeahMenu>().ShowDeahMenu();

        //    }
        //    else {
        //        GameObject.Find("Util").GetComponent<DeahMenu>().ShowGameOverMenu();
        //    }
        //    _hpText.text = "Hp:" + hp.ToString();
        //    _isDeath = true;
        //}
        //else if (_rb.position.y >= 10.0f)
        //{
        //    _isDeath = false;
        //}
    }
}
