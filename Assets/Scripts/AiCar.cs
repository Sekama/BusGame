using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SplineMesh;

public class AiCar : MonoBehaviour
{
    public List<Spline> Splinesss;
    int index = 0;
    private Spline _currentSpline;
    private Rigidbody _rb;
    public float time;
    private bool reverseFlag;
    public float Speed;
    private void Awake()
    {
        reverseFlag = false;
        _rb = GetComponent<Rigidbody>();
        index = 0;
        
    }
    private void Start()
    {
        _currentSpline = Splinesss[index];
        //gameObject.transform.position = _currentSpline.GetProjectionSample(gameObject.transform.position).location;
    }
    private void FixedUpdate()
    {
        //if(!reverseFlag)
        //{
        //    time += 0.005f;
        //}
        //else
        //{
        //    time -= 0.005f;
        //}
        //if(time <= 0.005)
        //{
        //    reverseFlag = false;
        //}
        //if(time >= (float)_currentSpline.nodes.Count * 0.99f)
        //{
        //    reverseFlag = true;
        //}
        Drive();
    }
    public void Test()
    {
       
        //gameObject.transform.position = _currentSpline.GetProjectionSample(gameObject.transform.position).location;
        
        //gameObject.transform.rotation = _currentSpline.GetSample(time).Rotation;
    }

    public void Drive()
    {
        CurveSample ClosestPoint = _currentSpline.GetProjectionSample(gameObject.transform.position);
        Vector3 DirectionVec = ClosestPoint.location - gameObject.transform.position;
        if (DirectionVec.magnitude >= 0.5)
        {
            _rb.velocity = DirectionVec.normalized * Speed;
            gameObject.transform.rotation = Quaternion.LookRotation(DirectionVec);
        }
        else
        {
            _rb.velocity = _currentSpline.GetProjectionSample(gameObject.transform.position).tangent * Speed;
            gameObject.transform.rotation = _currentSpline.GetProjectionSample(gameObject.transform.position).Rotation;
            Debug.Log(Vector3.Distance(_currentSpline.nodes[_currentSpline.nodes.Count - 1].Position, gameObject.transform.position));
            if (Vector3.Distance(_currentSpline.nodes[_currentSpline.nodes.Count - 1].Position, gameObject.transform.position) <= 1f)
            {
                index++;
                index %= Splinesss.Count;
                _currentSpline = Splinesss[index];
                Debug.Log("Hello");
            }

        }
        
    }
}
