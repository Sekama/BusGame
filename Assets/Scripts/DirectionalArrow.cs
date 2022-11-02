using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalArrow : MonoBehaviour
{
    public delegate void FChangeStation();
    public FChangeStation ChangeActiveStation;
    public GameObject DirectionalArrowComponent;
    public GameObject Destination;

    //Supreme Leader I am Very Sorry For Deleting your Function. It Had to be done for the benefit of your empire. 
    private void Awake()
    {
        GetComponent<Player_Controller>().AtStation += ChangeStation;
    }

    public void ChangeStation(bool bIsArriving)
    {
        if (!bIsArriving)
        {
            ChangeActiveStation();
        }
    }
    private void FixedUpdate()
    {
        DirectionalArrowComponent.transform.LookAt(Destination.transform);
    }
}
