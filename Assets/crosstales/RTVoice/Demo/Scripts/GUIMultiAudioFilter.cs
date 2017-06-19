using UnityEngine;
using UnityEngine.UI;

namespace Crosstales.RTVoice.Demo
{
    /// <summary>Simple GUI for audio filters on multiple objects.</summary>
    [HelpURL("https://www.crosstales.com/media/data/assets/rtvoice/api/class_crosstales_1_1_r_t_voice_1_1_demo_1_1_g_u_i_multi_audio_filter.html")]
    public class GUIMultiAudioFilter : MonoBehaviour
    {

        #region Variables

		public System.Collections.Generic.List<AudioSource> Sources = new System.Collections.Generic.List<AudioSource>();

		public System.Collections.Generic.List<AudioReverbFilter> ReverbFilters = new System.Collections.Generic.List<AudioReverbFilter>();
		public System.Collections.Generic.List<AudioChorusFilter> ChorusFilters = new System.Collections.Generic.List<AudioChorusFilter>();
		public System.Collections.Generic.List<AudioEchoFilter> EchoFilters = new System.Collections.Generic.List<AudioEchoFilter>();
		public System.Collections.Generic.List<AudioDistortionFilter> DistortionFilters = new System.Collections.Generic.List<AudioDistortionFilter>();
		public System.Collections.Generic.List<AudioLowPassFilter> LowPassFilters = new System.Collections.Generic.List<AudioLowPassFilter>();
		public System.Collections.Generic.List<AudioHighPassFilter> HighPassFilters = new System.Collections.Generic.List<AudioHighPassFilter>();

        public Text Distortion;
        public Text Lowpass;
        public Text Highpass;
        public Text Volume;
        public Text Pitch;

#if UNITY_5_3 || UNITY_5_3_OR_NEWER
        public Dropdown ReverbFilterDropdown;
#endif

		private System.Collections.Generic.List<AudioReverbPreset> reverbPresets = new System.Collections.Generic.List<AudioReverbPreset>();

        #endregion


        #region MonoBehaviour methods

        public void Start()
        {
#if UNITY_5_3 || UNITY_5_3_OR_NEWER
			System.Collections.Generic.List<Dropdown.OptionData> options = new System.Collections.Generic.List<Dropdown.OptionData>();
#endif

			foreach (AudioReverbPreset arp in System.Enum.GetValues(typeof(AudioReverbPreset)))
            {
#if UNITY_5_3 || UNITY_5_3_OR_NEWER
                options.Add(new Dropdown.OptionData(arp.ToString()));
#endif

                reverbPresets.Add(arp);
            }

#if UNITY_5_3 || UNITY_5_3_OR_NEWER
            if (ReverbFilterDropdown != null)
            {
                ReverbFilterDropdown.ClearOptions();
                ReverbFilterDropdown.AddOptions(options);
            }
#endif
        }

        #endregion


        #region Public methods

        public void ResetFilters()
        {
            foreach (AudioSource source in Sources)
            {
                source.volume = 1f;
                source.pitch = 1f;
            }

            foreach (AudioReverbFilter reverbFilter in ReverbFilters)
            {
                reverbFilter.reverbPreset = reverbPresets[0];
            }

            foreach (AudioChorusFilter chorusFilter in ChorusFilters)
            {
                chorusFilter.enabled = false;
            }

            foreach (AudioEchoFilter echoFilter in EchoFilters)
            {
                echoFilter.enabled = false;
            }

            foreach (AudioDistortionFilter distortionFilter in DistortionFilters)
            {
                distortionFilter.distortionLevel = 0.5f;
                distortionFilter.enabled = false;
            }

            foreach (AudioLowPassFilter lowPassFilter in LowPassFilters)
            {
                lowPassFilter.cutoffFrequency = 5000;
                lowPassFilter.enabled = false;
            }

            foreach (AudioHighPassFilter highPassFilter in HighPassFilters)
            {
                highPassFilter.cutoffFrequency = 5000;
                highPassFilter.enabled = false;
            }

        }

        public void ClearFilters()
        {
            Sources.Clear();
            ReverbFilters.Clear();
            ChorusFilters.Clear();
            EchoFilters.Clear();
            DistortionFilters.Clear();
            LowPassFilters.Clear();
            HighPassFilters.Clear();
        }

		public void ReverbFilterDropdownChanged(System.Int32 index)
        {
            foreach (AudioReverbFilter reverbFilter in ReverbFilters)
            {
                reverbFilter.reverbPreset = reverbPresets[index];
            }
        }

        public void ChorusFilterEnabled(bool enabled)
        {
            foreach (AudioChorusFilter chorusFilter in ChorusFilters)
            {
                chorusFilter.enabled = enabled;
            }
        }

        public void EchoFilterEnabled(bool enabled)
        {
            foreach (AudioEchoFilter echoFilter in EchoFilters)
            {
                echoFilter.enabled = enabled;
            }
        }

        public void DistortionFilterEnabled(bool enabled)
        {
            foreach (AudioDistortionFilter distortionFilter in DistortionFilters)
            {
                distortionFilter.enabled = enabled;
            }
        }

        public void DistortionFilterChanged(float value)
        {
            foreach (AudioDistortionFilter distortionFilter in DistortionFilters)
            {
                distortionFilter.distortionLevel = value;
            }
            Distortion.text = value.ToString("0.00");
        }

        public void LowPassFilterEnabled(bool enabled)
        {
            foreach (AudioLowPassFilter lowPassFilter in LowPassFilters)
            {
                lowPassFilter.enabled = enabled;
            }
        }

        public void LowPassFilterChanged(float value)
        {
            foreach (AudioLowPassFilter lowPassFilter in LowPassFilters)
            {
                lowPassFilter.cutoffFrequency = value;
            }
            Lowpass.text = value.ToString();
        }

        public void HighPassFilterEnabled(bool enabled)
        {
            foreach (AudioHighPassFilter highPassFilter in HighPassFilters)
            {
                highPassFilter.enabled = enabled;
            }
        }

        public void HighPassFilterChanged(float value)
        {
            foreach (AudioHighPassFilter highPassFilter in HighPassFilters)
            {
                highPassFilter.cutoffFrequency = value;
            }
            Highpass.text = value.ToString();
        }

        public void VolumeChanged(float value)
        {
            foreach (AudioSource source in Sources)
            {
                source.volume = value;
            }
            Volume.text = value.ToString("0.00");
        }

        public void PitchChanged(float value)
        {
            foreach (AudioSource source in Sources)
            {
                source.pitch = value;
            }
            Pitch.text = value.ToString("0.00");
        }

        #endregion

    }
}
// © 2016-2017 crosstales LLC (https://www.crosstales.com)