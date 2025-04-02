using System.Collections.Generic;
using _1Core.Scripts.Game.Map;
using Core.Scripts.Tools.Extensions;
using Core.Scripts.Tools.Tools;
using Lean.Pool;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _1Core.Scripts.Game.Wall
{
    public class PlatformFactory : MonoBehaviour
    {
        [SerializeField] private Transform _finish;
        [SerializeField] private Transform[] _platforms;

        [SerializeField] private RangeFloat _rangeStep;
        [SerializeField] private RangeFloat _rangeHp;
        [SerializeField] private Material[] _materials;
        [SerializeField] private Factory<Wall> _wallFactory;
        [SerializeField] private Factory<Trap> _trapFactory;
        [SerializeField] private Factory<Collider> _coinFactory;

        private List<Wall> _walls;
        private List<Trap> _traps;
        private List<Collider> _coins;

        private float _step = 24;

        private void Start()
        {
            SetPlatforms();
            _walls = new List<Wall>();
            _traps = new List<Trap>();
            _coins = new List<Collider>();
        }

        [Button, DisableInEditorMode]
        public void CreateWalls()
        {
            LeanPool.DespawnAll();
            _walls.Clear();
            _traps.Clear();
            _coins.Clear();
            var listPlatforms = SetPlatforms();

            var minZ = listPlatforms[1].position.z;
            var maxZ = listPlatforms[^1].position.z;

            while (minZ < maxZ)
            {
                var backMinZ = minZ;
                minZ += _rangeStep.RandomInRange();
                if (minZ > maxZ) break;
                var posRandom = new Vector3(Random.Range(-2.1f, 2.1f), .4f, minZ);
                if (Random.value < .7f)
                {
                    var wall = _wallFactory.Create(posRandom);
                    wall.Init(_rangeHp.RandomInRange(), _materials);
                    _walls.Add(wall);
                    if (!(Mathf.Abs(posRandom.x) > 1) || !(Random.value > .6f)) continue;
                    var trap = _trapFactory.Create(posRandom.SetX(posRandom.x * -1));
                    _traps.Add(trap);
                }
                else
                {
                    var trap = _trapFactory.Create(posRandom);
                    _traps.Add(trap);
                }

                if (!(Random.value > .6f)) continue;
                var coin = _coinFactory.Create(posRandom.SetZ((backMinZ + minZ) / 2));
                coin.enabled = true;
                coin.transform.localScale = Vector3.one;
                _coins.Add(coin);
            }
        }

        private List<Transform> SetPlatforms()
        {
            var indexPlatform = Random.Range(Mathf.CeilToInt(_platforms.Length * .33f), _platforms.Length);
            var listPlatforms = new List<Transform>(indexPlatform);
            for (int i = 0; i < _platforms.Length; i++)
            {
                var res = i < indexPlatform;
                if (res)
                {
                    _platforms[i].position = _platforms[i].position.SetZ(i * _step);
                    listPlatforms.Add(_platforms[i]);
                }

                _platforms[i].gameObject.SetActive(res);
            }

            _finish.position = _finish.position.SetZ(listPlatforms.Count * _step + 5);
            return listPlatforms;
        }
    }

    public enum TypeWall
    {
        BulletSpeed,
        BulletPower,
        BulletTimeLife,
        BulletDelayFire,
        PlayerSpeed
    }
}