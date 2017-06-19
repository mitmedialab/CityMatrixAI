using UnityEngine;
using UnityEditor;

namespace Crosstales.RTVoice.EditorExt
{
    /// <summary>Custom editor for the 'TextFileSpeaker'-class.</summary>
    [CustomEditor(typeof(Tool.TextFileSpeaker))]
    [CanEditMultipleObjects]
    public class TextFileSpeakerEditor : Editor
    {

        #region Variables

        private Tool.TextFileSpeaker script;

        #endregion


        #region Editor methods

        public void OnEnable()
        {
            script = (Tool.TextFileSpeaker)target;
        }

        public void OnDisable()
        {
            if (Util.Helper.isEditorMode)
            {
                Speaker.Silence();
            }
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            EditorHelper.SeparatorUI();

            if (script.isActiveAndEnabled)
            {
                if (Speaker.isTTSAvailable)
                {
                    GUILayout.Label("Test-Drive", EditorStyles.boldLabel);

                    if (Util.Helper.isEditorMode)
                    {
                        GUILayout.BeginHorizontal();
                        {
                            if (GUILayout.Button(new GUIContent("Speak", EditorHelper.Icon_Speak, "Speaks a random text file with the selected voice and settings.")))
                            {
                                script.Speak();
                            }

                            if (GUILayout.Button(new GUIContent("Silence", EditorHelper.Icon_Silence, "Silence the active speaker.")))
                            {
                                script.Silence();
                            }
                        }
                        GUILayout.EndHorizontal();
                    }
                    else
                    {
                        GUILayout.Label("Disabled in Play-mode!");
                    }
                }
                else
                {
                    EditorHelper.NoVoicesUI();
                }
            }
            else
            {
                GUILayout.Label("Script is disabled!", EditorStyles.boldLabel);
            }
        }

        #endregion
    }
}
// © 2016-2017 crosstales LLC (https://www.crosstales.com)