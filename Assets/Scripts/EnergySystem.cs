using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergySystem : MonoBehaviour
{
    //Variable To Hold Current Energy
    public float Energy;
    public bool bHasEnergy;
    //Variable To Hold Reference to the Sprite
    //Add Energy
    public void AddEnergy(float Amount)
    {
        Debug.Log(Energy);
        Energy += Amount;
    }
    //Subtract Energy
    public void ConsumeEnergy(float Amount)
    {
        Energy -= Amount;
        if(Energy <= 0f)
        {
            bHasEnergy = false;
        }
    }
    
    
}
