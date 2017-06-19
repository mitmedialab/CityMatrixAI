using UnityEngine;
using UnityEngine.UI;
using Crosstales.RTVoice.Model.Event;

namespace Crosstales.RTVoice.Demo
{
    /// <summary>Simple TTS example.</summary>
    [HelpURL("https://www.crosstales.com/media/data/assets/rtvoice/api/class_crosstales_1_1_r_t_voice_1_1_demo_1_1_simple.html")]
    public class Simple : MonoBehaviour
    {

        #region Variables

        public AudioSource SourceA;
        public AudioSource SourceB;

        public Text TextSpeakerA;
        public Text TextSpeakerB;

        public Text PhonemeSpeakerA;
        public Text PhonemeSpeakerB;

        public Text VisemeSpeakerA;
        public Text VisemeSpeakerB;

        [Range(0f, 3f)]
        public float RateSpeakerA = 1.25f;
        [Range(0f, 3f)]
        public float RateSpeakerB = 1.75f;

        public bool PlayOnStart = false;

		private string uidSpeakerA;
		private string uidSpeakerB;

        private string textA;
        private string textB;

        #endregion


        #region MonoBehaviour methods

        public void Start()
        {
            // Subscribe event listeners
            Speaker.OnSpeakAudioGenerationStart += speakAudioGenerationStartMethod;
            Speaker.OnSpeakAudioGenerationComplete += speakAudioGenerationCompleteMethod;
            Speaker.OnSpeakCurrentWord += speakCurrentWordMethod;
            Speaker.OnSpeakCurrentPhoneme += speakCurrentPhonemeMethod;
            Speaker.OnSpeakCurrentViseme += speakCurrentVisemeMethod;
            Speaker.OnSpeakStart += speakStartMethod;
            Speaker.OnSpeakComplete += speakCompleteMethod;

            textA = TextSpeakerA.text;
            textB = TextSpeakerB.text;

            if (PlayOnStart)
            {
                Play();
            }
        }

        public void OnDestroy()
        {
            // Unsubscribe event listeners
            Speaker.OnSpeakAudioGenerationStart -= speakAudioGenerationStartMethod;
            Speaker.OnSpeakAudioGenerationComplete -= speakAudioGenerationCompleteMethod;
            Speaker.OnSpeakCurrentWord -= speakCurrentWordMethod;
            Speaker.OnSpeakCurrentPhoneme -= speakCurrentPhonemeMethod;
            Speaker.OnSpeakCurrentViseme -= speakCurrentVisemeMethod;
            Speaker.OnSpeakStart -= speakStartMethod;
            Speaker.OnSpeakComplete -= speakCompleteMethod;
        }

        #endregion


        #region Public methods

        public void Play()
        {
            TextSpeakerA.text = textA;
            TextSpeakerB.text = textB;

            //usedGuids.Clear();

            SpeakerA(); //start with speaker A
        }

        public void SpeakerA()
        { //Don't speak the text immediately
            uidSpeakerA = Speaker.Speak(TextSpeakerA.text, SourceA, Speaker.VoiceForCulture("en"), false, RateSpeakerA);
        }

        public void SpeakerB()
        { //Don't speak the text immediately
            uidSpeakerB = Speaker.Speak(TextSpeakerB.text, SourceB, Speaker.VoiceForCulture("en", 1), false, RateSpeakerB);
        }

        public void Silence()
        {
            Speaker.Silence();
            SourceA.Stop();
            SourceB.Stop();

            TextSpeakerA.text = textA;
            TextSpeakerB.text = textB;
            VisemeSpeakerB.text = PhonemeSpeakerB.text = VisemeSpeakerA.text = PhonemeSpeakerA.text = "-";
        }

        #endregion


        #region Callback methods

        private void speakAudioGenerationStartMethod(SpeakEventArgs e)
        {
            Debug.Log("speakAudioGenerationStartMethod: " + e);
        }

        private void speakAudioGenerationCompleteMethod(SpeakEventArgs e)
        {
            Debug.Log("speakAudioGenerationCompleteMethod: " + e);

            Speaker.SpeakMarkedWordsWithUID(e.Wrapper);
        }

        private void speakStartMethod(SpeakEventArgs e)
        {
            if (e.Wrapper.Uid.Equals(uidSpeakerA))
            {
                Debug.Log("Speaker A - Speech start: " + e);
            }
            else if (e.Wrapper.Uid.Equals(uidSpeakerB))
            {
                Debug.Log("Speaker B - Speech start: " + e);
            }
            else
            {
                Debug.LogWarning("Unknown speaker: " + e);
            }
        }

        private void speakCompleteMethod(SpeakEventArgs e)
        {
            if (e.Wrapper.Uid.Equals(uidSpeakerA))
            {
                Debug.Log("Speaker A - Speech complete: " + e);
                TextSpeakerA.text = e.Wrapper.Text;
                VisemeSpeakerA.text = PhonemeSpeakerA.text = "-";

                SpeakerB();
            }
            else if (e.Wrapper.Uid.Equals(uidSpeakerB))
            {
                Debug.Log("Speaker B - Speech complete: " + e);
                TextSpeakerB.text = e.Wrapper.Text;

                VisemeSpeakerB.text = PhonemeSpeakerB.text = "-";
            }
            else
            {
                Debug.LogWarning("Unknown speaker: " + e);
            }
        }

        private void speakCurrentWordMethod(CurrentWordEventArgs e)
        {
            //Debug.Log(speechTextArray [wordIndex]);

            if (e.Wrapper.Uid.Equals(uidSpeakerA))
            {
                TextSpeakerA.text = RTVoice.Util.Helper.MarkSpokenText(e.SpeechTextArray, e.WordIndex);
            }
            else if (e.Wrapper.Uid.Equals(uidSpeakerB))
            {
                TextSpeakerB.text = RTVoice.Util.Helper.MarkSpokenText(e.SpeechTextArray, e.WordIndex);
            }
            else
            {
                Debug.LogWarning("Unknown speaker: " + e);
            }
        }

        private void speakCurrentPhonemeMethod(CurrentPhonemeEventArgs e)
        {
            if (e.Wrapper.Uid.Equals(uidSpeakerA))
            {
                PhonemeSpeakerA.text = e.Phoneme;
            }
            else if (e.Wrapper.Uid.Equals(uidSpeakerB))
            {
                PhonemeSpeakerB.text = e.Phoneme;
            }
            else
            {
                Debug.LogWarning("Unknown speaker: " + e);
            }
        }

        private void speakCurrentVisemeMethod(CurrentVisemeEventArgs e)
        {
            if (e.Wrapper.Uid.Equals(uidSpeakerA))
            {
                VisemeSpeakerA.text = e.Viseme;
            }
            else if (e.Wrapper.Uid.Equals(uidSpeakerB))
            {
                VisemeSpeakerB.text = e.Viseme;
            }
            else
            {
                Debug.LogWarning("Unknown speaker: " + e);
            }
        }

        #endregion
    }
}
// © 2015-2017 crosstales LLC (https://www.crosstales.com)