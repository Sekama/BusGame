using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusStateManager : MonoBehaviour
{
    [SerializeField] Player_Controller _pc;
    //Variable to Store The Passenger INfo
    public List<BotScript> PassengersOnBoard;
    public List<BotScript> PassengersDropOff;
    private float _accMod;
    private float _brakeMod;
    private float _maxSpeedMod;
    private float _wobbleMod;

    //Testing
    public PassengerData TestData;


    private void Awake()
    {
        PassengersOnBoard = new List<BotScript>();
        PassengersDropOff = new List<BotScript>();
    }

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
            switch(Passenger.PasData.PasType)
            {
                case EPassengerType.Bashful:
                    _maxSpeedMod += Passenger.PasData.SpeedMod;
                    break;
                case EPassengerType.Dopey:
                    _wobbleMod += Passenger.PasData.WobbleMod;
                    break;
                case EPassengerType.Sleepy:
                    _maxSpeedMod += Passenger.PasData.SpeedMod;
                    break;
            }
        }

        SendEffects();
    }
    //Reduce StopCounts on Passengers
    public void ReduceStopCount()
    {
        foreach(var Passenger in PassengersOnBoard)
        {
            --Passenger.StopsLeft;
            
        }
        CollectDropOffs();
    }

    //Add Passenger
    public void AddPassenger(BotScript InPassenger)
    {
        
        PassengersOnBoard.Add(InPassenger);
        CollateEffects();
    }

    public void CollectDropOffs()
    {
        foreach(var Passenger in PassengersOnBoard)
        {
            if (Passenger.StopsLeft == 0)
            {
                PassengersDropOff.Add(Passenger);
                
            }
        }
        foreach(var Passenger in PassengersDropOff)
        {
            PassengersOnBoard.Remove(Passenger);
        }
        CollateEffects();
    }
    ////Remove Passenger
    //public void RemovePassenger(BotScript InPassenger)
    //{
    //    Debug.Log(PassengersOnBoard.Remove(InPassenger));
    //    CollateEffects();
    //    return;
    //}
}
