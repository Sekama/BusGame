using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RechargeScript : MonoBehaviour
{
    
    private GameObject _playableCharacter;
    private Player_Controller _playerController;
    private EnergySystem _energySystem;
    private BoxCollider _playerCollider;

    [SerializeField] private Material _mainMat;
    [SerializeField] private Material _stoppedMat;
    [SerializeField] private MeshRenderer _meshRenderer;

    //Fuel to Economy Link
    [SerializeField] private int _pricePerUnit;
    [SerializeField] private int _unitCharge;
    private int _amountFill;

    private void Awake()
    {
        _playableCharacter = GameObject.FindGameObjectWithTag("Player");
        _playerController = _playableCharacter.GetComponent<Player_Controller>();
        _energySystem = _playableCharacter.GetComponent<EnergySystem>();
        _playerCollider = _playableCharacter.GetComponent<BoxCollider>();
    }
    private void Start()
    {
        _meshRenderer = gameObject.GetComponentInChildren<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == _playerCollider && !_playerController._bIsAtStation)
        {
            //Stop the bus's forward auto-drive and disable controls
            _playerController._bIsAtStation = true;
            _meshRenderer.material = _stoppedMat;
            Recharge();

        }
    }

    private void Recharge()
    {
        if (_energySystem.Energy >= 100f || _playerController.Money < _pricePerUnit)
        {
            ResumeDriving();
        }
        else
        {
            _energySystem.AddEnergy(_unitCharge);
            _playerController.Money -= _pricePerUnit;
            Invoke("Recharge", 1f);
        }

    }

    private void ResumeDriving()
    {
        _playerController._bIsAtStation = false;
        _meshRenderer.material = _mainMat;
    }

}
