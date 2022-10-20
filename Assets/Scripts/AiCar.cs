using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SplineMesh;

public class AiCar : MonoBehaviour
{
    private RoadScript _currentRoad;
    [SerializeField]private Spline _currentSpline;
    private Rigidbody _rb;
    public float time;
    private bool reverseFlag;
    public float Speed;
    private void Awake()
    {
        reverseFlag = false;
        _rb = GetComponent<Rigidbody>();
        
        
    }
    private void Start()
    {
        //CurveSample ClosestPoint = _currentSpline.GetProjectionSample(gameObject.transform.position);
        //Debug.Log(ClosestPoint.distanceInCurve);
        float distStart = Vector3.Distance(_currentSpline.nodes[0].Position, gameObject.transform.position);
        float distEnd = Vector3.Distance(_currentSpline.nodes[_currentSpline.nodes.Count - 1].Position, gameObject.transform.position);
        gameObject.transform.position = distStart <= distEnd ? _currentSpline.nodes[0].Position : _currentSpline.nodes[_currentSpline.nodes.Count - 1].Position;
    }
    private void FixedUpdate()
    {
        //Drive();
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
                
            }

        }
        
    }
}
