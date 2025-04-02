using System;
using System.Collections;
using Core.Scripts.Tools.Tools;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace _1Core.Scripts.Game.BossCube
{
    public class BossCubeTimer : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _timerTime;

        private float _step = .01f;

        private float _time;

        private float Time
        {
            get => _time;
            set
            {
                _time = value;
                _timerTime.text = _time.ToString("F2");
                if (_time <= 0) _timerTime.gameObject.SetActive(false);
            }
        }

        public UnityEvent OnFinishTimer;

        public void StartTimer()
        {
            Time = 10f;
            _timerTime.gameObject.SetActive(true);
            StartCoroutine(TimerWork());
        }

        private IEnumerator TimerWork()
        {
            while (Time > 0)
            {
                Time -= _step;
                yield return new WaitForSeconds(_step);
            }

            Time = 0;
            OnFinishTimer?.Invoke();
        }
    }
}