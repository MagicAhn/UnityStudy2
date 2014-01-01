using System;
using System.ComponentModel;
using System.Linq;
using UnityEngine;
using System.Collections;

public class Hit : MonoBehaviour
{
    public Texture2D BackgroundColor;
    public Texture2D ForegroundColor;

    private Single _keyDownTime;
    private Single _timeSpan;

    // Use this for initialization
    void Start()
    {
        GameObject box = GameObject.Find("Box");

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                GameObject boxObj = (GameObject)Instantiate(box);
                boxObj.transform.position = new Vector3(j - 2, 0.5f + i, 4);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                // 自游戏运行开始后的 时间（这个游戏 已经开始 运行了多长时间）
                _keyDownTime = Time.realtimeSinceStartup;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.name.Equals("Box(Clone)"))
                {
                    GameObject bullet = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    bullet.AddComponent<Rigidbody>();
                    bullet.transform.position = Camera.main.transform.position;
                    Vector3 v3 = hit.point - Camera.main.transform.position;

                    bullet.rigidbody.AddForce(v3 * (_timeSpan%5), ForceMode.Impulse);
                    bullet.AddComponent("DestoryAll");

                    // 释放鼠标左键后，将 相应时间清零 保证 其不会再在 GUI上 绘制 蓄力状态
                    _keyDownTime = 0.0f;
                    _timeSpan = 0.0f;
                }
            }
        }
    }

    void OnGUI()
    {
        GUI.DrawTexture(new Rect(10, 10, 100, 20), BackgroundColor);

        // 保证 只要 鼠标左键按下去后 就开始 蓄力
        if (_keyDownTime > 0.0f)
        {
            // 保证 鼠标左键按下后 每一帧 都会有蓄力的效果
            _timeSpan = Time.realtimeSinceStartup - _keyDownTime;

            //if (_timeSpan > 5.0f)
            //{
            //    _timeSpan = 5.0f;
            //}
            GUI.DrawTexture(new Rect(10, 10, (_timeSpan%5 * 20), 20), ForegroundColor);
        }
    }
}
