using UnityEditor;
using UnityEngine;

namespace Crosstales.RTVoice.EditorExt
{
    /// <summary>Editor window extension.</summary>
    [InitializeOnLoad]
    public class ConfigWindow : ConfigBase
    {

        #region Variables

        private int tab = 0;
        private int lastTab = 0;
        private string text = "Test all your voices with different texts and settings.";
        private int voiceIndex;
        private float rate = 1f;
        private float volume = 1f;
        private bool silenced = true;

        private static Vector2 scrollPosPrefabs;
        private static Vector2 scrollPosTD;

        public delegate void StopPlayback();

        public static event StopPlayback OnStopPlayback;

        #endregion


        #region Static constructor

        static ConfigWindow()
        {
            EditorApplication.update += onEditorUpdate;
        }

        #endregion


        #region EditorWindow methods

		[MenuItem("Tools/" + Util.Constants.ASSET_NAME + "/Configuration...", false, EditorHelper.MENU_ID + 1)]
        public static void ShowWindow()
        {
            EditorWindow.GetWindow(typeof(ConfigWindow));
        }

        public void OnEnable()
        {
            titleContent = new GUIContent(Util.Constants.ASSET_NAME, EditorHelper.Logo_Asset_Small);

            OnStopPlayback += silence;
        }

        public void OnDisable()
        {
            Speaker.Silence();

            OnStopPlayback -= silence;
        }

        public void OnGUI()
        {
            tab = GUILayout.Toolbar(tab, new string[] { "Config", "Prefabs", "TD", "Help", "About" });

            if (tab != lastTab)
            {
                lastTab = tab;
                GUI.FocusControl(null);
            }

            if (tab == 0)
            {
                showConfiguration();

                EditorHelper.SeparatorUI();

                GUILayout.BeginHorizontal();
                {
                    if (GUILayout.Button(new GUIContent("Save", EditorHelper.Icon_Save, "Saves the configuration settings for this project.")))
                    {
                        save();
                    }

                    if (GUILayout.Button(new GUIContent("Reset", EditorHelper.Icon_Reset, "Resets the configuration settings for this project.")))
                    {
                        if (EditorUtility.DisplayDialog("Reset configuration?", "Reset the configuration of " + Util.Constants.ASSET_NAME + "?", "Yes", "No"))
                        {
                            Util.Config.Reset();
                            save();
                        }
                    }
                }
                GUILayout.EndHorizontal();

                GUILayout.Space(6);
            }
            else if (tab == 1)
            {
                showPrefabs();
            }
            else if (tab == 2)
            {
                showTestDrive();
            }
            else if (tab == 3)
            {
                showHelp();
            }
            else
            {
                showAbout();
            }
        }

        public void OnInspectorUpdate()
        {
            Repaint();
        }

        #endregion


        #region Private methods

        private static void onEditorUpdate()
        {
            if (EditorApplication.isCompiling || EditorApplication.isPlaying || BuildPipeline.isBuildingPlayer)
            {
                onStopPlayback();
            }
        }

        private static void onStopPlayback()
        {
            if (OnStopPlayback != null)
            {
                OnStopPlayback();
            }
        }

        private void silence()
        {
            if (!silenced)
            {
                Speaker.Silence();
                silenced = true;
            }
        }

