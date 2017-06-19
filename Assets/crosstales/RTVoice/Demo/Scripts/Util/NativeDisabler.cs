using UnityEngine;

namespace Crosstales.RTVoice.Demo.Util
{
    /// <summary>Disable game objects for native mode.</summary>
    [HelpURL("https://www.crosstales.com/media/data/assets/rtvoice/api/class_crosstales_1_1_r_t_voice_1_1_demo_1_1_util_1_1_native_disabler.html")]
    public class NativeDisabler : MonoBehaviour
    {

        public GameObject[] Objects;

        public void Update()
        {
            foreach (GameObject go in Objects)
            {
                go.SetActive(!GUISpeech.isNative);
            }
        }
    }
}
// © 2016-2017 crosstales LLC (https://www.crosstales.com)