using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeBuilding : MonoBehaviour
{


    Transform startPos;


    public bool IsHolding()
    {
        return false;
    }

    private bool onBase;
    public bool OnBase()
    {
        return onBase;
    }

    // Start is called before the first frame update
    void Start()
    {
        onBase = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
