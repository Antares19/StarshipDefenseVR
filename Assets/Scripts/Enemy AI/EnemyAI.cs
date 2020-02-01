using System;
using System.Collections.Generic;
using UnityEngine;
public class EnemyAI : MonoBehaviour
{

    [SerializeField] private SpawnerManager _spawner;

    private List<EnemyData> _activeEnemies;
    private Dictionary<GameObject, EnemyData> _buildingsWithAttachedEnemies;//не геймобжект, а билдинг
    private EnemyState _enemyState_Idle;
         
    private void Start()
    {
        _activeEnemies = new List<EnemyData>();

        _enemyState_Idle = new EnemyState_Idle();

        _spawner.OnEnemySpawn += AddEnemyToActiveEnemiesList;
    }

    private void AddEnemyToActiveEnemiesList(Enemy enemyMono)
    {
        _activeEnemies.Add(new EnemyData(enemyMono, _enemyState_Idle));
    }

    private void Update()
    {
        foreach (var enemy in _activeEnemies)
        {
            enemy.CurrentState.Tick(enemy, this);
        }
    }
}

public class EnemyData
{
    public Enemy EnemyMono;
    public Transform Transform;
    public Rigidbody RigidBody;
    public GameObject CurrentTarget;

    public EnemyState CurrentState;

    public EnemyData(Enemy enemyMono, EnemyState initialState)
    {
        EnemyMono = enemyMono;
        Transform = enemyMono.transform;
        RigidBody = enemyMono.GetComponent<Rigidbody>();
        CurrentState = initialState;
    }
}