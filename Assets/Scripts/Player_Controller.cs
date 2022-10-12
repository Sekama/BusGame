using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Player_Controller : MonoBehaviour
{
    //Static const
    static float MAX_SPEED = 50;
    //References For Movement
    public InputMaster playerInputMaster;
    //Variables for Movement
    [SerializeField] private float _turnSpeed = 30;
    [SerializeField] private float _moveSpeed = 10;
    [SerializeField] private float _brakeSpeed = 1;
    [SerializeField] private float _accSpeed = 1;
    // acceleration speed in case of passenger alteration?
    
    private bool _bIsBraking = false;
    private bool _bIsLeftPressed = false;
    private bool _bIsRightPressed = false;

    //Variables for Pickup and Drop
    private bool _bIsAtStation = false;
    
    private void Awake()
    {
        //Binding the Player Input System to the Controller
        playerInputMaster = new InputMaster();
        playerInputMaster.Player_XBox.Enable();
        playerInputMaster.Player_XBox.TurnLeft.started += TurnLeft;
        playerInputMaster.Player_XBox.TurnLeft.canceled += TurnLeft;
        playerInputMaster.Player_XBox.TurnRight.started += TurnRight;
        playerInputMaster.Player_XBox.TurnRight.canceled += TurnRight;
        

        //Key Bindings (If needed)
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Turn();
        Brake();
        Move();
    }

    //Functions for Basic Movement
    private void Move()
    {
        Vector3 moveDirection = Camera.main.transform.forward;
        moveDirection.y = 0;
        if(_bIsBraking)
        {
            _moveSpeed = Mathf.Clamp(_moveSpeed -= _brakeSpeed, 0, MAX_SPEED);
        }
        else
        {
            _moveSpeed = Mathf.Clamp(_moveSpeed += _accSpeed, 0, MAX_SPEED);
        }


        //gameObject.transform.Translate(moveDirection * _moveSpeed * Time.deltaTime);]
        gameObject.transform.position += moveDirection * _moveSpeed * Time.deltaTime;
    }

    private void Turn()
    {
        float turnInput = 0;
        turnInput += _bIsLeftPressed ? -1f : 0f;
        turnInput += _bIsRightPressed ? 1f : 0f;
        gameObject.transform.Rotate(new Vector3(0, turnInput * _turnSpeed * Time.deltaTime, 0));
    }

    private void Brake()
    {
        if((_bIsLeftPressed && _bIsRightPressed) || _bIsAtStation)
        {
            _bIsBraking = true;
        }
        else
        {
            _bIsBraking = false;
        }
    }

    public void TurnLeft(InputAction.CallbackContext InContext)
    {
        Debug.Log("Turn Left");
        if(InContext.started)
        {
            _bIsLeftPressed = true;
        }
        if(InContext.canceled)
        {
            _bIsLeftPressed = false;
        }
    }

    public void TurnRight(InputAction.CallbackContext InContext)
    {
        Debug.Log("Turn Right");
        if (InContext.started)
        {
            _bIsRightPressed = true;
        }
        if (InContext.canceled)
        {
            _bIsRightPressed = false;
        }
    }

    //Functions for Changing Movement
    public void AddTurnSpeed(float deltaTurn)
    {
        _turnSpeed += deltaTurn;
    }

    public void AddMoveSpeed(float deltaMove)
    {
        _moveSpeed += deltaMove;
    }


}
