using UnityEngine;

namespace _1Core.Scripts.RewardController
{
    public class CurrencyManager : MonoBehaviour
    {
        [SerializeField] private MoneyManager _moneyManager;

        public void AddCurrency(int amount)
        {
            _moneyManager.Money += amount;
        }
    }
}