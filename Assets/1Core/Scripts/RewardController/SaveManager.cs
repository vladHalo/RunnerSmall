using System;
using UnityEngine;

namespace _1Core.Scripts.RewardController
{
    public class SaveManager
    {
        private const string LastExitTimeKey = "LastExitTime";
        private const string RemainingTimeKey = "RemainingTime";

        public static void SaveTimerState(float remainingTime)
        {
            PlayerPrefs.SetString(LastExitTimeKey, DateTime.UtcNow.ToString());
            PlayerPrefs.SetFloat(RemainingTimeKey, remainingTime);
        }

        public static (float remainingTime, bool hasData) LoadTimerState()
        {
            if (PlayerPrefs.HasKey(LastExitTimeKey) && PlayerPrefs.HasKey(RemainingTimeKey))
            {
                DateTime lastExitTime = DateTime.Parse(PlayerPrefs.GetString(LastExitTimeKey));
                float savedTime = PlayerPrefs.GetFloat(RemainingTimeKey);

                TimeSpan timePassed = DateTime.UtcNow - lastExitTime;
                float remainingTime = savedTime - (float)timePassed.TotalSeconds;

                return (remainingTime, true);
            }

            return (0, false);
        }
    }
}