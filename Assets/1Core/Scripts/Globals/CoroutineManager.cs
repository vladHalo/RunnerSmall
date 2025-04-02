using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace _1Core.Scripts.Globals
{
    public class CoroutineManager : SerializedMonoBehaviour
    {
        public static CoroutineManager instance;

        [SerializeField] private Dictionary<string, CoroutineModel> _coroutines = new();

        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
                return;
            }

            instance = this;
        }

        [Button]
        public void StopAllCoroutinesMono()
        {
            foreach (var coroutineModel in _coroutines.Values)
            {
                coroutineModel.mono.StopAllCoroutines();
            }
        }

        [Button]
        public void StopCoroutineOwner(string nameMono)
        {
            if (_coroutines.TryGetValue(nameMono, out var coroutineModel))
            {
                coroutineModel.mono.StopAllCoroutines();
            }
        }

        public void RegisterCoroutineOwner(string nameMono, MonoBehaviour mono)
        {
            if (!_coroutines.ContainsKey(nameMono))
            {
                _coroutines.Add(nameMono, new CoroutineModel(nameMono, mono));
            }
        }

        public void UnregisterCoroutineOwner(string nameMono)
        {
            _coroutines.Remove(nameMono);
        }

        public void AddCoroutineToOwner(string nameMono, string coroutineName, IEnumerator routine)
        {
            if (_coroutines.TryGetValue(nameMono, out var coroutineModel))
            {
                coroutineModel.coroutines[coroutineName] = routine;
            }
        }

        public void RemoveCoroutineFromOwner(string nameMono, string coroutineName)
        {
            if (_coroutines.TryGetValue(nameMono, out var coroutineModel))
            {
                coroutineModel.coroutines.Remove(coroutineName);
            }
        }

        [Button]
        public void StartNamedCoroutine(string nameMono, string coroutineName)
        {
            if (_coroutines.TryGetValue(nameMono, out var coroutineModel) &&
                coroutineModel.coroutines.TryGetValue(coroutineName, out var coroutine))
            {
                coroutineModel.mono.StartCoroutine(coroutine);
            }
        }

        [Button]
        public void StopNamedCoroutine(string nameMono, string coroutineName)
        {
            if (_coroutines.TryGetValue(nameMono, out var coroutineModel) &&
                coroutineModel.coroutines.TryGetValue(coroutineName, out var coroutine))
            {
                coroutineModel.mono.StopCoroutine(coroutine);
            }
        }
    }

    [Serializable]
    public class CoroutineModel
    {
        [SerializeField] public MonoBehaviour mono;

        [DictionaryDrawerSettings(KeyLabel = "Name", ValueLabel = "Coroutine")]
        public Dictionary<string, IEnumerator> coroutines = new();

        public CoroutineModel(string nameMono, MonoBehaviour mono)
        {
            this.mono = mono;
            coroutines = new Dictionary<string, IEnumerator>();
        }
    }
}