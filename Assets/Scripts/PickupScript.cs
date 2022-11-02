using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupScript : MonoBehaviour
{
    [SerializeField] private float _boostAmt;
    [SerializeField] private float _spawnDelay;
    [SerializeField] private float _spawnDelayDelta;
    [SerializeField] private GameObject _pickupLogic;
    // Start is called before the first frame update
    private void Start()
    {
        ShowSelf();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<EnergySystem>().AddEnergy(_boostAmt);
            HideSelf();
        }
    }
    

    private void HideSelf()
    {
        _spawnDelay += _spawnDelayDelta;
        Invoke("ShowSelf", _spawnDelay);
        _pickupLogic.SetActive(false);
    }

    private void ShowSelf()
    {
        _pickupLogic.SetActive(true);
    }
}
