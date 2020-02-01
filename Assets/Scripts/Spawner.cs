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
        [SerializeField]internal float period;

        [SerializeField] internal float timeToActive;

    }
}
