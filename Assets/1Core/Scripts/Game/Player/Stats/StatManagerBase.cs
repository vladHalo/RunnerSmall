using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _1Core.Scripts.Game.Player.Stats
{
    public abstract class StatManagerBase<TStat, TValue> : SerializedMonoBehaviour where TStat : StatBase<TValue>
    {
        [SerializeField] private Dictionary<string, TStat> _stats = new();

        protected void LoadAllStats()
        {
            foreach (var stat in _stats)
            {
                stat.Value.Load(stat.Key);
            }
        }

        public TValue GetStatValue(string statName)
        {
            return _stats.TryGetValue(statName, out var stat) ? stat.CurrentValue : default;
        }

        public void ModifyStat(string statName, TValue amount)
        {
            if (_stats.TryGetValue(statName, out var stat))
            {
                stat.Modify(amount, statName);
            }

            GameManager.instance.UpdateFullState();
        }

        public void ResetStat(string statName)
        {
            if (_stats.TryGetValue(statName, out var stat))
            {
                stat.Reset(statName);
            }

            GameManager.instance.UpdateFullState();
        }

        public void ResetAllStat()
        {
            foreach (var stat in _stats)
            {
                stat.Value.Reset(stat.Key);
            }

            GameManager.instance.UpdateFullState();
        }
    }
}