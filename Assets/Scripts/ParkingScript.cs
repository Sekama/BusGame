using System;
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

   private void Start()
   {
      _playerController = playableCharacter.GetComponent<Player_Controller>();
      playerCollider = playableCharacter.GetComponent<BoxCollider>();
      _meshRenderer = gameObject.GetComponentInChildren<MeshRenderer>();
   }

   private void OnTriggerStay(Collider other)
   {
      // playerController._moveSpeed is set to public
      if (other == playerCollider && _playerController._moveSpeed <= _stopSpeed)
      {
         Debug.Log("slow enough");
         //Stop the bus's forward auto-drive and disable controls
         //bIsAtStation is set to public
         _playerController._bIsAtStation = true;
         _meshRenderer.material = _stoppedMat;
         //play boarding animation
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
}
