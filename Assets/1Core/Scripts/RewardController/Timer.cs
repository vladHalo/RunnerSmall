using System.Collections;
using UnityEngine;

namespace _1Core.Scripts.RewardController
{
    public class Timer : MonoBehaviour
    {
        [SerializeField] private float _initialTime;

        private float _remainingTime;
        private Coroutine _countdownCoroutine;

        public void StartCountdown()
        {
            _remainingTime = _initialTime;

            if (_countdownCoroutine != null)
                StopCoroutine(_countdownCoroutine);

            _countdownCoroutine = StartCoroutine(CountdownCoroutine());
        }

        private IEnumerator CountdownCoroutine()
        {
            while (_remainingTime > 0)
            {
                yield return new WaitForSeconds(1f);
                _remainingTime--;
                EventManager.TimeUpdated(_remainingTime);
            }

            EventManager.TimerComplete();
        }

        private void OnApplicationPause(bool isPaused)
        {
            if (isPaused)
                SaveManager.SaveTimerState(_remainingTime);
        }

        private void Awake()
        {
            var (remainingTime, hasData) = SaveManager.LoadTimerState();
            if (hasData)
            {
                _remainingTime = remainingTime;
                if (_remainingTime <= 0)
                {
                    EventManager.TimerComplete();
                }
                else
                {
                    StartCountdown();
                }
            }
        }
    }
}