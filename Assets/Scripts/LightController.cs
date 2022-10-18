using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{

    [SerializeField] private GameObject[] busLight;

    void Update()
    {

        // on braking pressed
        // if (Input.anyKeyDown)  
        {
            Debug.Log("color");
            
            foreach (var busLightColors in busLight)
            {        
                busLightColors.GetComponent<MeshRenderer>().material.EnableKeyword("_EMISSION");
                busLightColors.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", Color.red);;
            }
        }
    }
}
