using _1Core.Scripts.Game.Player.Bullet;
using Core.Scripts.Tools.Extensions;
using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace _1Core.Scripts.Game.Wall
{
    public class Wall : MonoBehaviour
    {
        [SerializeField] private TypeWall _typeWall;
        [SerializeField] private BoxCollider _boxCollider;
        public MeshRenderer meshRenderer;
        [SerializeField] private float _hp;
        [SerializeField] private TMP_Text _textHp, _textTypeHp;

        private Material[] _materials;
        private GameManager _gameManager;

        private void Start()
        {
            _gameManager = GameManager.instance;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out Bullet bullet)) return;
            ChangeHp(bullet.damage);
            bullet.Touch();
        }

        public void Init(float hp, Material[] materials)
        {
            _hp = hp;
            _materials = materials;
            meshRenderer.material = _materials[(_hp > 0).ToInt()];
            meshRenderer.material = materials[0];
            _typeWall = (TypeWall)Random.Range(0, 5);
            _textHp.text = _hp.ToString("F2");
            _textTypeHp.text = _typeWall.ToString();
            EnterWall(true);
        }

        public void EnterWall(bool enable)
        {
            _boxCollider.enabled = enable;
            if (enable)
            {
                transform.localScale = Vector3.one;
            }
            else
            {
                ActivateBonusState();
                transform.DOScale(Vector3.zero, .5f).SetEase(Ease.OutBounce);
            }
        }

        private void ActivateBonusState()
        {
            if (_typeWall == TypeWall.PlayerSpeed) _hp *= -1;
            _gameManager.bonusStat.ModifyStat(_typeWall.ToString(), _hp);
        }

        [Button]
        private void ChangeHp(float damage)
        {
            if (_gameManager.isSuperPower)
            {
                damage *= 1.5f;
            }

            _hp += damage;
            meshRenderer.material = _materials[(_hp > 0).ToInt()];
            _textHp.text = _hp.ToString("F2");
        }
    }
}