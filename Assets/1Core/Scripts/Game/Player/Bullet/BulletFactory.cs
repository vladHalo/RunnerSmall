using System.Collections;
using _1Core.Scripts.Game.Wall;
using Core.Scripts.Tools.Extensions;
using DG.Tweening;
using Lean.Pool;
using UnityEngine;

namespace _1Core.Scripts.Game.Player.Bullet
{
    public class BulletFactory : MonoBehaviour
    {
        [SerializeField] private Factory<Bullet> _bulletFactory;
        [SerializeField] private SlotAnimation _slotAnimation;
        [SerializeField] private Transform _aim;

        private Coroutine _coroutine;
        private GameManager _gameManager;

        private void Start()
        {
            _gameManager = GameManager.instance;
        }

        public void EnableFire(MoveType moveType)
        {
            if (moveType == MoveType.Move) _coroutine = StartCoroutine(DelayFire());
            else StopCoroutine(_coroutine);
        }

        private IEnumerator DelayFire()
        {
            while (true)
            {
                _slotAnimation.MoveHand().OnComplete(CreateBullet);
                yield return new WaitForSeconds(
                    _gameManager.GetValue(TypeWall.BulletDelayFire));
            }
        }

        private void CreateBullet()
        {
            var bullet = _bulletFactory.Create(_aim.position, _aim.rotation);
            bullet.Init(_gameManager.selectedElementIndex, _gameManager.isSuperPower.ToInt(),
                _gameManager.GetValue(TypeWall.BulletPower),
                _gameManager.GetValue(TypeWall.BulletSpeed));
            LeanPool.Despawn(bullet, _gameManager.GetValue(TypeWall.BulletTimeLife));
        }
    }
}