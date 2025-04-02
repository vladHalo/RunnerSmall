using Sirenix.Utilities;
using UnityEngine;

public class DontDestroyObject : MonoBehaviour
{
    [SerializeField] private GameObject[] _dontDestroyObjects;

    void Start()
    {
        _dontDestroyObjects.ForEach(x => DontDestroyOnLoad(x));
    }
}