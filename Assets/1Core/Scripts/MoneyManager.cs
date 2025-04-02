using DG.Tweening;
using TMPro;
using UnityEngine;

namespace _1Core.Scripts
{
    public class MoneyManager : MonoBehaviour
    {
        [SerializeField] private int _money;

        public int Money
        {
            get => _money;
            set
            {
                if (value < 0)
                {
                    MoveTween();
                    return;
                }

                _money = value;
                _moneyText.text = value.ToString();
                ES3.Save(Str.Money, _money);
            }
        }

        [SerializeField] private int _roundMoney;

        public int RoundMoney
        {
            get => _roundMoney;
            set
            {
                _roundMoney = value;
                _roundMoneyText.text = value.ToString();
            }
        }

        [SerializeField] private int _fishka;

        public int Fishka
        {
            get => _fishka;
            set
            {
                _fishka = value;
                _fishkaText.text = value.ToString();
                ES3.Save(Str.Fishka, _fishka);
            }
        }

        [SerializeField] private TextMeshProUGUI _moneyText, _roundMoneyText, _fishkaText;
        [SerializeField] private Transform _panelMoneyText;
        [SerializeField] private GameObject _winPanel;

        private bool _canMoveTween;

        private void OnValidate()
        {
            _moneyText.text = $"{_money}";
        }

        private void Start()
        {
            _canMoveTween = true;
            if (ES3.KeyExists(Str.Money))
            {
                Money = ES3.Load<int>(Str.Money);
            }

            if (ES3.KeyExists(Str.Fishka))
            {
                Fishka = ES3.Load<int>(Str.Fishka);
            }
        }

        public void ShowHideWin(bool enable = true)
        {
            _winPanel.SetActive(enable);
            _money += _roundMoney;
            _roundMoney = 0;
        }

        public void MoveTween()
        {
            if (!_canMoveTween) return;
            _canMoveTween = false;
            _moneyText.color = new Color(0.4f, 0, 0);
            _panelMoneyText.DOScale(new Vector3(1.3f, 1.3f, 1.3f), .4f)
                .OnComplete(() =>
                    {
                        _panelMoneyText.DOScale(Vector3.one, .4f).OnComplete(() => _canMoveTween = true);
                        _moneyText.color = Color.white;
                    }
                );
        }
    }
}