using UnityEngine;
using UnityEngine.UI;

namespace Crosstales.RTVoice.Demo
{
    /// <summary>Simple GUI for runtime dialogs with all available OS voices.</summary>
    [HelpURL("https://www.crosstales.com/media/data/assets/rtvoice/api/class_crosstales_1_1_r_t_voice_1_1_demo_1_1_g_u_i_dialog.html")]
    public class GUIDialog : MonoBehaviour
    {

        #region Variables

        public Dialog DialogScript;
        public Image PanelPersonA;
        public Image PanelPersonB;
        public Text PersonA;
        public Text PersonB;
        public Color32 SpeakerColor = new Color32(0, 255, 0, 192);

        private Color32 baseColorA;
        private Color32 baseColorB;

        #endregion


        #region MonoBehaviour methods

        public void Start()
        {
            baseColorA = PanelPersonA.color;
            baseColorB = PanelPersonB.color;
        }

        public void Update()
        {
            if (!string.IsNullOrEmpty(DialogScript.CurrentDialogA))
            {
				PersonA.text += DialogScript.CurrentDialogA + System.Environment.NewLine + System.Environment.NewLine;
                DialogScript.CurrentDialogA = string.Empty;
                PanelPersonA.color = SpeakerColor;
                PanelPersonB.color = baseColorB;
            }

            if (!string.IsNullOrEmpty(DialogScript.CurrentDialogB))
            {
				PersonB.text += DialogScript.CurrentDialogB + System.Environment.NewLine + System.Environment.NewLine;
                DialogScript.CurrentDialogB = string.Empty;
                PanelPersonA.color = baseColorA;
                PanelPersonB.color = SpeakerColor;
            }
        }

        #endregion


        #region Public methods

        public void StartDialog()
        {
            Silence();
            if (DialogScript != null)
            {
                StartCoroutine(DialogScript.DialogSequence());
            }
            else
            {
                Debug.LogWarning("'DialogScript' is null - please assign it in the editor!");
            }
        }

        public void Silence()
        {
            StopAllCoroutines();
            DialogScript.AudioPersonA.Stop();
            DialogScript.AudioPersonB.Stop();

            Speaker.Silence();

            DialogScript.VisualsA.SetActive(false);
            DialogScript.VisualsB.SetActive(false);
            DialogScript.Running = false;

            PanelPersonA.color = baseColorA;
            PanelPersonB.color = baseColorB;

            PersonA.text = string.Empty;
            PersonB.text = string.Empty;
        }

        public void ChangeRateA(float value)
        {
            DialogScript.RateA = value;
        }

        public void ChangeRateB(float value)
        {
            DialogScript.RateB = value;
        }

        public void ChangeVolumeA(float value)
        {
            DialogScript.VolumeA = value;
        }

        public void ChangeVolumeB(float value)
        {
            DialogScript.VolumeB = value;
        }

        #endregion
    }
}
// © 2015-2017 crosstales LLC (https://www.crosstales.com)