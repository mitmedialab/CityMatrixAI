using UnityEngine;
using UnityEditor;

namespace Crosstales.RTVoice.EditorExt
{
    /// <summary>Custom editor for the 'InternetCheck'-class.</summary>
    [InitializeOnLoad]
    [CustomEditor(typeof(Tool.InternetCheck))]
    public class InternetCheckEditor : Editor
    {

        #region Variables

        private Tool.InternetCheck script;

        #endregion


        #region Editor methods

        public void OnEnable()
        {
            script = (Tool.InternetCheck)target;
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            EditorHelper.SeparatorUI();

            if (script.isActiveAndEnabled)
            {
                GUILayout.Label("Internet Status", EditorStyles.boldLabel);

                GUILayout.BeginHorizontal();
                {
                    GUILayout.Label("Available:");

                    GUI.enabled = false;
                    EditorGUILayout.Toggle(new GUIContent(string.Empty, "Is Internet currently available?"), Tool.InternetCheck.isInternetAvailable);
                    GUI.enabled = true;
                }
                GUILayout.EndHorizontal();

                if (Util.Helper.isEditorMode)
                {
                    if (GUILayout.Button(new GUIContent("Refresh", EditorHelper.Icon_Reset, "Restart the Internet availability check.")))
                    {
                        Tool.InternetCheck.Refresh();
                    }
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
// © 2017 crosstales LLC (https://www.crosstales.com)