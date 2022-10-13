using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusStateManager : MonoBehaviour
{
    [SerializeField] Player_Controller _pc;
    //Variable to Store The Passenger INfo
    public List<PassengerData> PassengersOnBoard;
    private float _accMod;
    private float _brakeMod;
    private float _maxSpeedMod;
    private float _wobbleMod;

    //Testing
    public PassengerData TestData;


    private void Awake()
    {
        PassengersOnBoard = new List<PassengerData>();
    }

    //Remove Passenger
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Stamps the Effects on to the PC
    private void SendEffects()
    {
        _pc.AccMod = _accMod;
        _pc.BrakeMod = _brakeMod;
        _pc.MaxSpeedMod = _maxSpeedMod;
        _pc.WobbleMod = _wobbleMod;
    }

    //Common Functions to Collate the Effects of the Passegers
    private void CollateEffects()
    {
        _accMod = _brakeMod = _maxSpeedMod = _wobbleMod = 0;
        foreach (var Passenger in PassengersOnBoard)
        {
            switch(Passenger.PasType)
            {
                case EPassengerType.Bashful:
                    _maxSpeedMod += Passenger.SpeedMod;
                    break;
                case EPassengerType.Dopey:
                    _wobbleMod += Passenger.WobbleMod;
                    break;
                case EPassengerType.Sleepy:
                    _maxSpeedMod += Passenger.SpeedMod;
                    break;
            }
        }

        SendEffects();
    }


    //Add Passenger
    public void AddPassenger(PassengerData InPassenger)
    {
        Debug.Log(InPassenger.PasType);
        PassengersOnBoard.Add(InPassenger);
        CollateEffects();
    }

    public void RemovePassenger(string InName)
    {
        foreach(var Passenger in PassengersOnBoard)
        {
            if(Passenger.Name == InName)
            {
                PassengersOnBoard.Remove(Passenger);
                CollateEffects();
                return;
            }
        }
    }
}
