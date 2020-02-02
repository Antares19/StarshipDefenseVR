using System;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    private int _numberOfEnemiesNeededToCarry = 2;
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


    ParticleSystem ps;


    public void Start()
    {
        ps = GetComponentInChildren<ParticleSystem>();

        ps.Stop();
    }
    public void SetOnFire()
    {
        Debug.Log("FIRE_STARTED");
        onFire = true;


        

    }


    public void Update()
    {
        //if (UnityEngine.Random.Range(0, 100) > 98 && !onFire)
        //    SetOnFire();

        
        if (onFire)
        {
            //Gizmos.color = Color.white;
            //Debug.DrawRay(transform.position, new Vector3(2, 2, 2));

            if (ps.isStopped)
            {
                ps.Play();
            }


            if (transform.position.y< 0.1)
            {
                onFire = false;
            }
        }


        else
        {
            if (!ps.isStopped)
            {
                ps.Stop();
            }

        }



    }
}
