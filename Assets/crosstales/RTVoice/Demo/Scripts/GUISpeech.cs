using UnityEngine;
using UnityEngine.UI;
using Crosstales.RTVoice.Model;
using Crosstales.RTVoice.Util;

namespace Crosstales.RTVoice.Demo
{
    /// <summary>Simple GUI for runtime TTS with all available OS voices.</summary>
    [HelpURL("https://www.crosstales.com/media/data/assets/rtvoice/api/class_crosstales_1_1_r_t_voice_1_1_demo_1_1_g_u_i_speech.html")]
    public class GUISpeech : MonoBehaviour
    {

        #region Variables

        public GameObject ItemPrefab;
        public GameObject Target;
        public Scrollbar Scroll;
        public int ColumnCount = 1;
        public Vector2 SpaceWidth = new Vector2(8, 8);
        public Vector2 SpaceHeight = new Vector2(8, 8);
        public InputField Input;
        public InputField Culture;
        public Text Cultures;
        public bool StartAsNative = false;

        public static float Rate = 1f;
        public static float Pitch = 1f;
        public static float Volume = 1f;
        public static bool isNative = false;
        public GUIMultiAudioFilter AudioFilter;

        private string lastCulture = "unknown";
		private System.Collections.Generic.List<SpeakWrapper> wrappers = new System.Collections.Generic.List<SpeakWrapper>();

        private bool buildVoicesListIOS = false;

        #endregion


        #region MonoBehaviour methods

        public void Start()
        {
            Speaker.OnProviderChange += onProviderChange;

            //            if (Helper.isWebPlatform)
            //            {
            //                Speaker.MaryMode = true;
            //            }

            Cultures.text = string.Join(", ", Speaker.Cultures.ToArray());
            Input.text = "Hi there, my name is RTVoice, your runtime speaker!";
            Culture.text = string.Empty;

            isNative = StartAsNative;

            //buildVoicesList();
        }

        public void Update()
        {
            if (!lastCulture.Equals(Culture.text))
            {
                buildVoicesList();

                lastCulture = Culture.text;
            }

            /*
            if (Helper.isIOSPlatform && Time.frameCount % 60 == 0 && !buildVoicesListIOS)
            {
                buildVoicesList();
                buildVoicesListIOS = true;
            }
            */
        }

        public void OnDestroy()
        {
            Speaker.OnProviderChange -= onProviderChange;

            //if (Helper.hasBuiltInTTS)
            //{
                Speaker.MaryMode = false;
            //}
        }

        #endregion


        #region Public methods

        public void Silence()
        {
            //foreach (SpeakWrapper wrapper in wrappers)
            //{
            //    if (wrapper.Audio != null)
            //    {
            //        wrapper.Audio.Stop();
            //        wrapper.Audio.clip = null;
            //    }
            //}

            Speaker.Silence();
        }

        public void ChangeRate(float rate)
        {
            Rate = rate;
        }

        public void ChangeVolume(float volume)
        {
            Volume = volume;
        }

        public void ChangePitch(float pitch)
        {
            Pitch = pitch;
        }

        public void ChangeNative(bool native)
        {
            isNative = native;
        }

        public void ChangeMaryTTS(bool maryTTS)
        {
            Speaker.MaryMode = maryTTS;
        }

        #endregion


        #region Private methods

        private void onProviderChange(string provider)
        {
            //Debug.Log("ProviderChange: " + provider);

            Cultures.text = string.Join(", ", Speaker.Cultures.ToArray());

            buildVoicesList();
        }

