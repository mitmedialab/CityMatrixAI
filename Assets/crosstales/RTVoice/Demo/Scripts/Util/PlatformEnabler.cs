using UnityEngine;

namespace Crosstales.RTVoice.Demo.Util
{
    /// <summary>Enables game objects for a given platform.</summary>
    [HelpURL("https://www.crosstales.com/media/data/assets/rtvoice/api/class_crosstales_1_1_r_t_voice_1_1_demo_1_1_util_1_1_platform_enabler.html")]
    public class PlatformEnabler : MonoBehaviour
    {

        #region Variables

        [Header("Configuration")]
		public System.Collections.Generic.List<Platform> EnabledPlatforms;

        [Header("Active objects")]
        public GameObject[] Objects;

        private Platform currentPlatform;

        #endregion


        #region MonoBehaviour methods

        public void Start()
        {
            Speaker.OnProviderChange += onProviderChange;

            onProviderChange(string.Empty);
        }

        public void Update()
        {

            foreach (GameObject go in Objects)
            {
                go.SetActive(EnabledPlatforms.Contains(currentPlatform));
            }
        }

        public void OnDestroy()
        {
            Speaker.OnProviderChange -= onProviderChange;
        }

        #endregion


        #region Private methods

        private void onProviderChange(string provider)
        {
            if (Speaker.MaryMode)
            {
                currentPlatform = Platform.MaryTTS;
            }
            else if (RTVoice.Util.Helper.isWindowsPlatform)
            {
                currentPlatform = Platform.Windows;
            }
            else if (RTVoice.Util.Helper.isMacOSPlatform)
            {
                currentPlatform = Platform.OSX;
            }
            else if (RTVoice.Util.Helper.isAndroidPlatform)
            {
                currentPlatform = Platform.Android;
            }
            else if (RTVoice.Util.Helper.isIOSPlatform)
            {
                currentPlatform = Platform.IOS;
            }
            else
            {
                currentPlatform = Platform.Unsupported;
            }
        }

        #endregion
    }


    #region Enumeration

    /// <summary>All available platforms.</summary>
    public enum Platform
    {
        OSX,
        Windows,
        IOS,
        Android,
        WSA,
        MaryTTS,
        Unsupported
    }

    #endregion
}
// © 2016-2017 crosstales LLC (https://www.crosstales.com)