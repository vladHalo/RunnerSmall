using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace BrowserPluginComponents
{
    public class LoadBar : MonoBehaviour
    {
        [Header("Variables:")]
        [SerializeField] private Image fillImg;
        [SerializeField] private float loadingTime;
        [SerializeField] private bool isLandscapeGame;

        private float _time;

        private Coroutine _loadingCoroutine;

        public void PlayLoadAnimation()
        {
            _loadingCoroutine = StartCoroutine(IE_LoadingAnimation());
        }

        public void PauseLoadAnimation()
        {
            StopCoroutine(_loadingCoroutine);
        }

        private void OpenGame()
        {
            if (isLandscapeGame)
            {
                Screen.autorotateToPortrait = false;
                Screen.autorotateToPortraitUpsideDown = false;
                Screen.autorotateToLandscapeLeft = true;
                Screen.autorotateToLandscapeRight = true;

                Screen.orientation = ScreenOrientation.AutoRotation;
                Screen.orientation = ScreenOrientation.LandscapeLeft;
            }
            else
            {
                Screen.autorotateToPortrait = true;
                Screen.autorotateToPortraitUpsideDown = true;
                Screen.autorotateToLandscapeLeft = false;
                Screen.autorotateToLandscapeRight = false;

                Screen.orientation = ScreenOrientation.AutoRotation;
                Screen.orientation = ScreenOrientation.Portrait;
            }

            SceneManager.LoadScene(1);
        }

        private IEnumerator IE_LoadingAnimation()
        {
            while (_time < loadingTime)
            {
                _time += Time.deltaTime;
                float progress = _time / loadingTime;
                fillImg.fillAmount = progress;
                yield return null;
            }

            OpenGame();
        }
    }
}