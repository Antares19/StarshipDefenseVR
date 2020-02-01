using System;
using System.Collections.Generic;
using UnityEngine;
public class EnemyAI : MonoBehaviour
{

    [SerializeField] private SpawnerManager _spawnManager;

    public float EnemySpeedFree;
    public float EnemySpeedCarry;
    public float DistanceToTargetToCountAsReached;
    public float EnemyHangTimeWhenBuildingIsGrabbed;

    private Dictionary<Enemy, EnemyData> _activeEnemies;
    public Dictionary<Building, BuildingData> Buildings { get; private set; }

    public Mover Mover { get; private set;}


    //public EnemyState State_StateChanger { get; private set; }

    public EnemyState State_Idle { get; private set; }
    public EnemyState State_GoingToWaypoint { get; private set; }
    public EnemyState State_GoingToBuilding { get; private set; }
    public EnemyState State_WaitingAtBuilding { get; private set; }
    public EnemyState State_CarryingBuilding { get; private set; }
    public EnemyState State_Hanging { get; private set; }
    public EnemyState State_Falling { get; private set; }

    public float DeltaTime { get; private set; }
    

    private void Awake()
    {
        Mover = new Mover();

        State_Idle = new EnemyState_Idle();
        State_GoingToWaypoint = new EnemyState_GoingToWayPoint();
        State_GoingToBuilding = new EnemyState_GoingToBuilding();
        State_CarryingBuilding = new EnemyState_AttachedToBuildingAndCarrying();
        State_WaitingAtBuilding = new EnemyState_AttachedToBuildingAndWaiting();
        State_Hanging = new EnemyState_Hanging();
        State_Falling = new EnemyState_Falling();
    }

    private void Start()
    {
        _activeEnemies = new Dictionary<Enemy, EnemyData>();
        Buildings = new Dictionary<Building, BuildingData>();

        var allBuildingsInScene = FindObjectsOfType<Building>();
        foreach (var building in allBuildingsInScene)
        {
            AddBuildingToList(building);
        }

        _spawnManager.OnEnemySpawn += AddEnemyToActiveEnemiesList;

    }

    private void Update()
    {
        DeltaTime = Time.deltaTime;

        UpdateEnemies();
        CarryBuildings();
    }

    private void UpdateEnemies()
    {
        foreach (var enemy in _activeEnemies)
        {
            enemy.Value.CurrentState.Tick(enemy.Value, this);
        }
    }

    private void CarryBuildings()
    {
        foreach (var building in Buildings)
        {
            if (building.Value.IsBeingCarried)
            {
                Mover.Move(building.Value.Transform, building.Value.CarryTarget.transform, EnemySpeedCarry * DeltaTime);
            }
        }
    }

    
    public void AddBuildingToList(Building building)
    {
        Buildings.Add(building, new BuildingData());
        building.OnPlayerGrabbedBuilding += HandleBuildingGrabbed;
    }

    public void FindNewTargetNodeForEnemy(EnemyData enemy)
    {
        enemy.CurrentTarget = enemy.CurrentTarget.GetComponent<Node>().getRandomPlayerPath().gameObject;
    }

    public void TurnEnemyToFaceTarget(EnemyData enemy)
    {
        enemy.Transform.LookAt(new Vector3
            (
                enemy.CurrentTarget.transform.position.x,
                enemy.Transform.position.y,
                enemy.CurrentTarget.transform.position.z
            ));
    }

    private void AddEnemyToActiveEnemiesList(Enemy enemyMono, GameObject closestNode)
    {
        _activeEnemies.Add(enemyMono, new EnemyData(enemyMono));
        _activeEnemies[enemyMono].CurrentTarget = closestNode;
        State_GoingToWaypoint.OnStateEnter(_activeEnemies[enemyMono], this);
    }


 

    private void HandleBuildingGrabbed(Building building)
    {
        Buildings[building].StopCarrying();

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

    public float HangTimer;
    public EnemyState CurrentState;

    public EnemyData(Enemy enemyMono)
    {
        EnemyMono = enemyMono;
        Transform = enemyMono.transform;
        RigidBody = enemyMono.GetComponent<Rigidbody>();
        Animator = enemyMono.GetComponent<Animator>();
    }    
}

public class BuildingData
{
    public List<Enemy> DependentEnemies;
    public GameObject CarryTarget { get; private set; }
    public bool IsBeingCarried { get; private set; }
    public Transform Transform { get; private set; }

    public BuildingData()
    {
        DependentEnemies = new List<Enemy>();
    }

    public void StartCarryingToTarget(GameObject target)
    {
        IsBeingCarried = true;
        CarryTarget = target;
    }

    public void StopCarrying()
    {
        IsBeingCarried = false;
        CarryTarget = null;
    }

}