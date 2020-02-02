using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ar
{
    public class ScoreCounter : MonoBehaviour
    {
        [SerializeField] private Transform _housePivot;

        private int totalHouses;
        private int currentHouses;

        private void Awake()
        {
            totalHouses = _housePivot.childCount;
            Debug.Log(string.Format("Количество домов на местах: {0}", totalHouses));

            currentHouses = totalHouses;
        }
    }
}
