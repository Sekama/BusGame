using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpeedometerScript : MonoBehaviour
{
    private GameObject _playableCharacter;
    private Rigidbody _playerRb;
    //public Text _text;
    public TMP_Text _textMeshPro;
    private void Awake()
    {
        _playableCharacter = GameObject.FindGameObjectWithTag("Player");
        _playerRb = _playableCharacter.GetComponent<Rigidbody>();
    }
  

    
    void Update()
    {
        
        _textMeshPro.SetText(_playerRb.velocity.magnitude.ToString("N0")); 
    }
}
