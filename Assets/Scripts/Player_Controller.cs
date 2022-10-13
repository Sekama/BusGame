using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Player_Controller : MonoBehaviour
{
    
    
    //References For Movement
    public InputMaster playerInputMaster;
    //Variables for Movement
    [SerializeField] private float _turnSpeed = 30;
    public float MoveSpeed { get; private set; }
    private float _max_speed = 25;
    [SerializeField] private float _brakeSpeed = 1;
    [SerializeField] private float _accSpeed = 1;
    // acceleration speed in case of passenger alteration?
    
    private bool _bIsBraking = false;
    private bool _bIsLeftPressed = false;
    private bool _bIsRightPressed = false;
    //Passenger Alteration variables
    public float MaxSpeedMod;
    public float WobbleMod;
    public float AccMod;
    public float BrakeMod;


    //Variables for Pickup and Drop
    public bool _bIsAtStation = false;
    
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
            MoveSpeed = Mathf.Clamp(MoveSpeed -= (_brakeSpeed - BrakeMod), 0, (_max_speed + MaxSpeedMod));
        }
        else
        {
            MoveSpeed = Mathf.Clamp(MoveSpeed += (_accSpeed + AccMod), 0, (_max_speed + MaxSpeedMod));
        }
        gameObject.transform.Rotate(new Vector3(0, WobbleMod * (Random.Range(0, 100) % 2 == 0 ? -1f : 1f) * Time.deltaTime, 0));
        //gameObject.transform.Translate(moveDirection * _moveSpeed * Time.deltaTime);
        gameObject.transform.position += moveDirection * MoveSpeed * Time.deltaTime;
        
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
        MoveSpeed += deltaMove;
    }

   


}
