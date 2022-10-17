using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergySystem : MonoBehaviour
{
    public delegate void FEnergyChanged();
    public FEnergyChanged EnergyChanged;
    //Variable To Hold Current Energy
    public float Energy;
    public bool bHasEnergy;
    //Add Energy
    public void AddEnergy(float Amount)
    {
        Energy += Amount;
        EnergyChanged();
    }
    //Subtract Energy
    public void ConsumeEnergy(float Amount)
    {
        Energy -= Amount;
        EnergyChanged();
        if (Energy <= 0f)
        {
            bHasEnergy = false;
        }
    }
    
    
}
