using System;
using System.Collections.Generic;
using UnityEngine;

using Arty;

public class SpawnerManager : MonoBehaviour
{
    [SerializeField] private Spawner[] _spawners;
    [SerializeField] private int _enemiesMaxCount;
    [SerializeField] private GameObject _prefab;
    [SerializeField] private float _maxRandomDistanceFromSpawnerOnSpawn;

    private Pool<Enemy> _enemyPool;

    //Заспавнившийся враг и ближайший к нему узел пути.
    public event Action<Enemy, GameObject> OnEnemySpawn = delegate { };

    
    private void Start()
    {
        _enemyPool = new Pool<Enemy>(_prefab, _enemiesMaxCount, transform);
    }

    private void Update()
    {
        float deltaTime = Time.deltaTime;
        for (int i = 0; i < _spawners.Length; i++)
        {
            _spawners[i].TimeToActive -= deltaTime;

            if (_spawners[i].TimeToActive <= 0 )
            {                
                Spawn(_spawners[i].transform.position,_spawners[i].GetComponent<Spawner>().ClosestNode);
                _spawners[i].TimeToActive = _spawners[i].TimeToSpawn;
            }
        }
    }

    private void Spawn(Vector3 spawnPosition,GameObject nextNode)
    {
        Enemy enemy = _enemyPool.GetObjectFromPool();
        Debug.Log(enemy);
        if (enemy == null)
            return;

        enemy.transform.position = new Vector3(RandomizeCoordinate(spawnPosition.x), spawnPosition.y, RandomizeCoordinate(spawnPosition.z));
        var meshRenderers = enemy.GetComponentsInChildren<MeshRenderer>();
        foreach (var renderer in meshRenderers)
        {
            renderer.enabled = true;
        }

        //enemy.setMoveNode(nextNode);
        
        OnEnemySpawn.Invoke(enemy, nextNode);
    }

    private float RandomizeCoordinate(float coordinate)
    {
        return UnityEngine.Random.Range(coordinate - _maxRandomDistanceFromSpawnerOnSpawn, coordinate + _maxRandomDistanceFromSpawnerOnSpawn);
    }

}
