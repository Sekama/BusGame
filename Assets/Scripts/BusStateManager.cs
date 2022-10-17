using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusStateManager : MonoBehaviour
{
    public delegate void FOnboardChanges();
    public FOnboardChanges OnboardChanges;
    [SerializeField] Player_Controller _pc;
    public int MaxPassengers;
    //Variable to Store The Passenger INfo
    public List<BotScript> PassengersOnBoard;
    public List<BotScript> PassengersDropOff;
    //Variable to Hold the Energy Sprite
    private float _accMod;
    private float _brakeMod;
    private float _maxSpeedMod;
    private float _turnMod;
    private bool _bCanDrain;

    


    private void Awake()
    {
        PassengersOnBoard = new List<BotScript>();
        PassengersDropOff = new List<BotScript>();
    }

    

    //Stamps the Effects on to the PC
    private void SendEffects()
    {
        _pc.AccMod = _accMod;
        _pc.BrakeMod = _brakeMod;
        _pc.MaxSpeedMod = _maxSpeedMod;
        _pc.TurnMod = _turnMod;
        _pc.bCanDrain = _bCanDrain;
        OnboardChanges();
    }

    //Common Functions to Collate the Effects of the Passegers
    private void CollateEffects()
    {
        bool bHasDoc = false;
        _accMod = _brakeMod = _maxSpeedMod = _turnMod = 0;
        _bCanDrain = true;
        foreach (var Passenger in PassengersOnBoard)
        {
            switch(Passenger.PasData.PasType)
            {
                case EPassengerType.Bashful:
                    _accMod += Passenger.PasData.AccMod;
                    break;
                case EPassengerType.Doc:
                    bHasDoc = true;
                    break;
                case EPassengerType.Dopey:
                    _turnMod += Passenger.PasData.TurnMod;
                    break;
                case EPassengerType.Grumpy:
                    break;
                case EPassengerType.Happy:
                    _bCanDrain = false;
                    break;
                case EPassengerType.Sleepy:
                    _maxSpeedMod += Passenger.PasData.SpeedMod;
                    break;
                case EPassengerType.Sneezy:
                    _maxSpeedMod += Passenger.PasData.SpeedMod;
                    break;
            }
        }
        if(bHasDoc)
        {
            _maxSpeedMod = 0;
            _accMod = 0;
            _brakeMod = 0;
            _turnMod = 0;
            _bCanDrain = true;
            
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
                _pc.Money += Passenger.PasData.Money;
                Debug.Log(_pc.Money);
            }
        }
        foreach(var Passenger in PassengersDropOff)
        {
            PassengersOnBoard.Remove(Passenger);
        }
        CollateEffects();
    }
    
}
