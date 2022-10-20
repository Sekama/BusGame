using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class TurnFXController : MonoBehaviour
{
    //References For Animators
    [SerializeField] private Animator []  animatorLeft, animatorRight, animatorMotor;  
   
    [SerializeField] private Animator []  animatorLighLeft, animatorLightRight, animatorLightMotor;  
 
    
   
    //References For Movement
    public InputMaster playerInputMaster;
    
    //Variables for Movement
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
            _bIsLeftPressed = true;

            //FX
            foreach (var animatorMotorR in animatorRight)
            {
                animatorMotorR.SetBool("isLeftPressed", true);
            }
            foreach (var animatorMotorL in animatorLeft)
            {
                animatorMotorL.SetBool("isRightPressed", true);
            }
            
            //Lights
            foreach (var animatorLightMotorR in animatorLightRight)
            {
                animatorLightMotorR.SetBool("isLeftPressed", true);
            }
            foreach (var animatorLightMotorL in animatorLighLeft)
            {
                animatorLightMotorL.SetBool("isRightPressed", true);
            }
        }
        if(InContext.canceled)
        {
            _bIsLeftPressed = false;

            //FX
            foreach (var animatorMotorR in animatorRight)
            {
                animatorMotorR.SetBool("isLeftPressed", false);
            }
            foreach (var animatorMotorL in animatorLeft)
            {
                animatorMotorL.SetBool("isRightPressed", false);
            }
            
            //Lights
            foreach (var animatorLightMotorR in animatorLightRight)
            {
                animatorLightMotorR.SetBool("isLeftPressed", false);
            }
            foreach (var animatorLightMotorL in animatorLighLeft)
            {
                animatorLightMotorL.SetBool("isRightPressed", false);
            }
        }
    }

    private void TurnRight(InputAction.CallbackContext InContext)
    {
        if(InContext.started)
        {
            _bIsRightPressed = true;

            //FX
            foreach (var animatorMotorR in animatorRight)
            {
                animatorMotorR.SetBool("isRightPressed", true);
            }
            foreach (var animatorMotorL in animatorLeft)
            {
                animatorMotorL.SetBool("isLeftPressed", true);
            }
            
            //Lights
            foreach (var animatorLightMotorR in animatorLightRight)
            {
                animatorLightMotorR.SetBool("isRightPressed", true);
            }
            foreach (var animatorLightMotorL in animatorLighLeft)
            {
                animatorLightMotorL.SetBool("isLeftPressed", true);
            }
        }
        
        
        if(InContext.canceled)
        {
            _bIsRightPressed = false;

            //FX
            foreach (var animatorMotorR in animatorRight)
            {
                animatorMotorR.SetBool("isRightPressed", false);
            }
            foreach (var animatorMotorL in animatorLeft)
            {
                animatorMotorL.SetBool("isLeftPressed", false);
            }
            
            //Lights
            foreach (var animatorLightMotorR in animatorLightRight)
            {
                animatorLightMotorR.SetBool("isRightPressed", false);
            }
            foreach (var animatorLightMotorL in animatorLighLeft)
            {
                animatorLightMotorL.SetBool("isLeftPressed", false);
            }
        }
    }

    private void Brake()
    {
        if(_bIsLeftPressed && _bIsRightPressed)
        {
            foreach (var animatorMotor in animatorMotor)
            {
                animatorMotor.SetBool("isBraking", true);
            }
            foreach (var animatorLightMotor in animatorLightMotor)
            {
                animatorLightMotor.SetBool("isBraking", true);
            }
        }
        else
        {
            foreach (var animatorMotor in animatorMotor)
            {
                animatorMotor.SetBool("isBraking", false);
            }
            foreach (var animatorLightMotor in animatorLightMotor)
            {
                animatorLightMotor.SetBool("isBraking", false);
            }
        }
    }
    
    
}

