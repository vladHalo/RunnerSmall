using System;
using System.Collections.Generic;
using Lean.Pool;
using UnityEngine;

[Serializable]
public class Factory<T> where T : Component
{
    [SerializeField] private Transform _parent;
    [SerializeField] private List<T> _prefab;
    public int Count => _prefab.Count;


    public T Create(Vector3 position, Quaternion quaternion)
    {
        return LeanPool.Spawn(_prefab[0], position, quaternion, _parent);
    }

    public T Create(int indexPrefab, Vector3 position, Quaternion quaternion)
    {
        return LeanPool.Spawn(_prefab[indexPrefab], position, quaternion, _parent);
    }

    public T Create(Vector3 position)
    {
        return LeanPool.Spawn(_prefab[0], position, _prefab[0].transform.rotation, _parent);
    }

    public T Create(int indexPrefab, Vector3 position)
    {
        return LeanPool.Spawn(_prefab[indexPrefab], position, _prefab[indexPrefab].transform.rotation, _parent);
    }
}