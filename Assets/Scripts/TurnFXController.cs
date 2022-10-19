using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class TurnFXController : MonoBehaviour
{
    //References For Animators
    [SerializeField] private Animator []  animatorLeft;  
    [SerializeField] private Animator []  animatorRight;  
    [SerializeField] private Animator []  animatorMotor;  
    
   
    //References For Movement
    public InputMaster playerInputMaster;
    
    //Variables for Movement
    private bool _bIsBraking = false;
    private bool _bIsLeftPressed = false;
    private bool _bIsRightPressed = false;

    private void Awake()
    {
        //Binding the Player Input System to the Controller
        playerInputMaster = new InputMaster();
        playerInputMaster.Player_XBox.Enable();
        playerInputMaster.Player_XBox.TurnLeft.started += TurnLeft;
        playerInputMaster.Player_XBox.TurnLeft.canceled += TurnLeft;
        playerInputMaster.Player_XBox.TurnRight.started += TurnRight;
        playerInputMaster.Player_XBox.TurnRight.canceled += TurnRight;    
    }

    private void FixedUpdate()
    {
        Brake();
    }
    
    private void TurnLeft(InputAction.CallbackContext InContext)
    {
        if(InContext.started)
        {
            Debug.Log("left pressed");
            _bIsLeftPressed = true;

            foreach (var animatorMotorR in animatorRight)
            {
                animatorMotorR.SetBool("isLeftPressed", true);
            }
            foreach (var animatorMotorL in animatorLeft)
            {
                animatorMotorL.SetBool("isRightPressed", true);
            }
        }
        if(InContext.canceled)
        {
            Debug.Log("left released");

            _bIsLeftPressed = false;

            foreach (var animatorMotorR in animatorRight)
            {
                animatorMotorR.SetBool("isLeftPressed", false);
            }
            foreach (var animatorMotorL in animatorLeft)
            {
                animatorMotorL.SetBool("isRightPressed", false);
            }
        }
    }

    private void TurnRight(InputAction.CallbackContext InContext)
    {
        if(InContext.started)
        {
            Debug.Log("right pressed");

            _bIsRightPressed = true;

            foreach (var animatorMotorR in animatorRight)
            {
                animatorMotorR.SetBool("isRightPressed", true);
            }
            foreach (var animatorMotorL in animatorLeft)
            {
                animatorMotorL.SetBool("isLeftPressed", true);
            }
        }
        if(InContext.canceled)
        {
            Debug.Log("right released");

            _bIsRightPressed = false;

            foreach (var animatorMotorR in animatorRight)
            {
                animatorMotorR.SetBool("isRightPressed", false);
            }
            foreach (var animatorMotorL in animatorLeft)
            {
                animatorMotorL.SetBool("isLeftPressed", false);
            }
        }
    }

    private void Brake()
    {
        if(_bIsLeftPressed && _bIsRightPressed)
        {
            Debug.Log("brake pressed");

            foreach (var animatorMotor in animatorMotor)
            {
                animatorMotor.SetBool("isBraking", true);
            }
            _bIsBraking = true;
        }
        else
        {
            Debug.Log("brake released");

            _bIsBraking = false;
            foreach (var animatorMotor in animatorMotor)
            {
                animatorMotor.SetBool("isBraking", false);
            }
        }
    }
    
    
}

