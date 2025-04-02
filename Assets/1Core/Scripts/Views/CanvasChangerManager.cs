using System.Collections;
using System.Collections.Generic;
using Core.Scripts.Views.Enums;
using Core.Scripts.Views.Models;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Sirenix.Utilities;

namespace Core.Scripts.Views
{
    public class CanvasChangerManager : MonoBehaviour
    {
        [SerializeField] private bool _isLoad;
        [SerializeField] private float _delay = .2f, _durationFade = .3f;
        [SerializeField] private List<CanvasModel> _canvasModels;

        private CanvasGroup _lastCanvas;
        private WaitForSecondsRealtime _waitForSecondsRealtime;

        private void Start()
        {
            _waitForSecondsRealtime = new WaitForSecondsRealtime(_delay);

            foreach (var i in _canvasModels)
            {
                if (i.canvasGroup == null) continue;

                i.canvasGroup.alpha = 0;
                i.canvasGroup.gameObject.SetActive(false);
            }

            _canvasModels.ForEach((item, index) =>
            {
                if (item.button != null)
                    item.button.onClick.AddListener(() => ShowHideCanvas(index));
            });

            if (_isLoad)
            {
                LoadingManager.Instantiate.OnFinishLoad.AddListener(() =>
                    StartCoroutine(ProcessCanvasGroup(_canvasModels[0].canvasGroup, 0f, 1f, true)));
            }
            else
            {
                _canvasModels[0].canvasGroup.alpha = 1;
                _canvasModels[0].canvasGroup.gameObject.SetActive(true);
            }

            _lastCanvas = _canvasModels[0].canvasGroup;
        }

        private void OnDestroy()
        {
            _canvasModels.ForEach(item =>
            {
                if (item.button != null)
                    item.button.onClick.RemoveAllListeners();
            });
        }

        public void AddListener(Button button, CanvasGroup canvasGroup)
        {
            _canvasModels.Add(new CanvasModel(button, canvasGroup));
            button.onClick.AddListener(() => ShowHideCanvas(_canvasModels.Count - 1));
        }

        public void AddListener(Button button, CanvasGroup canvasGroup, UnityAction action)
        {
            _canvasModels.Add(new CanvasModel(button, canvasGroup));
            button.onClick.AddListener(action);
        }

        public void ShowHideCanvas(CanvasType type)
        {
            int index = (int)type;
            ShowHideCanvas(index);
        }

        public void ShowHideCanvas(int index) => StartCoroutine(ShowHide(index));

        private IEnumerator ShowHide(int index)
        {
            yield return StartCoroutine(ProcessCanvasGroup(_lastCanvas, 1f, 0f, false));
            if (_canvasModels[index].canvasGroup != null)
            {
                StartCoroutine(ProcessCanvasGroup(_canvasModels[index].canvasGroup, 0f, 1f, true));
                _lastCanvas = _canvasModels[index].canvasGroup;
            }
        }

        private IEnumerator ProcessCanvasGroup(CanvasGroup canvasGroup, float initAlpha, float alpha, bool enable)
        {
            canvasGroup.alpha = initAlpha;
            if (enable) canvasGroup.gameObject.SetActive(true);
            yield return _waitForSecondsRealtime;
            yield return canvasGroup.DOFade(alpha, _durationFade).SetUpdate(true);
            yield return _waitForSecondsRealtime;
            canvasGroup.gameObject.SetActive(enable);
        }
    }
}