using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PassengerList : MonoBehaviour
{
    [SerializeField] private BusStateManager _bsm;
    public Image[] PassengerImages;

    private void Awake()
    {
        _bsm.OnboardChanges = SetPassengers;
    }

    public void SetPassengers()
    {
        foreach(var Img in PassengerImages)
        {
            Img.color = new Color(0,0,0,0);
        }
        for(int i = 0; i < _bsm.PassengersOnBoard.Count; ++i)
        {
            PassengerImages[i].sprite = _bsm.PassengersOnBoard[i].PasData.BotImage;
            PassengerImages[i].color = new Color(1, 1, 1, 1);
        }
        
    }
}
