using UnityEngine;
using UnityEngine.UI;
#if UNITY_5_3 || UNITY_5_3_OR_NEWER
using UnityEngine.SceneManagement;
#endif
using Crosstales.RTVoice.Util;

namespace Crosstales.RTVoice.Demo
{
    /// <summary>Main GUI component for all demo scenes.</summary>
    [HelpURL("https://www.crosstales.com/media/data/assets/rtvoice/api/class_crosstales_1_1_r_t_voice_1_1_demo_1_1_g_u_i_main.html")]
    public class GUIMain : MonoBehaviour
    {

        #region Variables

        public Text Version;
        public Text Scene;
        public GameObject NoVoices;
        public Text Errors;

        #endregion


        #region MonoBehaviour methods

        public void Start()
        {
            Speaker.OnErrorInfo += errorInfoMethod;

            if (Version != null)
            {
                Version.text = Constants.ASSET_NAME + " - " + Constants.ASSET_VERSION;
            }

            if (Scene != null)
            {
#if UNITY_5_3 || UNITY_5_3_OR_NEWER
                Scene.text = SceneManager.GetActiveScene().name;
#else
            Scene.text = Application.loadedLevelName;
#endif
            }

            if (Speaker.Voices.Count > 0 && NoVoices != null)
            {
                NoVoices.SetActive(false);
            }

            if (Errors != null)
            {
                Errors.text = string.Empty;
            }
        }

        public void Update()
        {
            Cursor.visible = true;
        }

        public void OnDestroy()
        {
            Speaker.OnErrorInfo -= errorInfoMethod;
        }

        #endregion


        #region Public methods

        public void OpenAssetURL()
        {
            Application.OpenURL(Constants.ASSET_CT_URL);
        }

        public void OpenCTURL()
        {
            Application.OpenURL(Constants.ASSET_AUTHOR_URL);
        }

        public void Silence()
        {
            Speaker.Silence();
        }

        public void Quit()
        {
            if (Application.isEditor)
            {
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#endif
            }
            else
            {
                Application.Quit();
            }
        }

        #endregion


        #region Callbacks

		private void errorInfoMethod(Model.Event.SpeakEventArgs e, string errorInfo)
        {
            if (Errors != null)
            {
                Errors.text = errorInfo;
            }
        }

        #endregion

    }
}
// © 2015-2017 crosstales LLC (https://www.crosstales.com)