using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum EPassengerType
{
    Bashful,
    Dopey,
    Sleepy
};

[CreateAssetMenu(fileName = "Passenger Data", menuName = "Passengers/ Passenger Data", order = 1)]
public class PassengerData : ScriptableObject
{
    public string Name;
    public EPassengerType PasType;
    public float AccMod;
    public float BrakeMod;
    public float SpeedMod;
    public float WobbleMod;
    public int NoOfStops;
    public Mesh BotMesh;
    public Material BotMaterial;
}
