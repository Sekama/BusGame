using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalArrow : MonoBehaviour
{

    public GameObject directionalArrow;
    
    public GameObject FindClosestBusStop()
    {
        GameObject[] stops;
        stops = GameObject.FindGameObjectsWithTag("BusStop");
        GameObject closestStop = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;

        foreach (GameObject stop in stops)
        {
            Vector3 diff = stop.transform.position - position;
            float currentDistance = diff.sqrMagnitude;
            if (currentDistance < distance)
            {
                closestStop = stop;
                distance = currentDistance;
            }
        }
        return closestStop;
    }

    private void Update()
    {
        directionalArrow.transform.LookAt(FindClosestBusStop().transform);
    }
}
