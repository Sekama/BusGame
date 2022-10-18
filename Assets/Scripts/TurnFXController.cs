using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnFXController : MonoBehaviour
{
    [SerializeField] private GameObject[] turnFX_Left;
    [SerializeField] private GameObject[] turnFX_Right;

    private void Start()
    {

    }

    private void Update()
    {
        Debug.Log("scale");

        if (Input.anyKeyDown)
        {
            foreach (var turnFX_Left_motor in turnFX_Left)
            {        
                var originalScale = turnFX_Left_motor.gameObject.transform.localScale;
                var newScale = new Vector3(originalScale.x, 0.4f, originalScale.z );

                turnFX_Left_motor.GetComponent<Transform>().localScale = newScale;
            }
        }

    }
}
