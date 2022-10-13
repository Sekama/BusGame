using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotScript : MonoBehaviour
{
    public MeshFilter BotMesh;
    public MeshRenderer BotRenderer;
    public PassengerData PasData;

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
        PasData = InData;
        BotMesh.mesh = PasData.BotMesh;
        BotRenderer.material = InData.BotMaterial;
    }
}
