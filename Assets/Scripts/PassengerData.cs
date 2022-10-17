using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum EPassengerType
{
    Bashful,
    Doc,
    Dopey,
    Grumpy,
    Happy,
    Sleepy,
    Sneezy
};

[CreateAssetMenu(fileName = "Passenger Data", menuName = "Passengers/ Passenger Data", order = 1)]
public class PassengerData : ScriptableObject
{
    public string Name;
    public EPassengerType PasType;
    public float AccMod;
    public float BrakeMod;
    public float SpeedMod;
    public float TurnMod;
    public bool canDrain;
    public int Money;
    public int NoOfStops;
    public Sprite BotImage;
    public Mesh BotMesh;
    public Material[] BotMaterial;
}