        private void buildVoicesList()
        {
            wrappers.Clear();

            if (AudioFilter != null)
            {
                AudioFilter.ClearFilters();
            }
            RectTransform rowRectTransform = ItemPrefab.GetComponent<RectTransform>();
            RectTransform containerRectTransform = Target.GetComponent<RectTransform>();

            for (var i = Target.transform.childCount - 1; i >= 0; i--)
            {
                Transform child = Target.transform.GetChild(i);
                child.SetParent(null);
                Destroy(child.gameObject);
                // Optionally destroy the objectA if not longer needed
            }


			System.Collections.Generic.List<Voice> items = Speaker.VoicesForCulture(Culture.text);

            if (items.Count == 0)
            {
                Debug.LogWarning("No voices for culture '" + Culture.text + "' found - using the default system voices.");
                items = Speaker.Voices;
            }

            if (items.Count > 0)
            {
                //Debug.Log("ITEMS: " + items.Count);

                //calculate the width and height of each child item.
                float width = containerRectTransform.rect.width / ColumnCount - (SpaceWidth.x + SpaceWidth.y) * ColumnCount;
                float height = rowRectTransform.rect.height - (SpaceHeight.x + SpaceHeight.y);

                int rowCount = items.Count / ColumnCount;

                if (rowCount > 0 && items.Count % rowCount > 0)
                {
                    rowCount++;
                }

                //adjust the height of the container so that it will just barely fit all its children
                float scrollHeight = height * rowCount;
                containerRectTransform.offsetMin = new Vector2(containerRectTransform.offsetMin.x, -scrollHeight / 2);
                containerRectTransform.offsetMax = new Vector2(containerRectTransform.offsetMax.x, scrollHeight / 2);

                int j = 0;
                for (int ii = 0; ii < items.Count; ii++)
                {
                    //this is used instead of a double for loop because itemCount may not fit perfectly into the rows/columns
                    if (ii % ColumnCount == 0)
                    {
                        j++;
                    }

                    //create a new item, name it, and set the parent
                    GameObject newItem = Instantiate(ItemPrefab) as GameObject;
                    newItem.name = Target.name + " item at (" + ii + "," + j + ")";
                    newItem.transform.SetParent(Target.transform);
                    newItem.transform.localScale = Vector3.one;

                    if (AudioFilter != null)
                    {
                        AudioFilter.Sources.Add(newItem.GetComponent<AudioSource>());
                        AudioFilter.ReverbFilters.Add(newItem.GetComponent<AudioReverbFilter>());
                        AudioFilter.ChorusFilters.Add(newItem.GetComponent<AudioChorusFilter>());
                        AudioFilter.EchoFilters.Add(newItem.GetComponent<AudioEchoFilter>());
                        AudioFilter.DistortionFilters.Add(newItem.GetComponent<AudioDistortionFilter>());
                        AudioFilter.LowPassFilters.Add(newItem.GetComponent<AudioLowPassFilter>());
                        AudioFilter.HighPassFilters.Add(newItem.GetComponent<AudioHighPassFilter>());
                    }

                    SpeakWrapper wrapper = newItem.GetComponent<SpeakWrapper>();
                    wrapper.SpeakerVoice = items[ii];
                    wrapper.Input = Input;
                    wrapper.Label.text = items[ii].Name;
                    wrappers.Add(wrapper);

                    //move and size the new item
                    RectTransform rectTransform = newItem.GetComponent<RectTransform>();

                    float x = -containerRectTransform.rect.width / 2 + (width + SpaceWidth.x) * (ii % ColumnCount) + SpaceWidth.x * ColumnCount;
                    float y = containerRectTransform.rect.height / 2 - height * j;
                    rectTransform.offsetMin = new Vector2(x, y);

                    x = rectTransform.offsetMin.x + width;
                    y = rectTransform.offsetMin.y + height;
                    rectTransform.offsetMax = new Vector2(x, y);
                }

                if (AudioFilter != null)
                {
                    AudioFilter.ResetFilters();
                }
            }
            else
            {
                Debug.LogError("No voices found - speech is not possible!");
            }

            Scroll.value = 1f;
        }

        #endregion
    }
}
// © 2015-2017 crosstales LLC (https://www.crosstales.com)