using UnityEngine;
using UnityEditor;

namespace Crosstales.RTVoice.EditorExt
{
    /// <summary>Custom editor for the 'Sequencer'-class.</summary>
    [CustomEditor(typeof(Tool.Sequencer))]
    public class SequencerEditor : Editor
    {
        #region Variables

        private Tool.Sequencer script;

        #endregion


        #region Editor methods

        public void OnEnable()
        {
            script = (Tool.Sequencer)target;
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            if (script.isActiveAndEnabled)
            {
                if (!Speaker.isTTSAvailable)
                {
                    EditorHelper.SeparatorUI();

                    EditorHelper.NoVoicesUI();
                }
            }
            else
            {
                EditorHelper.SeparatorUI();

                GUILayout.Label("Script is disabled!", EditorStyles.boldLabel);
            }
        }

        #endregion
    }
}
// © 2016-2017 crosstales LLC (https://www.crosstales.com)