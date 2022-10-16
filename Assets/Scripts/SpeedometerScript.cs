using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpeedometerScript : MonoBehaviour
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
        _textMeshPro.SetText(_playerController.MoveSpeed.ToString("N0")); 
    }
}
