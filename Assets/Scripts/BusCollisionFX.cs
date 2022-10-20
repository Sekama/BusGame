using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusCollisionFX : MonoBehaviour
{
    public ParticleSystem collisionFX;
    [SerializeField] private Animator busCollission;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Buildings")
        {
            Debug.Log("Hit");
            busCollission.SetBool("isColliding", true);

        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Buildings")
        {
            busCollission.SetBool("isColliding", false);

        }
    }
}
