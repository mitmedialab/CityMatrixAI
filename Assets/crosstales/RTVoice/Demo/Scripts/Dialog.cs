using UnityEngine;
using System.Collections;
using Crosstales.RTVoice.Model;
using Crosstales.RTVoice.Model.Event;

namespace Crosstales.RTVoice.Demo
{
    /// <summary>Simple dialog system with TTS voices.</summary>
    [HelpURL("https://www.crosstales.com/media/data/assets/rtvoice/api/class_crosstales_1_1_r_t_voice_1_1_demo_1_1_dialog.html")]
    public class Dialog : MonoBehaviour
    {

        #region Variables

        public string Culture = "en";
        public AudioSource AudioPersonA;
        public AudioSource AudioPersonB;
        public GameObject VisualsA;
        public GameObject VisualsB;
        public string[] DialogPersonA;
        public string[] DialogPersonB;
        public string CurrentDialogA = string.Empty;
        public string CurrentDialogB = string.Empty;
        public bool Running = false;
        [Range(0f, 3f)]
        public float RateA = 1f;
        [Range(0f, 3f)]
        public float RateB = 1f;
        [Range(0f, 1f)]
        public float VolumeA = 1f;
        [Range(0f, 1f)]
        public float VolumeB = 1f;

        private string uidSpeakerA;
		private string uidSpeakerB;

        private bool playingA = false;
        private bool playingB = false;

        #endregion


        #region MonoBehaviour methods

        public void Start()
        {
            VisualsA.SetActive(false);
            VisualsB.SetActive(false);

            // Subscribe event listeners
            Speaker.OnSpeakStart += speakStartMethod;
            Speaker.OnSpeakComplete += speakCompleteMethod;
        }

        void OnDestroy()
        {
            // Unsubscribe event listeners
            Speaker.OnSpeakStart -= speakStartMethod;
            Speaker.OnSpeakComplete -= speakCompleteMethod;
        }

        #endregion


        #region Public methods

        public IEnumerator DialogSequence()
        {
            if (!Running)
            {
                Running = true;

                playingA = false;
                playingB = false;

                Voice personA = Speaker.VoiceForCulture(Culture);
                Voice personB = Speaker.VoiceForCulture(Culture, 1);

                int index = 0;

                while (Running && (DialogPersonA != null && index < DialogPersonA.Length) || (DialogPersonB != null && index < DialogPersonB.Length))
                {

                    //Person A
                    VisualsA.SetActive(true);
                    VisualsB.SetActive(false);
                    if (DialogPersonA != null && index < DialogPersonA.Length)
                    {
                        CurrentDialogA = DialogPersonA[index];
                    }

                    uidSpeakerA = Speaker.Speak(CurrentDialogA, AudioPersonA, personA, true, RateA, VolumeA);

                    yield return null;

                    CurrentDialogA = string.Empty;

                    //wait until ready
                    while (!playingA && Running)
                    {
                        yield return null;
                    }

                    //wait until played
                    while (playingA && Running)
                    {
                        yield return null;
                    }

                    if (Running)
                    { //ensure it's still running

                        // Person B
                        VisualsA.SetActive(false);
                        VisualsB.SetActive(true);
                        if (DialogPersonB != null && index < DialogPersonB.Length)
                        {
                            CurrentDialogB = DialogPersonB[index];
                        }

                        uidSpeakerB = Speaker.Speak(CurrentDialogB, AudioPersonB, personB, true, RateB, VolumeB);

                        yield return null;

                        CurrentDialogB = string.Empty;

                        //wait until ready
                        while (!playingB && Running)
                        {
                            yield return null;
                        }

                        //wait until played
                        while (playingB && Running)
                        {
                            yield return null;
                        }
                    }
                    index++;
                }

                VisualsA.SetActive(false);
                VisualsB.SetActive(false);

                Running = false;
            }
        }

        #endregion


        #region Callback methods

        private void speakStartMethod(SpeakEventArgs e)
        {
            if (e.Wrapper.Uid.Equals(uidSpeakerA))
            {
                Debug.Log("Speaker A - Speech start: " + e);
                playingA = true;
            }
            else if (e.Wrapper.Uid.Equals(uidSpeakerB))
            {
                Debug.Log("Speaker B - Speech start: " + e);
                playingB = true;
            }
            else
            {
                Debug.LogWarning("Unknown speaker: " + e);

                Running = false;
            }
        }

        private void speakCompleteMethod(SpeakEventArgs e)
        {
            if (e.Wrapper.Uid.Equals(uidSpeakerA))
            {
                Debug.Log("Speaker A - Speech complete: " + e);
                playingA = false;
            }
            else if (e.Wrapper.Uid.Equals(uidSpeakerB))
            {
                Debug.Log("Speaker B - Speech complete: " + e);
                playingB = false;
            }
            else
            {
                Debug.LogWarning("Unknown speaker: " + e);

                Running = false;
            }
        }

        #endregion
    }
}
// © 2015-2017 crosstales LLC (https://www.crosstales.com)