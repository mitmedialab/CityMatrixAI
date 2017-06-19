using UnityEngine;
using Crosstales.RTVoice.Model.Event;

namespace Crosstales.RTVoice.Demo
{
    /// <summary>Simple example with native audio for exact timing.</summary>
    [HelpURL("https://www.crosstales.com/media/data/assets/rtvoice/api/class_crosstales_1_1_r_t_voice_1_1_demo_1_1_native_audio.html")]
    public class NativeAudio : MonoBehaviour
    {

        #region Variables

        public string SpeechText = "This is an example with native audio for exact timing (e.g. animations).";
        public bool PlayOnStart = false;
        public float Delay = 1f;

        #endregion


        #region MonoBehaviour methods

        public void Start()
        {
            // Subscribe event listeners
            Speaker.OnSpeakStart += play;
            Speaker.OnSpeakComplete += stop;

            if (PlayOnStart)
            {
                Invoke("StartTTS", Delay); //Invoke the TTS-system after x seconds
            }
        }

        public void OnDestroy()
        {
            // Unsubscribe event listeners
            Speaker.OnSpeakStart -= play;
            Speaker.OnSpeakComplete -= stop;
        }

        #endregion


        #region Public methods

        public void StartTTS()
        {
            Speaker.SpeakNative(SpeechText, Speaker.VoiceForCulture("en", 1));
        }

        public void Silence()
        {
            Speaker.Silence();
        }

        #endregion


        #region Callback methods

        private void play(SpeakEventArgs e)
        {
            Debug.Log("Play your animations to the event: " + e);

            //Here belongs your stuff, like animations
        }

        private void stop(SpeakEventArgs e)
        {
            Debug.Log("Stop your animations from the event: " + e);

            //Here belongs your stuff, like animations
        }

        #endregion

    }
}
// © 2015-2017 crosstales LLC (https://www.crosstales.com)