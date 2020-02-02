using System;
using System.Collections.Generic;
using UnityEngine;
public class EnemyAI : MonoBehaviour
{
    [SerializeField] private SpawnerManager _spawner;

    private Dictionary<Enemy, EnemyData> _activeEnemies;
    public Dictionary<Building, BuildingData> Buildings { get; private set; }

    //public EnemyState State_StateChanger { get; private set; }

    public EnemyState State_WaitingAtBuilding { get; private set; }
    public EnemyState State_CarryingBuilding { get; private set; }
    public EnemyState State_Hanging { get; private set; }
    public EnemyState State_GoingToWaypoint { get; private set; }
    public EnemyState State_GoingToBuilding { get; private set; }

    private void Awake()
    {
        State_GoingToWaypoint = new EnemyState_GoingToWayPoint();
        State_GoingToBuilding = new EnemyState_GoingToBuilding();
        State_CarryingBuilding = new EnemyState_AttachedToBuildingAndCarrying();
        State_WaitingAtBuilding = new EnemyState_AttachedToBuildingAndWaiting();
        State_Hanging = new EnemyState_Hanging();
    }

    private void Start()
    {
        _activeEnemies = new Dictionary<Enemy, EnemyData>();
        Buildings = new Dictionary<Building, BuildingData>();

        //Добавить зданий!!

        _spawner.OnEnemySpawn += AddEnemyToActiveEnemiesList;

    }

    private void Update()
    {
        UpdateEnemies();
        CarryBuildings();
    }

    public void AddBuildingToList(Building building)
    {
        Buildings.Add(building, new BuildingData());
        building.OnPlayerGrabbedBuilding += HandleBuildingGrabbed;
    }

    private void AddEnemyToActiveEnemiesList(Enemy enemyMono)
    {
        _activeEnemies.Add(enemyMono, new EnemyData(enemyMono, State_CarryingBuilding));
    }



    private void CarryBuildings()
    {
        foreach (var building in Buildings)
        {
            if (building.Value.IsBeingCarried)
            {
                //ДВИГАЕМ ДОМ
            }
        }
    }

    private void UpdateEnemies()
    {
        foreach (var enemy in _activeEnemies)
        {
            Debug.Log(enemy + " is in " + enemy.Value.CurrentState);
            enemy.Value.CurrentState.Tick(enemy.Value, this);
        }
    }

    private void HandleBuildingGrabbed(Building building)
    {
        foreach (var dependentEnemy in Buildings[building].DependentEnemies)
        {
            var enemy = _activeEnemies[dependentEnemy];
            if (enemy.CurrentState == State_GoingToBuilding)
            {
                //останавливаются и идут к новой точке
                //_activeEnemies[enemy].CurrentTarget = выбор нода
                //STATE_IDLE.OnEnterState
                Buildings[building].DependentEnemies.Remove(dependentEnemy);
            }
            else if (enemy.CurrentState == State_CarryingBuilding)
            {
                State_Hanging.OnStateEnter(enemy, this);
            }
        }

        //ОТПИСАТЬСЯ?
    }
    
}



public class EnemyData
{
    public Enemy EnemyMono;
    public Transform Transform;
    public Rigidbody RigidBody;
    public GameObject CurrentTarget;
    public Building BuildingAttachedTo;
    public Animator Animator;

    public EnemyState CurrentState;

    public EnemyData(Enemy enemyMono, EnemyState initialState)
    {
        EnemyMono = enemyMono;
        Transform = enemyMono.transform;
        RigidBody = enemyMono.GetComponent<Rigidbody>();
        Animator = enemyMono.GetComponent<Animator>();

        CurrentState = initialState;
    }
}

public class BuildingData
{
    public List<Enemy> DependentEnemies { get; private set; }

    public bool IsBeingCarried { get; internal set; }

    public BuildingData()
    {
        DependentEnemies = new List<Enemy>();
    }

    
}