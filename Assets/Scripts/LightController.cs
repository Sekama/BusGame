using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    [SerializeField] private Animator []  busLight;  

    void Update()
    {
        foreach (var busLightBulb in busLight)
        {
            //Braking
            if (Input.GetKeyDown(KeyCode.Space))
            {
                busLightBulb.SetBool("isBraking", true);
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                busLightBulb.SetBool("isBraking", false);
            }
        }
    }
}
