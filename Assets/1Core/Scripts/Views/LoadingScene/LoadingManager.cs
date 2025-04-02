using System;
using System.Collections;
using Core.Scripts.Tools.Extensions;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

#pragma warning disable 0649
namespace Core.Scripts.Views
{
    public class LoadingManager : MonoBehaviour
    {
        [SerializeField] private float _fadeDuration = .5f;
        [SerializeField] private LoadBar _loadBar;
        [SerializeField] private Canvas _canvas;
        [SerializeField] private CanvasGroup _canvasGroup;

        private string _nameDisableScene;
        
        public static LoadingManager Instantiate;

        public UnityEvent OnFinishLoad;

        private void Awake()
        {
            if (Instantiate == null)
            {
                Instantiate = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        [Button]
        public void LoadScene(string sceneName)
        {
            _nameDisableScene = SceneManager.GetActiveScene().name;
            SafeInvoke(loadBar => loadBar.SetProgress(0));
            _canvas.sortingOrder = 100;
            StartCoroutine(Load(sceneName));
        }

        private IEnumerator Load(string sceneName)
        {
            yield return StartCoroutine(Fade(0, 100));
            yield return StartCoroutine(RunLoad(sceneName));
            StartCoroutine(Fade(1, 0));
        }

        private IEnumerator Fade(float valueFade, int indexSortOrder)
        {
            _canvasGroup.alpha = valueFade;
            _canvasGroup.gameObject.SetActive(true);

            _canvasGroup.DOFade(valueFade.OppositeZero(), _fadeDuration).SetUpdate(true);
            yield return new WaitForSecondsRealtime(_fadeDuration);

            _canvas.sortingOrder = indexSortOrder;
        }

        private IEnumerator RunLoad(string sceneName)
        {
            yield return new WaitForSecondsRealtime(.05f);

            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            asyncLoad.allowSceneActivation = false;

            while (asyncLoad.progress < .9f)
            {
                SafeInvoke(loadBar => loadBar.SetProgress(asyncLoad.progress));
                yield return null;
            }

            yield return new WaitForSecondsRealtime(.05f);

            SafeInvoke(loadBar => loadBar.SetProgress(.9f));
            asyncLoad.allowSceneActivation = true;
            while (!asyncLoad.isDone)
            {
                yield return null;
            }

            SafeInvoke(loadBar => loadBar.SetProgress(1));
            yield return new WaitForSecondsRealtime(.25f);
            //codeInstance while (!LevelManager.instance.started) yield return null;
            Time.timeScale = 1;
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
            SceneManager.UnloadSceneAsync(_nameDisableScene);
            OnFinishLoad?.Invoke();
            _canvas.gameObject.SetActive(false);
        }

        private void SafeInvoke(Action<LoadBar> action)
        {
            if (_loadBar != null)
            {
                action(_loadBar);
            }
            else
            {
                Debug.LogWarning("LoadBar is not assigned.");
            }
        }
    }
}