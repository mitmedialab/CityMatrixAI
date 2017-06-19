using UnityEngine;
using UnityEditor;

namespace Crosstales.RTVoice.EditorExt
{
    /// <summary>Custom editor for the 'SpeechText'-class.</summary>
    [CustomEditor(typeof(Tool.SpeechText))]
    [CanEditMultipleObjects]
    public class SpeechTextEditor : Editor
    {

        #region Variables

        private Tool.SpeechText script;

        #endregion


        #region Editor methods

        public void OnEnable()
        {
            script = (Tool.SpeechText)target;
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
                            if (GUILayout.Button(new GUIContent("Speak", EditorHelper.Icon_Speak, "Speaks the text with the selected voice and settings.")))
                            {
                                script.Speak();
                            }

                            if (GUILayout.Button(new GUIContent("Silence", EditorHelper.Icon_Silence, "Silence the active speaker.")))
                            {
                                script.Silence();
                            }
                        }
                        GUILayout.EndHorizontal();

                        EditorHelper.SeparatorUI();

                        GUILayout.Label("Editor", EditorStyles.boldLabel);

                        if (GUILayout.Button(new GUIContent("Refresh AssetDatabase", EditorHelper.Icon_Refresh, "Refresh the AssetDatabase from the Editor.")))
                        {
                            refreshAssetDatabase();
                        }
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


        #region Private methods

        private void refreshAssetDatabase()
        {
            if (Util.Helper.isEditorMode)
            {
                //Debug.Log("Refresh AssetDatabase");
                AssetDatabase.Refresh();
            }
        }

        #endregion
    }
}
// © 2016-2017 crosstales LLC (https://www.crosstales.com)