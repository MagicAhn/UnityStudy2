using System;
using UnityEngine;
using System.Collections;

public class CharacterControllerWuKong : MonoBehaviour
{
    private GameObject _wukong;
    private CharacterController _characterController;
    private Single _speed;
    private Vector3 _targetPosition;
    private Single _lastClick;
    private NavMeshAgent _agent;
    // Use this for initialization
    void Start()
    {
        _characterController = this.gameObject.GetComponent<CharacterController>();
        _wukong = GameObject.Find("wukong");
        _speed = 0.5f;
        _targetPosition = this.gameObject.transform.position;
        _lastClick = Time.realtimeSinceStartup;
        _agent = this.gameObject.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            // 通过两个 点击时间差 计算是单击还是双击
            Single nowClick = Time.realtimeSinceStartup;
            if (nowClick - _lastClick >= 0.2f)
            {
                _speed = 0.5f;
            }
            else
            {
                _speed = 1f;
            }
            _lastClick = nowClick;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.name.Equals("Terrain"))
                {
                    // 执行 移动的逻辑
                    // 取的目标位置
                    _targetPosition = hit.point;
                }
            }
        }
        if (_targetPosition != transform.position)
        {
            transform.LookAt(new Vector3(_targetPosition.x, transform.position.y, _targetPosition.z));

            #region 通过 CharacterController 控制的移动
            //// 根据 移动的速度 和 位置的向量差 取得 移动的速度（带方向）
            //Vector3 step = Vector3.ClampMagnitude(_targetPosition - transform.position, _speed);

            //// 角色控制器 移动
            //_characterController.Move(step); 
            #endregion

            //_agent.Move(_targetPosition);
            _agent.SetDestination(_targetPosition);

            // 当 wukong 接近 目标位置的时候 ，，若目标位置与初始位置在同一水平面，在 y 轴方向存在一定偏差，否则这可能x,y,z都存在偏差，当偏差小于的 数值 小于 一定的值后，wukong 就保持Idle
            Single distance = Vector3.Distance(_targetPosition, transform.position);
            if (distance <= 3.5f)
            {
                _speed = 0.0f;
            }

            // wukong 的移动动作
            WukongAnimation(_speed, _wukong);
        }
    }

    void WukongAnimation(Single spped, GameObject gameObj)
    {
        if (_speed == 1f)
        {
            gameObj.animation.Play("Run");
        }
        else if (_speed == 0.5f)
        {
            gameObj.animation.Play("Walk");
        }
        else
        {
            gameObj.animation.Play("Idle");
        }
    }
}
