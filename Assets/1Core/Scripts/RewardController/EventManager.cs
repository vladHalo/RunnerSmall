using System;
using UnityEngine;

namespace _1Core.Scripts.RewardController
{
    public class EventManager : MonoBehaviour
    {
        public static event Action<float> OnTimeUpdated;
        public static event Action OnTimerComplete;

        public static void TimeUpdated(float time) => OnTimeUpdated?.Invoke(time);
        public static void TimerComplete() => OnTimerComplete?.Invoke();
    }
}