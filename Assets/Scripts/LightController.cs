using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class LightController : MonoBehaviour
{
    [SerializeField] private Animator []  busLight;  
    
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
  
    void Update()
    {
        Brake();
        
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
    private void TurnLeft(InputAction.CallbackContext InContext)
    {
        if(InContext.started)
        {
            _bIsLeftPressed = true;
        }
        if(InContext.canceled)
        {
            _bIsLeftPressed = false;
        }
    }

    private void TurnRight(InputAction.CallbackContext InContext)
    {
        if(InContext.started)
        {
            _bIsRightPressed = true;
       }
        if(InContext.canceled)
        {
            _bIsRightPressed = false;
        }
    }
    
    private void Brake()
    {
        if(_bIsLeftPressed && _bIsRightPressed)
        {
            foreach (var busLightBulb in busLight)
            {
                busLightBulb.SetBool("isBraking", true);
            }
            
        }
        else
        {
            foreach (var busLightBulb in busLight)
            {
                busLightBulb.SetBool("isBraking", false);
            }
        }
    }
}
