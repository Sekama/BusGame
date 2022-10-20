using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyMeter : MonoBehaviour
{
    private GameObject _playableCharacter;
    private Player_Controller _playerController;
    //public Text _text;
    public TMP_Text _textMeshPro;
    private void Awake()
    {
        _playableCharacter = GameObject.FindGameObjectWithTag("Player");
        _playerController = _playableCharacter.GetComponent<Player_Controller>();
    }
  

    
    void Update()
    {
        _textMeshPro.SetText(_playerController.Money.ToString("N0")); 
    }
}
