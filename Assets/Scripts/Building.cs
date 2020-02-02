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
        onFire = true;
    }
}
