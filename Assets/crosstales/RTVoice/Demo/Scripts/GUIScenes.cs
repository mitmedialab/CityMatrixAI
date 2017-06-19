using UnityEngine;
#if UNITY_5_3 || UNITY_5_3_OR_NEWER
using UnityEngine.SceneManagement;
#endif

namespace Crosstales.RTVoice.Demo
{
    /// <summary>Main GUI scene manager for all demo scenes.</summary>
    [HelpURL("https://www.crosstales.com/media/data/assets/rtvoice/api/class_crosstales_1_1_r_t_voice_1_1_demo_1_1_g_u_i_scenes.html")]
    public class GUIScenes : MonoBehaviour
    {

        #region Variables

        [Tooltip("Name of the previous scene.")]
        public string PreviousScene;

        [Tooltip("Name of the next scene.")]
        public string NextScene;

        public void LoadPrevoiusScene()
        {
            Speaker.Silence();
#if UNITY_5_3 || UNITY_5_3_OR_NEWER
            SceneManager.LoadScene(PreviousScene);
#else
         Application.LoadLevel(PreviousScene);
#endif
        }

        #endregion


        #region Public methods

        public void LoadNextScene()
        {
            Speaker.Silence();
#if UNITY_5_3 || UNITY_5_3_OR_NEWER
            SceneManager.LoadScene(NextScene);
#else
         Application.LoadLevel(NextScene);
#endif
        }

        #endregion
    }
}
// © 2015-2017 crosstales LLC (https://www.crosstales.com)