using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ParkingScript : MonoBehaviour
{
   public GameObject playableCharacter;
   public BoxCollider playerCollider;
   
   [SerializeField] private float stopSpeed;
   [SerializeField] private Player_Controller _playerController;

   private void Start()
   {
      _playerController = playableCharacter.GetComponent<Player_Controller>();
      playerCollider = playableCharacter.GetComponent<BoxCollider>();
   }

   private void OnTriggerStay(Collider other)
   {
      if (other == playerCollider && _playerController._moveSpeed <= stopSpeed)
      {
         Debug.Log("slow enough");
         //Stop the bus's forward auto-drive and disable controls
         _playerController._bIsAtStation = true;
         //play boarding animation
         //apply driving modifiers
         //prompt player to signal they are ready
         Invoke("ResumeDriving", 2);
         //resume auto-driving and gameplay
      }
   }

   private void ResumeDriving()
   {
      _playerController._bIsAtStation = false;
   }
}
