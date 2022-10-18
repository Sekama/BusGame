using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BatteryScript : MonoBehaviour
{
    [SerializeField] private EnergySystem _es;
    private Slider _slider;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
        _es.EnergyChanged += UpdateEnergy;
    }

    private void Start()
    {
        UpdateEnergy();
    }

    public void UpdateEnergy()
    {
        _slider.value = _es.Energy;
    }
}
