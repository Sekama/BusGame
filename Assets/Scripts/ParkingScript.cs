using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ParkingScript : MonoBehaviour
{
    //Event Delegates
    public delegate void FCheckDropoffs();
    FCheckDropoffs CheckDropOffs;
    public GameObject playableCharacter;
    public BoxCollider playerCollider;

    [SerializeField] private float _stopSpeed;
    [SerializeField] private Player_Controller _playerController;
    [SerializeField] private Material _mainMat;
    [SerializeField] private Material _stoppedMat;
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private float timeStopped;

    //Passenger LogiC Variables
    public List<PassengerData> PossiblePassengers;

    //Pickup And Drop Variables
    public GameObject PassengerPreFab;
    private List<GameObject> _passengers;
    private BusStateManager busStateManager;
    private List<GameObject> _garbagePassengers;


    private void Awake()
    {
        _passengers = new List<GameObject>();
        _garbagePassengers = new List<GameObject>();
    }
    private void Start()
    {
        _playerController = playableCharacter.GetComponent<Player_Controller>();
        playerCollider = playableCharacter.GetComponent<BoxCollider>();
        _meshRenderer = gameObject.GetComponentInChildren<MeshRenderer>();
        busStateManager = playableCharacter.GetComponent<BusStateManager>();
        CreatePickups();
        
        
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == playerCollider && _playerController.MoveSpeed <= _stopSpeed && !_playerController._bIsAtStation)
        {
            //Stop the bus's forward auto-drive and disable controls
            //bIsAtStation is set to public
            _playerController._bIsAtStation = true;
            _meshRenderer.material = _stoppedMat;
            CheckDropOffs += busStateManager.ReduceStopCount;
            CheckDropOffs();
            CreateDropOffs(ref busStateManager.PassengersDropOff);
            //play boarding animation
            Invoke("SendPassengersToBus", timeStopped / 2);
            //apply driving modifiers
            //prompt player to signal they are ready
            //Invoke("ResumeDriving", timeStopped);
            //resume auto-driving and gameplay
        }
    }

    private void ResumeDriving()
    {
        CheckDropOffs -= busStateManager.ReduceStopCount;
        _playerController._bIsAtStation = false;
        _meshRenderer.material = _mainMat;
        Invoke("CreatePickups", 2f);
    }
    void CreatePickups()
    {
        Debug.Log("Create Pickups Called");
        foreach(var Passenger in _garbagePassengers)
        {
            Passenger.GetComponent<BotScript>().CallDestroy();
        }
        _garbagePassengers.Clear();
        int temp = Random.Range(1, 4);
        for (int i = 0; i < temp; ++i)
        {
            _passengers.Add(CreatePassenger(PossiblePassengers[Random.Range(0, PossiblePassengers.Count)]));
        }
    }

    void CreateDropOffs(ref List<BotScript> OutDropOffPassengers)
    {
        foreach (var Passenger in OutDropOffPassengers)
        {
            GameObject PassengerPF = Passenger.gameObject;
            PassengerPF.transform.parent = gameObject.transform;
            float randpos = Random.Range(-5f, +5f);
            PassengerPF.transform.localPosition = new Vector3(randpos, 0, randpos);
            PassengerPF.GetComponent<MeshRenderer>().enabled = true;
            _garbagePassengers.Add(PassengerPF);
        }
        OutDropOffPassengers.Clear();
    }
    GameObject CreatePassenger(PassengerData InData)
    {
        GameObject Passenger = GameObject.Instantiate(PassengerPreFab, gameObject.transform);
        Passenger.GetComponent<BotScript>().SetBot(InData);
        float randpos = Random.Range(-5f, +5f);
        Passenger.transform.localPosition = new Vector3(randpos, 0, randpos);// Delete Later Maybe?
        /*_passengers.Add(Passenger)*/
        return Passenger;
    }

    void SendPassengersToBus()
    {
        
        foreach(var Passenger in _passengers)
        {
            busStateManager.AddPassenger(Passenger.GetComponent<BotScript>());
            Passenger.transform.parent = busStateManager.gameObject.transform;
            Passenger.GetComponent<MeshRenderer>().enabled = false;
        }
        _passengers.Clear();
        
        
        ResumeDriving();
    }
    

    
}
