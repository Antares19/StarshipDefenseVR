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

        private UI _ui;

        [SerializeField] private float _sessionTime = 120f;
        private float _timeToEnd;
        

        private void Awake()
        {
            totalHouses = _housePivot.childCount;
            Debug.Log(string.Format("Количество домов на местах: {0}", totalHouses));

            currentHouses = totalHouses;

            _ui = FindObjectOfType<UI>();
            _timeToEnd = _sessionTime;
            _ui.SetState(string.Format("{0} / {1}", currentHouses, totalHouses), _timeToEnd);

            Messenger.AddListener(HomeEvent.lostHome, OnHomeLost);
            Messenger.AddListener(HomeEvent.receiveHome, OnHomeReceive);

        }

        private void Update()
        {
            _timeToEnd -= Time.deltaTime;
            _ui.SetState(string.Format("{0} / {1}", currentHouses, totalHouses), _timeToEnd);
        }

        private void OnHomeLost()
        {
            currentHouses--;
        }

        private void  OnHomeReceive()
        {
            currentHouses++;
        }

        private void OnDestroy()
        {
            Messenger.RemoveListener(HomeEvent.lostHome, OnHomeLost);
            Messenger.RemoveListener(HomeEvent.receiveHome, OnHomeReceive);
        }
    }

   
}
