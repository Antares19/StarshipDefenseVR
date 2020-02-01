using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Arty
{
    public class Spawner : MonoBehaviour
    {
        internal GameObject spawnObj;
        internal Transform spawnTransform;

        internal bool isActive = false;

        /// <summary>
        /// частота спауна
        /// </summary>
        public float TimeToSpawn;
        /// <summary>
        /// время, через которое
        /// </summary>
        public float TimeToActive;

        internal float currentTimeToSpawn;


        public GameObject ClosestNode;

    }
}
