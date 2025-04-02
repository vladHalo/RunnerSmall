using TMPro;
using UnityEngine;

namespace _1Core.Scripts.RewardController
{
    public class RewardManager : MonoBehaviour
    {
        [SerializeField] private CurrencyManager _currencyManager;
        private int _currentReward;

        public void GenerateReward(TMP_Text rewardText)
        {
            _currentReward = Random.Range(10, 101);
            rewardText.text = $"{_currentReward}";
            EventManager.TimeUpdated(_currentReward);
        }

        public void ClaimReward()
        {
            _currencyManager.AddCurrency(_currentReward);
        }
    }
}