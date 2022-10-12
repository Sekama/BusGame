using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BatteryController : MonoBehaviour
{
    [Header("TurnSetting")]
    [SerializeField] private float batteryTime;
    private float currentBatteryTime;
    private float batteryStep;
    private float batteryStepsNumber;

    [Header("UI")]
    [SerializeField] private GameObject[] batteryBar;
    
    void Start()
    {
        currentBatteryTime = batteryTime;
    }

    private void Update()
    {
        BatteryLife();
        CollissionBatteryDamage();
        Debug.Log(currentBatteryTime.ToString());
    }

    private void CollissionBatteryDamage()
    {
        //Replace if with OnCollissionEnter
        // if (Input.anyKeyDown)
        // {
        //     currentBatteryTime -= batteryStep;
        //     Debug.Log("Ouch!");
        // }
    }
    
    private void BatteryLife()
    {
        batteryStepsNumber = batteryBar.Length;
        currentBatteryTime -= 1 * Time.deltaTime;
        batteryStep = batteryTime / batteryStepsNumber;

        for (int i = 0; i < batteryStepsNumber; i++)
        {
            if (currentBatteryTime > batteryStep * (batteryStepsNumber - (i + 1))  && currentBatteryTime < batteryStep * (batteryStepsNumber - i) )
            {
                batteryBar[i].SetActive(true);
            }
            else
            {
                batteryBar[i].SetActive(false);
            }
        }
        
        if (currentBatteryTime <= 0)
        {
            //Insert go to GameOver screen here
            Debug.Log("Game over");
            currentBatteryTime = 0;
        }
    }
}
