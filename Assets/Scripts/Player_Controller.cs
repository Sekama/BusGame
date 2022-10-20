using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class Player_Controller : MonoBehaviour
{
    public delegate void FAtStation(bool bIsAtStation);
    public FAtStation AtStation;
    //References For Movement
    public InputMaster playerInputMaster;
    private Rigidbody _rb;
    //Variables for Movement
    public float MoveSpeed { get; private set; }
    [SerializeField] private float _turnSpeed = 120;
    [SerializeField] private float _max_speed = 45;
    [SerializeField] private float _brakeSpeed = 0.5f;
    [SerializeField] private float _accSpeed = 0.15f;
    private bool _bIsBraking = false;
    private bool _bIsLeftPressed = false;
    private bool _bIsRightPressed = false;

    private RaycastHit _hit;
    //Passenger Alteration variables
    public float MaxSpeedMod;
    public float TurnMod;
    public float AccMod;
    public float BrakeMod;
    public bool bCanDrain;
    //Variables for Energy System
    private EnergySystem _es;
    [SerializeField] float ConsumptionRate;

    //Variables for Pickup and Drop
    public bool _bIsAtStation = false;


    //Variables for Economy
    public int Money;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _es = GetComponent<EnergySystem>();
        _es.bHasEnergy = true;
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
        _es.Energy = 100.0f;
        bCanDrain = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Turn();
        Brake();
        if(_es.bHasEnergy)
        {
            Move();
            YOrientation();
        }
        else
        {
            EndScene();
        }
    }

    private void LateUpdate()
    {
        
        if (_rb.velocity.sqrMagnitude > 1 && bCanDrain)
        {
            _es.ConsumeEnergy(ConsumptionRate * Time.deltaTime);
        }
    }

    //Functions for Basic Movement
    private void Move()
    {

        Vector3 moveDirection = Camera.main.transform.forward;
        if(_bIsBraking && _bIsAtStation)
        {
            MoveSpeed = Mathf.Lerp(MoveSpeed, 0f, (MoveSpeed / 360));
        }
        else if(_bIsBraking)
        {
            MoveSpeed = Mathf.Clamp(MoveSpeed -= (_brakeSpeed - BrakeMod), 0, (_max_speed + MaxSpeedMod));
        }
        else
        {
            MoveSpeed = Mathf.Clamp(MoveSpeed += (_accSpeed + AccMod), 0, (_max_speed + MaxSpeedMod));
        }
        //gameObject.transform.Rotate(new Vector3(0, WobbleMod * (Random.Range(0, 100) % 2 == 0 ? -1f : 1f) * Time.deltaTime, 0));
        //gameObject.transform.Translate(moveDirection * _moveSpeed * Time.deltaTime);
        //gameObject.transform.position += moveDirection * MoveSpeed * Time.deltaTime;
        _rb.velocity = moveDirection * MoveSpeed;
        
        
    }

    private void Turn()
    {
        float turnInput = 0;
        turnInput += _bIsLeftPressed ? -1f : 0f;
        turnInput += _bIsRightPressed ? 1f : 0f;
        gameObject.transform.Rotate(new Vector3(0, turnInput * (_turnSpeed + TurnMod) * Time.deltaTime, 0));
        
        
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

    private void YOrientation()
    {
        Physics.Raycast(gameObject.transform.position, Vector3.down, out _hit);
        
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, (_hit.point.y + 0.4f), gameObject.transform.position.z);
        
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
    private void EndScene()
    {
        SceneManager.LoadScene(4);
    }

    
   


}
