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

    public event Action<Enemy> OnEnemySpawn = delegate { };

    // Start is called before the first frame update
    private void Start()
    {
        _enemyPool = new Pool<Enemy>(_prefab, _enemiesMaxCount, transform);
    }

    // Update is called once per frame
    void Update()
    {
        float deltaTime = Time.deltaTime;
        for (int i = 0; i < _spawners.Length; i++)
        {
            _spawners[i].TimeToActive -= deltaTime;

            if (_spawners[i].TimeToActive <= 0 )
            {
                Spawn(_spawners[i].transform.position);
                _spawners[i].TimeToActive = _spawners[i].TimeToSpawn;
            }
        }
    }

    private void Spawn(Vector3 spawnPosition)
    {
        Enemy enemy = _enemyPool.GetObjectFromPool();
        if (enemy == null)
            return;

        enemy.transform.position = new Vector3(RandomizeCoordinate(spawnPosition.x), spawnPosition.y, RandomizeCoordinate(spawnPosition.z));
        enemy.GetComponent<MeshRenderer>().enabled = true;

        OnEnemySpawn.Invoke(enemy);
    }

    private float RandomizeCoordinate(float coordinate)
    {
        return UnityEngine.Random.Range(coordinate - _maxRandomDistanceFromSpawnerOnSpawn, coordinate + _maxRandomDistanceFromSpawnerOnSpawn);
    }

}
