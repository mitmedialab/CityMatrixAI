using UnityEngine;
using UnityEngine.UI;
using Crosstales.RTVoice.Model;

namespace Crosstales.RTVoice.Demo
{
    /// <summary>Wrapper for the dynamic speakers.</summary>
    [RequireComponent(typeof(AudioSource))]
    [HelpURL("https://www.crosstales.com/media/data/assets/rtvoice/api/class_crosstales_1_1_r_t_voice_1_1_demo_1_1_speak_wrapper.html")]
    public class SpeakWrapper : MonoBehaviour
    {

        #region Variables

        public Voice SpeakerVoice;
        public InputField Input;
        public Text Label;
        public AudioSource Audio;

        #endregion


        #region MonoBehaviour methods

        public void Start()
        {
            Audio = GetComponent<AudioSource>();
        }

        #endregion


        #region Public methods

        public void Speak()
        {
            if (GUISpeech.isNative)
            {
                Speaker.SpeakNative(Input.text, SpeakerVoice, GUISpeech.Rate, GUISpeech.Volume, GUISpeech.Pitch);
            }
            else
            {
                Speaker.Speak(Input.text, Audio, SpeakerVoice, true, GUISpeech.Rate, GUISpeech.Volume, "", GUISpeech.Pitch);
            }
        }

        #endregion
    }
}
// © 2015-2017 crosstales LLC (https://www.crosstales.com)