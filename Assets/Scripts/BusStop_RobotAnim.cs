using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusStop_RobotAnim : MonoBehaviour
{
    [SerializeField] private Animator[] robotArray;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            foreach (var robot in robotArray)
            {
                robot.SetBool("isActive", true);
            }
            Debug.Log("robot active");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            foreach (var robot in robotArray)
            {
                robot.SetBool("isActive", false);
            }
            Debug.Log("robot de_active");

        }    
    }
}
