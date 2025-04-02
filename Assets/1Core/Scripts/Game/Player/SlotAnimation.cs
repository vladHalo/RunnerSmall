using Core.Scripts.Tools.Extensions;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _1Core.Scripts.Game.Player
{
    public class SlotAnimation : MonoBehaviour
    {
        [Header("Hand"), SerializeField] private float _durationHand = .2f;
        [SerializeField] private float _angleHandX = 100;
        [SerializeField] private Transform _hand;

        [Space, Header("Roll"), SerializeField]
        private float _durationRoll = .7f;

        [SerializeField] private float _angleRollX = 95f;
        [SerializeField] private Transform[] _rolls;

        [Header("VFX")] [SerializeField] private GameObject _superPowerUI;
        [SerializeField] private GameObject _superPowerParticle;

        private int _currentRoll;
        private Sequence _sequence;

        private void Start()
        {
            Shuffle();
        }

        public void Reset()
        {
            Shuffle();
            EnableSuperPower(false);
            _sequence = null;
            _currentRoll = 0;
        }

        [Button]
        public Sequence MoveHand()
        {
            _sequence = DOTween.Sequence()
                .AppendCallback(() =>
                {
                    if (_currentRoll == 0) EnableSuperPower(false);
                    if (_currentRoll == _rolls.Length - 1) EnableSuperPower(true);
                })
                .Append(_hand.DOLocalRotate(Vector3.zero.SetX(_angleHandX), _durationHand).SetLoops(2, LoopType.Yoyo))
                .Append(_rolls[_currentRoll]
                    .DOLocalRotateQuaternion(Quaternion.Euler(_angleRollX, 0, 0), _durationRoll)
                    .SetEase(Ease.OutBounce));
            if (GameManager.instance.isSuperPower) _sequence.AppendInterval(.6f);
            _sequence.AppendCallback(() =>
            {
                _currentRoll++;
                if (_currentRoll < _rolls.Length) return;
                _currentRoll = 0;
                Shuffle();
            });
            return _sequence;
        }

        private void EnableSuperPower(bool enable)
        {
            GameManager.instance.isSuperPower = enable;
            _superPowerUI.SetActive(enable);
            _superPowerParticle.SetActive(enable);
        }

        private void Shuffle()
        {
            _rolls.ForEach(x => x.eulerAngles = x.eulerAngles.SetX(Random.Range(200, 350)));
        }
    }
}