using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _1Core.Scripts.RewardController
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private RewardManager _rewardManager;
        [SerializeField] private Timer _timer;
        [SerializeField] private TextMeshProUGUI _timerText;
        [SerializeField] private TextMeshProUGUI _rewardAmountText;
        [SerializeField] private Button _claimButton;
        [SerializeField] private Button _addCurrencyButton;
        [SerializeField] private Image _claimButtonImage;

        private void Awake()
        {
            _claimButton.onClick.AddListener(OnClaimButtonClicked);
            _addCurrencyButton.onClick.AddListener(OnAddCurrencyButtonClicked);

            EventManager.OnTimeUpdated += UpdateTimerDisplay;
            EventManager.OnTimerComplete += OnTimerComplete;
        }

        private void OnDestroy()
        {
            _claimButton.onClick.RemoveListener(OnClaimButtonClicked);
            _addCurrencyButton.onClick.RemoveListener(OnAddCurrencyButtonClicked);

            EventManager.OnTimeUpdated -= UpdateTimerDisplay;
            EventManager.OnTimerComplete -= OnTimerComplete;
        }

        private void UpdateTimerDisplay(float remainingTime)
        {
            _timerText.text = FormatTime(remainingTime);
        }

        private void OnTimerComplete()
        {
            ToggleClaimButton(true, 1f);
        }

        private void OnClaimButtonClicked()
        {
            _rewardManager.GenerateReward(_rewardAmountText);
            ToggleClaimButton(false, 0.6f);
        }

        private void OnAddCurrencyButtonClicked()
        {
            _rewardManager.ClaimReward();
            _timer.StartCountdown();
        }

        private void ToggleClaimButton(bool isEnabled, float alpha)
        {
            _claimButton.interactable = isEnabled;
            var color = _claimButtonImage.color;
            color.a = alpha;
            _claimButtonImage.color = color;
        }

        private string FormatTime(float seconds)
        {
            TimeSpan time = TimeSpan.FromSeconds(seconds);
            return time.ToString(@"hh\:mm\:ss");
        }
    }
}