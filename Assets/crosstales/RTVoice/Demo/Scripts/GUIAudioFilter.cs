using UnityEngine;
using UnityEngine.UI;

namespace Crosstales.RTVoice.Demo
{
    /// <summary>Simple GUI for audio filters.</summary>
    [HelpURL("https://www.crosstales.com/media/data/assets/rtvoice/api/class_crosstales_1_1_r_t_voice_1_1_demo_1_1_g_u_i_audio_filter.html")]
    public class GUIAudioFilter : MonoBehaviour
    {

        #region Variables

        public AudioSource Source;

        public AudioReverbFilter ReverbFilter;
        public AudioChorusFilter ChorusFilter;
        public AudioEchoFilter EchoFilter;
        public AudioDistortionFilter DistortionFilter;
        public AudioLowPassFilter LowPassFilter;
        public AudioHighPassFilter HighPassFilter;

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

            ResetFilters();
        }

        #endregion


        #region Public methods

        public void ResetFilters()
        {
            Source.pitch = 1f;
            Source.volume = 1f;
            ReverbFilter.reverbPreset = reverbPresets[0];
            ChorusFilter.enabled = false;
            EchoFilter.enabled = false;
            DistortionFilter.distortionLevel = 0.5f;
            DistortionFilter.enabled = false;
            LowPassFilter.cutoffFrequency = 5000;
            LowPassFilter.enabled = false;
            HighPassFilter.cutoffFrequency = 5000;
            HighPassFilter.enabled = false;
        }

		public void ReverbFilterDropdownChanged(System.Int32 index)
        {
            ReverbFilter.reverbPreset = reverbPresets[index];
        }

        public void ChorusFilterEnabled(bool enabled)
        {
            ChorusFilter.enabled = enabled;
        }

        public void EchoFilterEnabled(bool enabled)
        {
            EchoFilter.enabled = enabled;
        }

        public void DistortionFilterEnabled(bool enabled)
        {
            DistortionFilter.enabled = enabled;
        }

        public void DistortionFilterChanged(float value)
        {
            DistortionFilter.distortionLevel = value;
            Distortion.text = value.ToString("0.00");
        }

        public void LowPassFilterEnabled(bool enabled)
        {
            LowPassFilter.enabled = enabled;
        }

        public void LowPassFilterChanged(float value)
        {
            LowPassFilter.cutoffFrequency = value;
            Lowpass.text = value.ToString();
        }

        public void HighPassFilterEnabled(bool enabled)
        {
            HighPassFilter.enabled = enabled;
        }

        public void HighPassFilterChanged(float value)
        {
            HighPassFilter.cutoffFrequency = value;
            Highpass.text = value.ToString();
        }

        public void VolumeChanged(float value)
        {
            Source.volume = value;
            Volume.text = value.ToString("0.00");
        }

        public void PitchChanged(float value)
        {
            Source.pitch = value;
            Pitch.text = value.ToString("0.00");
        }

        #endregion

    }
}
// © 2016-2017 crosstales LLC (https://www.crosstales.com)