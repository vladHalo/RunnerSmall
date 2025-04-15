using System;
using UnityEngine;

namespace BrowserPluginComponents
{
    public class ActionTimer : MonoBehaviour
    {
        [SerializeField] private string date;

        private DateTime _currentTime;

        public bool GetTimerStatus()
        {
            if (DateTime.TryParseExact(date, "yyyy-MM-dd HH:mm:ss", null, System.Globalization.DateTimeStyles.None, out _currentTime))
            {
                if (DateTime.Now >= _currentTime)
                {
                    return true;
                }
            }

            return false;
        }
    }
}