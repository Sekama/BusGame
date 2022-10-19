using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnFXController : MonoBehaviour
{
    [SerializeField] private Animator []  animatorLeft;  
    [SerializeField] private Animator []  animatorRight;  
    [SerializeField] private Animator []  animatorMotor;  

 
    private void Update()
    {

        foreach (var animatorMotor in animatorMotor)
        {
            //Braking
            if (Input.GetKeyDown(KeyCode.Space))
            {
                animatorMotor.SetBool("isBraking", true);
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                animatorMotor.SetBool("isBraking", false);
            }
        }
        foreach (var animatorMotorL in animatorLeft)
        {
            //TurningLeft
            if (Input.GetKeyDown(KeyCode.A))
            {
                animatorMotorL.SetBool("isRightPressed", true);
            }
            if (Input.GetKeyUp(KeyCode.A))
            {
                animatorMotorL.SetBool("isRightPressed", false);
            }
            
            //TurningRight
            if (Input.GetKeyDown(KeyCode.D))
            {
                animatorMotorL.SetBool("isLeftPressed", true);
            }
            if (Input.GetKeyUp(KeyCode.D))
            {
                animatorMotorL.SetBool("isLeftPressed", false);
            }
        }
        
        foreach (var animatorMotorR in animatorRight)
        {
            //TurningLeft
            if (Input.GetKeyDown(KeyCode.A))
            {
                animatorMotorR.SetBool("isLeftPressed", true);
            }
            if (Input.GetKeyUp(KeyCode.A))
            {
                animatorMotorR.SetBool("isLeftPressed", false);
            }
            
            //TurningRight
            if (Input.GetKeyDown(KeyCode.D))
            {
                animatorMotorR.SetBool("isRightPressed", true);
            }
            if (Input.GetKeyUp(KeyCode.D))
            {
                animatorMotorR.SetBool("isRightPressed", false);
            }
        }
    }
}
