using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SplineMesh;


public class RoadScript : MonoBehaviour
{
    [SerializeField] public GameObject SplinePreFab;
    public Spline ForwardLane;
    public Spline BackwardLane;
    [SerializeField] private List<RoadScript> StartNeighbours;
    [SerializeField] private List<RoadScript> EndNeighbours;
    [SerializeField] private SphereCollider _startCollider;
    [SerializeField] private SphereCollider _endCollider;
    

    private void Awake()
    {
        //ForwardLane
        GameObject Temp = Instantiate(SplinePreFab, gameObject.transform);
        var TempSpline = Temp.GetComponent<Spline>();
        var SelfSpline = GetComponent<Spline>();
        TempSpline.IsLoop = SelfSpline.IsLoop;
        for (int j = 0; j < SelfSpline.nodes.Count; ++j)
        {
            if (j <= 1)
            {
                TempSpline.nodes[j] = SelfSpline.nodes[j];
            }
            else
            {
                TempSpline.AddNode(SelfSpline.nodes[j]);
            }
        }
        Temp.transform.localPosition = new Vector3(0, 0, 2.5f);
        ForwardLane = TempSpline;
        TempSpline.RefreshCurves();
        //BackwardLane
        Temp = Instantiate(SplinePreFab, gameObject.transform);
        TempSpline = Temp.GetComponent<Spline>();
        TempSpline.IsLoop = SelfSpline.IsLoop;
        for (int j = 0; j < SelfSpline.nodes.Count; ++j)
        {
            if (j <= 1)
            {
                TempSpline.nodes[j] = SelfSpline.nodes[j];
            }
            else
            {
                TempSpline.AddNode(SelfSpline.nodes[j]);
            }
        }
        Temp.transform.localPosition = new Vector3(0, 0, -2.5f);
        BackwardLane = TempSpline;
        TempSpline.RefreshCurves();
        _startCollider.center = SelfSpline.nodes[0].Position;
        _endCollider.center = SelfSpline.nodes[1].Position;
        StartNeighbours = new List<RoadScript>();
        EndNeighbours = new List<RoadScript>();
    }
    private void Start()
    {
        
        Collider[] hit = Physics.OverlapSphere(GetComponent<Spline>().nodes[0].Position,15f);
        foreach(var obj in hit)
        {
            if(obj.gameObject.tag == "RoadSplines" && obj.gameObject != _startCollider.gameObject && obj.gameObject != _endCollider.gameObject)
            {
                StartNeighbours.Add(obj.gameObject.transform.parent.gameObject.GetComponent<RoadScript>());
            }
            
        }
        hit = Physics.OverlapSphere(GetComponent<Spline>().nodes[GetComponent<Spline>().nodes.Count - 1].Position, 15f);
        foreach (var obj in hit)
        {
            if (obj.gameObject.tag == "RoadSplines" && obj.gameObject != _startCollider.gameObject && obj.gameObject != _endCollider.gameObject)
            {
                EndNeighbours.Add(obj.gameObject.transform.parent.gameObject.GetComponent<RoadScript>());
            }

        }

    }
}
