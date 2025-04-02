using _1Core.Scripts.Game.Map;
using _1Core.Scripts.Game.Player.Bullet;
using _1Core.Scripts.Game.Wall;
using Core.Scripts.Tools.Extensions;
using DG.Tweening;
using Lean.Pool;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace _1Core.Scripts.Game.Player
{
    public class Player : MonoBehaviour
    {
        [Header("Main")] [SerializeField] private Transform _body;
        [SerializeField] private Transform _finishCircle;
        [SerializeField] private SlotAnimation _slotAnimation;
        [SerializeField] private Collider _collider;
        [SerializeField] private ResetModel _resetModel;
        [SerializeField] private BulletFactory bulletFactory;
        [SerializeField] private BossCube.BossCube _bossCube;
        [SerializeField] private MoveType _moveType;

        [Space, Header("UI")] [SerializeField] private GameObject _btnStart;
        [SerializeField] private GameObject _loseUI;
        [SerializeField] private MoneyManager _moneyManager;

        private float _screenCenter, _targetDirection;
        private float _border;
        private GameManager _gameManager;

        private void Start()
        {
            _gameManager = GameManager.instance;
            _screenCenter = Screen.width / 2f;
            _resetModel.Init(_body);
            _border = 2.1f - .5f;
        }

        private void Update()
        {
            if (_moveType == MoveType.Stop) return;
            Movement(_gameManager.GetValue(TypeWall.PlayerSpeed));
            if (Mathf.Abs(_body.position.z - _finishCircle.position.z) <= 0.2f)
            {
                EnableMove(MoveType.Stop);
                return;
            }
#if UNITY_EDITOR
            HandleMouseInput();
#else
            HandleTouchInput();
#endif
            SmoothMoveObject(2);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Wall.Wall wall))
            {
                wall.EnterWall(false);
                return;
            }

            if (other.TryGetComponent(out Trap trap))
            {
                _collider.enabled = false;
                EnableMove(MoveType.Stop);
                _loseUI.SetActive(true);
                _moneyManager.RoundMoney = 0;
                _body.DOScale(Vector3.zero, 1f).SetEase(Ease.InBounce);
                return;
            }

            if (other.gameObject.layer == 3)
            {
                _moneyManager.RoundMoney += Random.Range(10, 50);
                other.enabled = false;
                other.transform.DOScale(Vector3.zero, .7f).SetEase(Ease.OutBounce).OnComplete(() =>
                {
                    LeanPool.Despawn(other);
                });
                return;
            }

            if (other.gameObject.layer != 6) return;
            _collider.enabled = false;
            _moveType = MoveType.Stop;
            _bossCube.Init();
        }

        public void Win()
        {
            EnableMove(MoveType.Stop);
            _body.DOJump(_body.position, 2, 1, 1).SetLoops(-1).SetEase(Ease.Linear);
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.TryGetComponent(out Wall.Wall wall)) return;
            wall.transform.DOScale(Vector3.zero, .5f);
        }

        public void StartMove()
        {
            _gameManager.Level++;
            EnableMove(MoveType.Move);
        }

        [Button]
        public void EnableMove(MoveType moveType)
        {
            _moveType = moveType;
            bulletFactory.EnableFire(_moveType);
        }

        [Button]
        public void Reset()
        {
            _body.DOKill();
            _body.DOScale(Vector3.zero, 1f).SetEase(Ease.InBounce).OnComplete(() =>
            {
                _gameManager.bonusStat.ResetAllStat();
                _resetModel.Reset(_body);
                _body.position = _body.position.SetX(0);
                _body.DOScale(Vector3.one, 1f).SetEase(Ease.InBounce).OnComplete(() =>
                {
                    _btnStart.SetActive(true);
                    _collider.enabled = true;
                });
            });
            _slotAnimation.Reset();
        }

        private void HandleMouseInput()
        {
            if (Input.GetMouseButton(0))
            {
                if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
                {
                    _targetDirection = 0;
                    return;
                }

                Vector2 currentMousePosition = Input.mousePosition;

                _targetDirection = currentMousePosition.x < _screenCenter ? -1 : 1;
            }
            else
            {
                _targetDirection = 0;
            }
        }


        private void HandleTouchInput()
        {
            if (Input.touchCount > 0)
            {
                var touch = Input.GetTouch(0);

                if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                {
                    _targetDirection = 0;
                    return;
                }

                if (touch.phase is TouchPhase.Began or TouchPhase.Stationary or TouchPhase.Moved)
                {
                    _targetDirection = touch.position.x < _screenCenter ? -1 : 1;
                }
            }
            else
            {
                _targetDirection = 0;
            }
        }


        private void SmoothMoveObject(float moveSpeed)
        {
            var moveAmount = _targetDirection * moveSpeed * Time.deltaTime;

            var targetPosition = _body.position + new Vector3(moveAmount, 0, 0);

            targetPosition.x = Mathf.Clamp(targetPosition.x, -_border, _border);

            _body.position = Vector3.MoveTowards(_body.position, targetPosition, moveSpeed * Time.deltaTime);
        }

        private void Movement(float moveSpeed) => _body.position =
            _body.position.SetZ(_body.position.z + Time.deltaTime * moveSpeed);
    }

    public enum MoveType
    {
        Stop,
        Move
    }
}