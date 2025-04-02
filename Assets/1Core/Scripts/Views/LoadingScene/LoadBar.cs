using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Scripts.Views
{
    public class LoadBar : MonoBehaviour
    {
        //[SerializeField] private Slider _progress;
        [SerializeField] private Image _image;
        //[SerializeField] private Gradient _gradient;
        [ShowIf("_image")] [SerializeField] private TextMeshProUGUI _textMesh;
        [ShowIf("_textMesh")] [SerializeField] private string _suffix;

        public void SetProgress(float value)
        {
            _image.fillAmount = value;
            SetValue();
        }

        private void SetValue()
        {
            //_image.color = _gradient.Evaluate(_progress.value);
            if (_textMesh != null)
                _textMesh.text = $"{_image.fillAmount * 100:0}{_suffix}";
        }
    }
}