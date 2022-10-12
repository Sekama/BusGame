using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayFinder : MonoBehaviour
{
    public GameObject[] busStop;
    public GameObject arrow3d;
    public GameObject arrow3d1;
    public GameObject arrow3d2;
    private int i = 0;


    void Update()
    {
        transform.LookAt(busStop[i].transform.position);
        Debug.Log(i.ToString());
    }

    private void OnTriggerEnter(Collider other)
    {
        arrow3d.gameObject.GetComponent<MeshRenderer>().enabled = false;
        arrow3d1.gameObject.GetComponent<MeshRenderer>().enabled = false;
        arrow3d2.gameObject.GetComponent<MeshRenderer>().enabled = false;
        
        if (other.gameObject.CompareTag("BusStop"))
        {
            if (i < busStop.Length - 1)
            {
                i++;
            }
            else
            {
                i = 0;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("BusStop"))
        {
            arrow3d.gameObject.GetComponent<MeshRenderer>().enabled = true;
            arrow3d1.gameObject.GetComponent<MeshRenderer>().enabled = true;
            arrow3d2.gameObject.GetComponent<MeshRenderer>().enabled = true;
        }
    }
}

