using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusAnimationController : MonoBehaviour
{
    
    [SerializeField] private Animator animatorBus;  

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Braking
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animatorBus.SetBool("isBraking", true);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            animatorBus.SetBool("isBraking", false);
        }

        //TurningLeft
        if (Input.GetKeyDown(KeyCode.A))
        {
            animatorBus.SetBool("isLeftPressed", true);
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            animatorBus.SetBool("isLeftPressed", false);
        }
            
        //TurningRight
        if (Input.GetKeyDown(KeyCode.D))
        {
            animatorBus.SetBool("isRightPressed", true);
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            animatorBus.SetBool("isRightPressed", false);
        }
    }
}
