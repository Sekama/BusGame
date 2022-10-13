using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ParkingScript : MonoBehaviour
{
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


    private void Awake()
    {
        _passengers = new List<GameObject>();
    }
    private void Start()
    {
        _playerController = playableCharacter.GetComponent<Player_Controller>();
        playerCollider = playableCharacter.GetComponent<BoxCollider>();
        _meshRenderer = gameObject.GetComponentInChildren<MeshRenderer>();
        busStateManager = playableCharacter.GetComponent<BusStateManager>();
        //Testing
        int temp = Random.Range(1, 4);
        for(int i = 0; i < temp; ++i)
        {
            CreatePassenger(PossiblePassengers[Random.Range(0, PossiblePassengers.Count)]);
        }
        
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == playerCollider && _playerController.MoveSpeed <= _stopSpeed && !_playerController._bIsAtStation)
        {
            //Stop the bus's forward auto-drive and disable controls
            //bIsAtStation is set to public
            _playerController._bIsAtStation = true;
            _meshRenderer.material = _stoppedMat;
            //play boarding animation
            Invoke("SendPassengersToBus", timeStopped / 2);
            //apply driving modifiers
            //prompt player to signal they are ready
            Invoke("ResumeDriving", timeStopped);
            //resume auto-driving and gameplay
        }
    }

    private void ResumeDriving()
    {
        
        _playerController._bIsAtStation = false;
        _meshRenderer.material = _mainMat;
    }

    void CreatePassenger(PassengerData InData)
    {
        GameObject Passenger = GameObject.Instantiate(PassengerPreFab, gameObject.transform);
        Passenger.GetComponent<BotScript>().SetBot(InData);
        float randpos = Random.Range(-5f, +5f);
        Passenger.transform.localPosition = new Vector3(randpos, 0, randpos);// Delete Later Maybe?
        _passengers.Add(Passenger);
    }

    void SendPassengersToBus()
    {
        
        foreach(var Passenger in _passengers)
        {
            busStateManager.AddPassenger(Passenger.GetComponent<BotScript>().PasData);
            Destroy(Passenger);
        }
        _passengers.Clear();
        
        
        ResumeDriving();
    }
    

    
}
