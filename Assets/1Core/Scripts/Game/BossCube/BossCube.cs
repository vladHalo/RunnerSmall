using _1Core.Scripts.Game.Player.Bullet;
using DG.Tweening;
using Lean.Pool;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _1Core.Scripts.Game.BossCube
{
    public class BossCube : MonoBehaviour
    {
        [SerializeField] private BoxCollider _boxCollider;
        [SerializeField] private MoneyManager _moneyManager;
        [SerializeField] private ParticleSystem _dieParticle;
        [SerializeField] private BossCubeTimer _bossCubeTimer;
        [SerializeField] private GameObject _confetty;
        [SerializeField] private Factory<Transform> _fishkaFactory;

        private GameManager _gameManager;

        private void Start()
        {
            _bossCubeTimer.OnFinishTimer.AddListener(Die);
        }

        [Button]
        public void Init()
        {
            gameObject.SetActive(true);
            _bossCubeTimer.StartTimer();
            _boxCollider.enabled = true;
            transform.DOKill();
            transform.DOScale(new Vector3(5, 5, 5), 2).SetEase(Ease.InOutBounce);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out Bullet bullet)) return;
            bullet.Touch();
            Touch();
            var fishka = _fishkaFactory.Create(transform.position + transform.localScale / 2);
            fishka.localScale = Vector3.one;
            fishka.DOMoveY(fishka.position.y + 2, 1).SetEase(Ease.Linear)
                .OnComplete(() => fishka.DOScale(Vector3.zero, .3f).SetEase(Ease.InOutBounce)
                    .OnComplete(() => { LeanPool.Despawn(fishka); }));
        }

        [Button]
        public void Touch()
        {
            transform.DOShakeScale(1, 2);
            _moneyManager.Fishka++;
        }

        [Button]
        public void Die()
        {
            _boxCollider.enabled = false;
            transform.DOKill();
            transform.DOScale(Vector3.zero, 2).SetEase(Ease.InOutBounce).OnComplete(() =>
            {
                gameObject.SetActive(false);
                _dieParticle.Play();
                DOVirtual.DelayedCall(1.5f, () =>
                {
                    _moneyManager.ShowHideWin();
                    _confetty.SetActive(true);
                });
            });
        }
    }
}