using System;
using TMPro;
using UnityEngine.UI;

namespace Core.Scripts.Views.Models
{
    [Serializable]
    public class ButtonUpgradeModel
    {
        public string nameSave;
        public int step;
        public float value;
        public Button button;
        public TextMeshProUGUI _textUpgrade;

        public Text priceText;
        //public List<Image> images;
    }
}