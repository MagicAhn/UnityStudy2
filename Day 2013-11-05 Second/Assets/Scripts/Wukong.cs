using System;
using UnityEngine;
using System.Collections;

public class Wukong : MonoBehaviour
{
    private GameObject _wukong;
    private String _status;
    // Use this for initialization
    void Start()
    {
        _wukong = GameObject.Find("wukong");
        _status = "Idle";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            Move(Vector3.forward);
        }
        else
        {
            _status = "Idle";
        }
        if (Input.GetKey(KeyCode.A))
        {
            this.gameObject.transform.Rotate(Vector3.up, -1);
        }
        if (Input.GetKey(KeyCode.S))
        {
            this.gameObject.transform.Rotate(Vector3.up, 180);
        }
        if (Input.GetKey(KeyCode.D))
        {
            this.gameObject.transform.Rotate(Vector3.up, 1);
        }
        if (Input.GetKey(KeyCode.Space))
        {
            this.gameObject.rigidbody.AddForce(Vector3.up , ForceMode.Impulse);
        }

        _wukong.animation.Play(_status);
    }

    void Move(Vector3 v3)
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            this.gameObject.transform.Translate(v3 * 1f);
            _status = "Run";
        }
        else
        {
            this.gameObject.transform.Translate(v3 * 0.3f);
            _status = "Walk";
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name.Equals("JapaneseMaple"))
        {
            Debug.Log(1);
        }
    }

    void OnCollisionStay(Collision col)
    {
        if (col.gameObject.name.Equals("JapaneseMaple"))
        {
            Debug.Log(2);
        }
        else
        {
            Debug.Log(3);
        }
    }

    void OnCollisionExit(Collision col)
    {
        if (col.gameObject.name.Equals("JapaneseMaple"))
        {
            Debug.Log(4);
        }
    }
}
