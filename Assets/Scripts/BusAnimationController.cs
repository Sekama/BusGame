using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class BusAnimationController : MonoBehaviour
{
    [SerializeField] private Animator animatorBus;  
    
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
            animatorBus.SetBool("isLeftPressed", true);
        }
        if(InContext.canceled)
        {
            _bIsLeftPressed = false;
            animatorBus.SetBool("isLeftPressed", false);

        }
    }

    private void TurnRight(InputAction.CallbackContext InContext)
    {
        if(InContext.started)
        {
            _bIsRightPressed = true;
            animatorBus.SetBool("isRightPressed", true);
        }
        if(InContext.canceled)
        {
            _bIsRightPressed = false;
            animatorBus.SetBool("isRightPressed", false);
        }
    }

    private void Brake()
    {
        if(_bIsLeftPressed && _bIsRightPressed)
        {
            animatorBus.SetBool("isBraking", true);

        }
        else
        {
            animatorBus.SetBool("isBraking", false);
        }
    }
}