        private static void showPrefabs()
        {
            scrollPosPrefabs = EditorGUILayout.BeginScrollView(scrollPosPrefabs, false, false);
            {
                //GUILayout.Space(8);
                GUILayout.Label("Available Prefabs", EditorStyles.boldLabel);

                GUILayout.Space(6);
                //EditorHelper.SeparatorUI (6);

                if (!EditorHelper.isRTVoiceInScene)
                {
					GUILayout.Label(Util.Constants.RTVOICE_SCENE_OBJECT_NAME);

					if (GUILayout.Button(new GUIContent("Add", EditorHelper.Icon_Plus, "Adds a '" + Util.Constants.RTVOICE_SCENE_OBJECT_NAME + "'-prefab to the scene.")))
                    {
						EditorHelper.InstantiatePrefab(Util.Constants.RTVOICE_SCENE_OBJECT_NAME);
                    }

                    EditorHelper.SeparatorUI();
                }

                GUILayout.Label("SpeechText");

                if (GUILayout.Button(new GUIContent("Add", EditorHelper.Icon_Plus, "Adds a 'SpeechText'-prefab to the scene.")))
                {
                    EditorHelper.InstantiatePrefab("SpeechText");
                }

                GUILayout.Space(6);

                GUILayout.Label("Sequencer");

                if (GUILayout.Button(new GUIContent("Add", EditorHelper.Icon_Plus, "Adds a 'Sequencer'-prefab to the scene.")))
                {
                    EditorHelper.InstantiatePrefab("Sequencer");
                }

                GUILayout.Space(6);

                GUILayout.Label("TextFileSpeaker");

                if (GUILayout.Button(new GUIContent("Add", EditorHelper.Icon_Plus, "Adds a 'TextFileSpeaker'-prefab to the scene.")))
                {
                    EditorHelper.InstantiatePrefab("TextFileSpeaker");
                }

                EditorHelper.SeparatorUI();

                GUILayout.Label("Loudspeaker");

                if (GUILayout.Button(new GUIContent("Add", EditorHelper.Icon_Plus, "Adds a 'Loudspeaker'-prefab to the scene.")))
                {
                    EditorHelper.InstantiatePrefab("Loudspeaker");
                }

				if (!EditorHelper.isInternetCheckInScene)
				{
					EditorHelper.SeparatorUI();

					GUILayout.Label(Util.Constants.INTERNETCHECK_SCENE_OBJECT_NAME);
					if (GUILayout.Button(new GUIContent("Add", EditorHelper.Icon_Plus, "Adds a '" + Util.Constants.INTERNETCHECK_SCENE_OBJECT_NAME + "'-prefab to the scene.")))
					{
						EditorHelper.InstantiatePrefab(Util.Constants.INTERNETCHECK_SCENE_OBJECT_NAME);
					}
				}

                if (!EditorHelper.isProxyInScene)
                {
                    EditorHelper.SeparatorUI();

					GUILayout.Label(Util.Constants.PROXY_SCENE_OBJECT_NAME);
					if (GUILayout.Button(new GUIContent("Add", EditorHelper.Icon_Plus, "Adds a '" + Util.Constants.PROXY_SCENE_OBJECT_NAME + "'-prefab to the scene.")))
                    {
						EditorHelper.InstantiatePrefab(Util.Constants.PROXY_SCENE_OBJECT_NAME);
                    }
                }

                GUILayout.Space(6);
            }
            EditorGUILayout.EndScrollView();
        }

        private void showTestDrive()
        {
            if (Util.Helper.isEditorMode)
            {
                if (Speaker.Voices.Count > 0)
                {
                    scrollPosTD = EditorGUILayout.BeginScrollView(scrollPosTD, false, false);
                    {
                        GUILayout.Label("Test-Drive", EditorStyles.boldLabel);

                        text = EditorGUILayout.TextField("Text: ", text);

                        voiceIndex = EditorGUILayout.Popup("Voice", voiceIndex, Speaker.Voices.CTToString().ToArray());
                        rate = EditorGUILayout.Slider("Rate", rate, 0f, 3f);

                        if (Util.Helper.isWindowsPlatform)
                        {
                            volume = EditorGUILayout.Slider("Volume", volume, 0f, 1f);
                        }

                    }
                    EditorGUILayout.EndScrollView();

                    EditorHelper.SeparatorUI();

                    GUILayout.BeginHorizontal();
                    {
                        if (GUILayout.Button(new GUIContent("Speak", EditorHelper.Icon_Speak, "Speaks the text with the selected voice and settings.")))
                        {
                            Speaker.SpeakNativeInEditor(text, Speaker.Voices[voiceIndex], rate, volume);
                            silenced = false;
                        }

                        if (GUILayout.Button(new GUIContent("Silence", EditorHelper.Icon_Silence, "Silence all active speakers.")))
                        {
                            silence();
                        }
                    }
                    GUILayout.EndHorizontal();

                    GUILayout.Space(6);

                }
                else
                {
                    EditorHelper.NoVoicesUI();
                }

            }
            else
            {
                GUILayout.Label("Disabled in Play-mode!");
            }
        }

        #endregion
    }
}
// © 2016-2017 crosstales LLC (https://www.crosstales.com)