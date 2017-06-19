using UnityEngine;
using UnityEngine.UI;

namespace Crosstales.RTVoice.Demo.Util
{
    /// <summary>Changes the sensitivity of ScrollRects under various platforms.</summary>
    [HelpURL("https://www.crosstales.com/media/data/assets/rtvoice/api/class_crosstales_1_1_r_t_voice_1_1_demo_1_1_util_1_1_scroll_rect_handler.html")]
    public class ScrollRectHandler : MonoBehaviour
    {

        #region Variables

        public ScrollRect Scroll;
        private float WindowsSensitivity = 35f;
        private float MacSensitivity = 25f;

        #endregion

        #region MonoBehaviour methods

        public void Start()
        {
            if (RTVoice.Util.Helper.isWindowsPlatform)
            {
                Scroll.scrollSensitivity = WindowsSensitivity;
            }
            else if (RTVoice.Util.Helper.isMacOSPlatform)
            {
                Scroll.scrollSensitivity = MacSensitivity;
            }
        }

        #endregion
    }
}
// © 2016-2017 crosstales LLC (https://www.crosstales.com)