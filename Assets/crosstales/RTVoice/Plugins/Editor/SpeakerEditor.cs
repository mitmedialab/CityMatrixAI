using UnityEngine;
using UnityEditor;

namespace Crosstales.RTVoice.EditorExt
{
    /// <summary>Custom editor for the 'Speaker'-class.</summary>
	[InitializeOnLoad]
    [CustomEditor(typeof(Speaker))]
    public class SpeakerEditor : Editor
    {

        #region Variables

        private int voiceIndex;
        private float rate = 1f;
        private float volume = 1f;
        private Speaker script;

        private bool showVoices = false;

        #endregion


        #region Static constructor

        static SpeakerEditor()
        {
            //EditorApplication.update += onEditorUpdate;
            EditorApplication.hierarchyWindowItemOnGUI += hierarchyItemCB;
        }

        #endregion


        #region Editor methods

        public void OnEnable()
        {
            script = (Speaker)target;
        }

        public void OnDisable()
        {
            if (Util.Helper.isEditorMode)
            {
                Speaker.Silence();
            }
        }

        //		public override bool RequiresConstantRepaint()
        //		{
        //			return true;
        //		}

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            //            GUILayout.Space(8);
            //
            //            script.MaryTTSMode = EditorGUILayout.BeginToggleGroup(new GUIContent("MaryTTS", "Enable or disable the MaryTTS-mode (default: off)."), script.MaryTTSMode);
            //            EditorGUI.indentLevel++;
            //            script.MaryTTSURL = EditorGUILayout.TextField(new GUIContent("URL", "Server URL for MaryTTS."), script.MaryTTSURL);
            //            script.MaryTTSPort = EditorGUILayout.IntSlider(new GUIContent("Port", "Server port for MaryTTS."), script.MaryTTSPort, 0, 65535);
            //            EditorGUI.indentLevel--;
            //            EditorGUILayout.EndToggleGroup();

            EditorHelper.SeparatorUI();

            if (script.isActiveAndEnabled)
            {
                GUILayout.Label("Data", EditorStyles.boldLabel);

                showVoices = EditorGUILayout.Foldout(showVoices, "Voices (" + Speaker.Voices.Count + ")");
                if (showVoices)
                {
                    EditorGUI.indentLevel++;

                    foreach (string voice in Speaker.Voices.CTToString())
                    {
                        EditorGUILayout.SelectableLabel(voice, GUILayout.Height(16), GUILayout.ExpandHeight(false));
                    }

                    EditorGUI.indentLevel--;
                }

                EditorHelper.SeparatorUI();

                if (Speaker.Voices.Count > 0)
                {
                    GUILayout.Label("Test-Drive", EditorStyles.boldLabel);

                    if (Util.Helper.isEditorMode)
                    {
                        voiceIndex = EditorGUILayout.Popup("Voice", voiceIndex, Speaker.Voices.CTToString().ToArray());
                        rate = EditorGUILayout.Slider("Rate", rate, 0f, 3f);

                        if (Util.Helper.isWindowsPlatform)
                        {
                            volume = EditorGUILayout.Slider("Volume", volume, 0f, 1f);
                        }

                        GUILayout.Space(8);

                        GUILayout.BeginHorizontal();
                        {
                            if (GUILayout.Button(new GUIContent("Preview Voice", EditorHelper.Icon_Speak, "Preview the selected voice.")))
                            {
                                Speaker.SpeakNativeInEditor("You have selected " + Speaker.Voices[voiceIndex].Name, Speaker.Voices[voiceIndex], rate, volume);
                            }

                            if (GUILayout.Button(new GUIContent("Silence", EditorHelper.Icon_Silence, "Silence all active previews.")))
                            {
                                Speaker.Silence();
                            }
                        }
                        GUILayout.EndHorizontal();
                    }
                    else
                    {
                        GUILayout.Label("Disabled in Play-mode!");
                    }
                }
            }
            else
            {
                GUILayout.Label("Script is disabled!", EditorStyles.boldLabel);
            }
        }

        #endregion


        #region Private methods

        private static void hierarchyItemCB(int instanceID, Rect selectionRect)
        {
            if (Util.Config.HIERARCHY_ICON)
            {
                GameObject go = EditorUtility.InstanceIDToObject(instanceID) as GameObject;

                if (go != null && go.GetComponent<Speaker>())
                {
                    Rect r = new Rect(selectionRect);
                    r.x = r.width - 4;

                    //Debug.Log("HierarchyItemCB: " + r);

                    GUI.Label(r, EditorHelper.Logo_Asset_Small);
                }
            }
        }

        #endregion

    }
}
// © 2016-2017 crosstales LLC (https://www.crosstales.com)