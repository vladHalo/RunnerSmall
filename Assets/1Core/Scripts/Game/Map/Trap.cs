using Core.Scripts.Tools.Extensions;
using UnityEngine;

namespace _1Core.Scripts.Game.Map
{
    public class Trap : MonoBehaviour
    {
        [SerializeField] private GameObject[] _types;

        private void OnEnable()
        {
            _types.ForEach(x => x.SetActive(false));
            _types.Random().SetActive(true);
        }
    }
}