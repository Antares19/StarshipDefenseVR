using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arty
{
    internal class SpawnManager : MonoBehaviour
    {
       
        private Transform _enemyPool;

        private GameObject[] _enemySpawnersGobj;
        private Spawner[] _enemySpawners;

        private float _deltaTime;

        private void Awake()
        {
            _enemyPool = GameObject.FindGameObjectWithTag("SpawnPool").transform;

            _enemySpawners = FindObjectsOfType<Spawner>();
        }

        private void Update()
        {
            _deltaTime = Time.deltaTime;


        }
    }
}