using UnityEngine;

namespace OneDevApp.CustomTabPlugin
{
    public class BrowserTab : MonoBehaviour
    {
        [SerializeField] private string m_unityMainActivity = "com.unity3d.player.UnityPlayer";

        public void Show(string urlToLaunch, string colorCode, string secColorCode, bool showTitle = false, bool showUrlBar = false)
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                using (var javaUnityPlayer = new AndroidJavaClass(m_unityMainActivity))
                {
                    using (var mContext = javaUnityPlayer.GetStatic<AndroidJavaObject>("currentActivity"))
                    {
                        using (AndroidJavaClass jc = new AndroidJavaClass("com.onedevapp.customchrometabs.CustomTabPlugin"))
                        {
                            var mAuthManager = jc.CallStatic<AndroidJavaObject>("getInstance");
                            mAuthManager.Call<AndroidJavaObject>("setActivity", mContext);
                            mAuthManager.Call<AndroidJavaObject>("setUrl", urlToLaunch);
                            mAuthManager.Call<AndroidJavaObject>("setColorString", colorCode);
                            mAuthManager.Call<AndroidJavaObject>("setSecondaryColorString", secColorCode);
                            mAuthManager.Call<AndroidJavaObject>("ToggleShowTitle", showTitle);
                            mAuthManager.Call<AndroidJavaObject>("ToggleUrlBarHiding", showUrlBar);
                            mAuthManager.Call("openCustomTab");
                        }
                    }
                }
            }
        }
    }
}
