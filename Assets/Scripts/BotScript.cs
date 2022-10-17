using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotScript : MonoBehaviour
{
    public MeshFilter BotMesh;
    public MeshRenderer BotRenderer;
    public PassengerData PasData;
    public int StopsLeft;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetBot(PassengerData InData)
    {
        BotRenderer.materials = new Material[InData.BotMaterial.Length];
        PasData = InData;
        BotMesh.mesh = PasData.BotMesh;
        BotRenderer.materials = InData.BotMaterial;
        StopsLeft = PasData.NoOfStops;
    }

    public void CallDestroy()
    {
        Invoke("DestroySelf", 2f);
        
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }
}
