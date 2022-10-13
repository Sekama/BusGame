using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpeedometerScript : MonoBehaviour
{
    public GameObject playableCharacter;
    //public Text _text;
    public TMP_Text _textMeshPro;
    public Player_Controller _playerController;
    
    void Start()
    {
        _playerController = playableCharacter.GetComponent<Player_Controller>();
    }

    
    void Update()
    {
        _textMeshPro.SetText(_playerController.MoveSpeed.ToString("N0")); 
    }
}
