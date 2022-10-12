using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkingScript : MonoBehaviour
{
   public GameObject playableCharacter;
   
   [SerializeField] private float stopSpeed;
   [SerializeField] private Rigidbody _busRB;
   //[SerializeField] private PlayerController _playerController;

   private void Start()
   {
     // _playerController = GetComponent<PlayerController>();
      _busRB = playableCharacter.GetComponent<Rigidbody>();
   }

   private void OnTriggerEnter(Collider other)
   {
      if (other == playableCharacter && _busRB.velocity.magnitude <= stopSpeed)
      {
         //Stop the bus's forward auto-drive and disable controls
         //play boarding animation
         //apply driving modifiers
         //prompt player to signal they are ready
         //resume auto-driving and gameplay
      }
   }
}
