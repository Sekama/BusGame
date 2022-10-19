using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ParkingScript : MonoBehaviour
{
    //Event Delegates
    public delegate void FCheckDropoffs();
    FCheckDropoffs CheckDropOffs;
    private GameObject _playableCharacter;
    private Player_Controller _playerController;
    private BoxCollider _playerCollider;

    [SerializeField] private float _stopSpeed;
    [SerializeField] private Material _mainMat;
    [SerializeField] private Material _stoppedMat;
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private float timeStopped;
    public Transform PickupTransform;
    public Transform DropoffTransform;

    //Passenger LogiC Variables
    public List<PassengerData> PossiblePassengers;

    //Pickup And Drop Variables
    public GameObject PassengerPreFab;
    private List<GameObject> _passengers;
    private BusStateManager _busStateManager;
    private List<GameObject> _garbagePassengers;

    //Bus Route variables
    public List<GameObject> StopsInRadius;
    [SerializeField] private float _radius;
    public bool bIsActive;

    private void Awake()
    {
        _passengers = new List<GameObject>();
        _garbagePassengers = new List<GameObject>();
        _playableCharacter = GameObject.FindGameObjectWithTag("Player");
        _playerController = _playableCharacter.GetComponent<Player_Controller>();
        _busStateManager = _playableCharacter.GetComponent<BusStateManager>();
        _playerCollider = _playableCharacter.GetComponent<BoxCollider>();
        bIsActive = false;
    }
    private void Start()
    {

        _meshRenderer = gameObject.GetComponentInChildren<MeshRenderer>();
        CreatePickups();
    }
    public void GetNearbyStops()
    {
        StopsInRadius = new List<GameObject>();
        foreach (var Stop in GameObject.FindGameObjectsWithTag("BusStop"))
        {
            if (Vector3.Distance(gameObject.transform.position, Stop.transform.position) <= _radius && Stop != this.gameObject)
            {
                StopsInRadius.Add(Stop);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (bIsActive)
        {
            if (other == _playerCollider && _playerController.MoveSpeed <= _stopSpeed && !_playerController._bIsAtStation)
            {
                //Stop the bus's forward auto-drive and disable controls
                _playerController._bIsAtStation = true;
                _playerController.AtStation(true);
                _meshRenderer.material = _stoppedMat;
                CheckDropOffs += _busStateManager.ReduceStopCount;
                CheckDropOffs();
                CreateDropOffs(ref _busStateManager.PassengersDropOff);
                Invoke("SendPassengersToBus", timeStopped / 2);

            }
        }

    }

    private void ResumeDriving()
    {
        CheckDropOffs -= _busStateManager.ReduceStopCount;
        _playerController._bIsAtStation = false;
        _playerController.AtStation(false);
        _meshRenderer.material = _mainMat;
        Invoke("CreatePickups", 2f);
    }
    void CreatePickups()
    {
        foreach (var Passenger in _garbagePassengers)
        {
            Passenger.GetComponent<BotScript>().CallDestroy();
        }
        _garbagePassengers.Clear();
        int temp = Random.Range(1, 3);
        for (int i = 0; i < temp; ++i)
        {
            float PassChance = Random.Range(0f, 10f);
            PassengerData Choice = PossiblePassengers[0]; //Defaulting
            switch (PassChance)
            {
                case < 4f:
                    Choice = PossiblePassengers[0];
                    break;
                case < 5f:
                    Choice = PossiblePassengers[1];
                    break;
                case < 6f:
                    Choice = PossiblePassengers[2];
                    break;
                case < 7f:
                    Choice = PossiblePassengers[3];
                    break;
                case < 8f:
                    Choice = PossiblePassengers[4];
                    break;
                case < 9f:
                    Choice = PossiblePassengers[5];
                    break;
                case < 10f:
                    Choice = PossiblePassengers[6];
                    break;
                default:
                    Choice = PossiblePassengers[3];
                    break;

            }
            GameObject NewPassenger = CreatePassenger(Choice);
            NewPassenger.transform.localPosition = new Vector3(i * 1f, 0f, 0.2f);
            _passengers.Add(NewPassenger);
        }
    }

    void CreateDropOffs(ref List<BotScript> OutDropOffPassengers)
    {
        int i = 0;
        foreach (var Passenger in OutDropOffPassengers)
        {
            GameObject PassengerPF = Passenger.gameObject;
            PassengerPF.transform.parent = DropoffTransform;
            PassengerPF.transform.localPosition = new Vector3(i * 1f, 0f, 0.2f);
            PassengerPF.GetComponent<MeshRenderer>().enabled = true;
            _garbagePassengers.Add(PassengerPF);
            ++i;
        }
        OutDropOffPassengers.Clear();
    }
    GameObject CreatePassenger(PassengerData InData)
    {
        GameObject Passenger = GameObject.Instantiate(PassengerPreFab, PickupTransform);
        Passenger.GetComponent<BotScript>().SetBot(InData);
        return Passenger;
    }

    void SendPassengersToBus()
    {

        foreach (var Passenger in _passengers)
        {
            if (_busStateManager.PassengersOnBoard.Count < _busStateManager.MaxPassengers)
            {
                _busStateManager.AddPassenger(Passenger.GetComponent<BotScript>());
                Passenger.transform.parent = _busStateManager.gameObject.transform;
                Passenger.GetComponent<MeshRenderer>().enabled = false;
            }
            else
            {
                Passenger.GetComponent<BotScript>().CallDestroy();
            }
        }
        _passengers.Clear();


        ResumeDriving();
    }



}
