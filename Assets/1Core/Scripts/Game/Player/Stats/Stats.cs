using System;
using UnityEngine;

namespace _1Core.Scripts.Game.Player.Stats
{
    [Serializable]
    public abstract class StatBase<T>
    {
        public T CurrentValue;
        [SerializeField] private T _initialValue;
        [SerializeField] protected bool _isSave;

        protected const string SaveSuffix = "Stat";

        public virtual void Load(string statName)
        {
            if (_isSave && ES3.KeyExists(statName + SaveSuffix))
                CurrentValue = ES3.Load<T>(statName + SaveSuffix);
        }

        public void Reset(string statName)
        {
            CurrentValue = _initialValue;
            if (_isSave) ES3.Save(statName + SaveSuffix, CurrentValue);
        }

        public abstract void Modify(T amount, string statName);
    }

    [Serializable]
    public class IntStat : StatBase<int>
    {
        public override void Modify(int amount, string statName)
        {
            CurrentValue += amount;
            if (_isSave) ES3.Save(statName + SaveSuffix, CurrentValue);
        }
    }

    [Serializable]
    public class FloatStat : StatBase<float>
    {
        public override void Modify(float amount, string statName)
        {
            CurrentValue += amount;
            if (_isSave) ES3.Save(statName + SaveSuffix, CurrentValue);
        }
    }
}