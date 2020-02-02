using System;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] private int _numberOfEnemiesNeededToCarry;
    public int NumberOfEnemiesNeededToCarry { get { return _numberOfEnemiesNeededToCarry; } }

    public event Action<Building> OnPlayerGrabbedBuilding;

    public void GrabBuilding()
    {
        OnPlayerGrabbedBuilding.Invoke(this);
    }

    public bool isOnFire()
    {
        return onFire;
    }

    public bool isPicked()
    {
        return picked;
    }


    bool picked = false;
    bool onFire = false;
    public void SetOnFire()
    {
        Debug.Log("FIRE_STARTED");
        onFire = true;
    }


    public void Update()
    {
        if (UnityEngine.Random.Range(0, 100) > 98 && !onFire)
            SetOnFire();

        
        if (onFire)
        {
            Gizmos.color = Color.white;
            Debug.DrawRay(transform.position, new Vector3(2, 2, 2));


            if (transform.position.y< 0.1)
            {
                onFire = false;
            }
        }
            


    }
}
