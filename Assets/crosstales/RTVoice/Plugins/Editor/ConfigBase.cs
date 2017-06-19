using UnityEngine;
using UnityEditor;

namespace Crosstales.RTVoice.EditorExt
{
    /// <summary>Base class for editor windows.</summary>
    public abstract class ConfigBase : EditorWindow
    {

        #region Variables

        protected static string updateText = UpdateCheck.TEXT_NOT_CHECKED;

        private static System.Threading.Thread worker;

        private static Vector2 scrollPosConfig;
        private static Vector2 scrollPosHelp;
        private static Vector2 scrollPosAbout;

        #endregion


        #region Protected methods

        protected static void showConfiguration()
        {
            scrollPosConfig = EditorGUILayout.BeginScrollView(scrollPosConfig, false, false);
            {
                GUILayout.Label("General Settings", EditorStyles.boldLabel);

                Util.Config.ASSET_PATH = EditorGUILayout.TextField(new GUIContent("Asset Path", "Path to the asset inside the Unity-project (default: '" + Util.Constants.DEFAULT_ASSET_PATH + "')."), Util.Config.ASSET_PATH);

                Util.Config.DEBUG = EditorGUILayout.Toggle(new GUIContent("Debug", "Enable or disable debug logs (default: " + Util.Constants.DEFAULT_DEBUG + ")."), Util.Config.DEBUG);

                Util.Config.UPDATE_CHECK = EditorGUILayout.BeginToggleGroup(new GUIContent("Update Check", "Enable or disable the update-check (default: " + Util.Constants.DEFAULT_UPDATE_CHECK + ")."), Util.Config.UPDATE_CHECK);
                EditorGUI.indentLevel++;
                Util.Config.UPDATE_OPEN_UAS = EditorGUILayout.Toggle(new GUIContent("Open UAS-Site", "Automatically opens the direct link to 'Unity AssetStore' if an update was found (default: " + Util.Constants.DEFAULT_UPDATE_OPEN_UAS + ")."), Util.Config.UPDATE_OPEN_UAS);
                EditorGUI.indentLevel--;
                EditorGUILayout.EndToggleGroup();

                //Constants.DONT_DESTROY_ON_LOAD = EditorGUILayout.Toggle(new GUIContent("Don't destroy on load", "Don't destroy RTVoice during scene switches (default: on, off is NOT RECOMMENDED!)."), Constants.DONT_DESTROY_ON_LOAD);
                Util.Config.PREFAB_AUTOLOAD = EditorGUILayout.Toggle(new GUIContent("Prefab Auto-Load", "Enable or disable auto-loading of the prefabs to the scene (default: " + Util.Constants.DEFAULT_PREFAB_AUTOLOAD + ")."), Util.Config.PREFAB_AUTOLOAD);

                Util.Config.AUDIOFILE_PATH = EditorGUILayout.TextField(new GUIContent("Audio Path", "Path to the generated audio files (default: '" + Util.Constants.DEFAULT_AUDIOFILE_PATH + "')."), Util.Config.AUDIOFILE_PATH);
                Util.Config.AUDIOFILE_AUTOMATIC_DELETE = EditorGUILayout.Toggle(new GUIContent("Audio Auto-Delete", "Enable or disable auto-delete of the generated audio files (default: " + Util.Constants.DEFAULT_AUDIOFILE_AUTOMATIC_DELETE + ")."), Util.Config.AUDIOFILE_AUTOMATIC_DELETE);

                EditorHelper.SeparatorUI();
                GUILayout.Label("UI Settings", EditorStyles.boldLabel);
                Util.Config.HIERARCHY_ICON = EditorGUILayout.Toggle(new GUIContent("Show Hierarchy Icon", "Show hierarchy icon (default: " + Util.Constants.DEFAULT_HIERARCHY_ICON + ")."), Util.Config.HIERARCHY_ICON);

                EditorHelper.SeparatorUI();
                GUILayout.Label("Windows Settings", EditorStyles.boldLabel);
                Util.Config.ENFORCE_32BIT_WINDOWS = EditorGUILayout.Toggle(new GUIContent("Enforce 32bit Voices", "Enforce 32bit versions of voices under Windows (default: " + Util.Constants.DEFAULT_ENFORCE_32BIT_WINDOWS + ")."), Util.Config.ENFORCE_32BIT_WINDOWS);

                //EditorHelper.SeparatorUI();
                //GUILayout.Label("macOS Settings", EditorStyles.boldLabel);
                //Constants.TTS_MACOS = EditorGUILayout.TextField(new GUIContent("TTS-command", "TTS-command under macOS (default: " + Constants.DEFAULT_TTS_MACOS + ")."), Constants.TTS_MACOS);
            }
            EditorGUILayout.EndScrollView();
        }

        protected static void showHelp()
        {
            scrollPosHelp = EditorGUILayout.BeginScrollView(scrollPosHelp, false, false);
            {
                GUILayout.Label("Resources", EditorStyles.boldLabel);

                //GUILayout.Space(8);

                GUILayout.BeginHorizontal();
                {
                    GUILayout.BeginVertical();
                    {

                        if (GUILayout.Button(new GUIContent("Manual", EditorHelper.Icon_Manual, "Show the manual.")))
                        {
                            Application.OpenURL(Util.Constants.ASSET_MANUAL_URL);
                        }

                        GUILayout.Space(6);

                        if (GUILayout.Button(new GUIContent("Forum", EditorHelper.Icon_Forum, "Visit the forum page.")))
                        {
                            Application.OpenURL(Util.Constants.ASSET_FORUM_URL);
                        }
                    }
                    GUILayout.EndVertical();

                    GUILayout.BeginVertical();
                    {

                        if (GUILayout.Button(new GUIContent("API", EditorHelper.Icon_API, "Show the API.")))
                        {
                            Application.OpenURL(Util.Constants.ASSET_API_URL);
                        }

                        GUILayout.Space(6);

                        if (GUILayout.Button(new GUIContent("Product", EditorHelper.Icon_Product, "Visit the product page.")))
                        {
                            Application.OpenURL(Util.Constants.ASSET_WEB_URL);
                        }
                    }
                    GUILayout.EndVertical();

                }
                GUILayout.EndHorizontal();

                EditorHelper.SeparatorUI();

                GUILayout.Label("3rd Party Assets", EditorStyles.boldLabel);

                //				GUILayout.BeginHorizontal ();
                //				{
                GUILayout.BeginHorizontal();
                {
                    if (GUILayout.Button(new GUIContent(string.Empty, EditorHelper.Store_PlayMaker, "More information about 'PlayMaker'.")))
                    {
                        Application.OpenURL(Util.Constants.ASSET_3P_PLAYMAKER);
                        //Application.OpenURL(Util.Constants.ASSET_3RD_PARTY_URL);
                    }

                    if (GUILayout.Button(new GUIContent(string.Empty, EditorHelper.Store_AdventureCreator, "More information about 'Adventure Creator'.")))
                    {
                        Application.OpenURL(Util.Constants.ASSET_3P_ADVENTURE_CREATOR);
                    }

                    if (GUILayout.Button(new GUIContent(string.Empty, EditorHelper.Store_CinemaDirector, "More information about 'Cinema Director'.")))
                    {
                        Application.OpenURL(Util.Constants.ASSET_3P_CINEMA_DIRECTOR);
                    }

                }
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                {
                    if (GUILayout.Button(new GUIContent(string.Empty, EditorHelper.Store_DialogueSystem, "More information about 'Dialogue System'.")))
                    {
                        Application.OpenURL(Util.Constants.ASSET_3P_DIALOG_SYSTEM);
                    }

                    if (GUILayout.Button(new GUIContent(string.Empty, EditorHelper.Store_LDC, "More information about 'Localized Dialogs'.")))
                    {
                        Application.OpenURL(Util.Constants.ASSET_3P_LOCALIZED_DIALOGS);
                    }

                    //GUILayout.Space (6);

                    if (GUILayout.Button(new GUIContent(string.Empty, EditorHelper.Store_LipSync, "More information about 'LipSync'.")))
                    {
                        Application.OpenURL(Util.Constants.ASSET_3P_LIPSYNC);
                    }


                }
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                {
                    if (GUILayout.Button(new GUIContent(string.Empty, EditorHelper.Store_NPC_Chat, "More information about 'NPC Chat'.")))
                    {
                        Application.OpenURL(Util.Constants.ASSET_3P_NPC_CHAT);
                    }

                    //GUILayout.Space (6);

                    if (GUILayout.Button(new GUIContent(string.Empty, EditorHelper.Store_QuestSystem, "More information about 'Quest System'.")))
                    {
                        Application.OpenURL(Util.Constants.ASSET_3P_QUEST_SYSTEM);
                    }

                    if (GUILayout.Button(new GUIContent(string.Empty, EditorHelper.Store_SALSA, "More information about 'SALSA'.")))
                    {
                        Application.OpenURL(Util.Constants.ASSET_3P_SALSA);
                    }

                }
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                {
                    //GUILayout.Space (6);

                    if (GUILayout.Button(new GUIContent(string.Empty, EditorHelper.Store_SLATE, "More information about 'SLATE'.")))
                    {
                        Application.OpenURL(Util.Constants.ASSET_3P_SLATE);
                    }

                    if (GUILayout.Button(new GUIContent(string.Empty, EditorHelper.Store_THE_Dialogue_Engine, "More information about 'THE Dialogue Engine'.")))
                    {
                        Application.OpenURL(Util.Constants.ASSET_3P_DIALOGUE_ENGINE);
                    }

                    //GUILayout.Space (6);

                    if (GUILayout.Button(new GUIContent(string.Empty, EditorHelper.Store_uSequencer, "More information about 'uSequencer'.")))
                    {
                        Application.OpenURL(Util.Constants.ASSET_3P_USEQUENCER);
                    }
                }
                GUILayout.EndHorizontal();
                //				}
                //				GUILayout.EndHorizontal ();

                GUILayout.Space(6);

                if (GUILayout.Button(new GUIContent("All Supported Assets", EditorHelper.Icon_3p_Assets, "More information about the all supported assets.")))
                {
                    Application.OpenURL(Util.Constants.ASSET_3P_URL);
                    //Application.OpenURL(Util.Constants.ASSET_3RD_PARTY_URL);
                }
            }
            EditorGUILayout.EndScrollView();

            GUILayout.Space(6);
        }

        protected static void showAbout()
        {
            scrollPosAbout = EditorGUILayout.BeginScrollView(scrollPosAbout, false, false);
            {
                GUILayout.Label(Util.Constants.ASSET_NAME, EditorStyles.boldLabel);

                GUILayout.BeginHorizontal();
                {
                    GUILayout.BeginVertical(GUILayout.Width(60));
                    {
                        GUILayout.Label("Version:");

                        GUILayout.Space(12);

                        GUILayout.Label("Web:");

                        GUILayout.Space(2);

                        GUILayout.Label("Email:");

                    }
                    GUILayout.EndVertical();

                    GUILayout.BeginVertical(GUILayout.Width(170));
                    {
                        GUILayout.Space(0);

                        GUILayout.Label(Util.Constants.ASSET_VERSION);

                        GUILayout.Space(12);

                        EditorGUILayout.SelectableLabel(Util.Constants.ASSET_AUTHOR_URL, GUILayout.Height(16), GUILayout.ExpandHeight(false));

                        GUILayout.Space(2);

                        EditorGUILayout.SelectableLabel(Util.Constants.ASSET_CONTACT, GUILayout.Height(16), GUILayout.ExpandHeight(false));
                    }
                    GUILayout.EndVertical();

                    GUILayout.BeginVertical(GUILayout.ExpandWidth(true));
                    {
                        //GUILayout.Space(0);
                    }
                    GUILayout.EndVertical();

                    GUILayout.BeginVertical(GUILayout.Width(64));
                    {
                        //GUILayout.Label(logo_asset, GUILayout.Height(80));

                        if (GUILayout.Button(new GUIContent(string.Empty, EditorHelper.Logo_Asset, "Visit asset website")))
                        {
                            Application.OpenURL(Util.Constants.ASSET_URL);
                        }

                        if (!Util.Constants.isPro)
                        {
                            if (GUILayout.Button(new GUIContent("Upgrade", "Upgrade " + Util.Constants.ASSET_NAME + " to the PRO-version")))
                            {
                                Application.OpenURL(Util.Constants.ASSET_PRO_URL);
                            }
                        }
                    }
                    GUILayout.EndVertical();
                }
                GUILayout.EndHorizontal();

                GUILayout.Label("© 2015-2017 by " + Util.Constants.ASSET_AUTHOR);

                EditorHelper.SeparatorUI();

				if (UpdateCheck.Status == UpdateStatus.NOT_CHECKED || UpdateCheck.Status == UpdateStatus.NO_UPDATE) {
					if (worker == null || (worker != null && !worker.IsAlive)) {
						if (!Tool.InternetCheck.isInternetAvailable)
							GUI.enabled = false;

						if (GUILayout.Button (new GUIContent ("Check For Update", EditorHelper.Icon_Check, "Checks for available updates of " + Util.Constants.ASSET_NAME))) {

							worker = new System.Threading.Thread (() => UpdateCheck.UpdateCheckForEditor (out updateText));
							worker.Start ();
						}

						GUI.enabled = true;
					} else {
						GUILayout.Label ("Checking... Please wait.", EditorStyles.boldLabel);
					}
				}

                Color fgColor = GUI.color;

				GUI.color = Color.yellow;

				if (UpdateCheck.Status == UpdateStatus.NO_UPDATE) {
					GUI.color = Color.green;
					GUILayout.Label (updateText);
				} else if (UpdateCheck.Status == UpdateStatus.UPDATE) {
					GUILayout.Label (updateText);

					if (GUILayout.Button (new GUIContent ("Download", "Visit the 'Unity AssetStore' to download the latest version."))) {
						Application.OpenURL (Util.Constants.ASSET_URL);
					}
				} else if (UpdateCheck.Status == UpdateStatus.UPDATE_PRO) {
					GUILayout.Label (updateText);

					if (GUILayout.Button (new GUIContent ("Upgrade", "Upgrade to the PRO-version in the 'Unity AssetStore'."))) {
						Application.OpenURL (Util.Constants.ASSET_PRO_URL);
					}
				} else if (UpdateCheck.Status == UpdateStatus.UPDATE_VERSION) {
					GUILayout.Label (updateText);

					if (GUILayout.Button (new GUIContent ("Upgrade", "Upgrade to the newer version in the 'Unity AssetStore'"))) {
						Application.OpenURL (Util.Constants.ASSET_CT_URL);
					}
				} else if (UpdateCheck.Status == UpdateStatus.DEPRECATED) {
					GUILayout.Label (updateText);

					if (GUILayout.Button (new GUIContent ("More Information", "Visit the 'crosstales'-site for more information."))) {
						Application.OpenURL (Util.Constants.ASSET_AUTHOR_URL);
					}
				} else {
					GUI.color = Color.cyan;
					GUILayout.Label(updateText);
				}

                GUI.color = fgColor;
            }
            EditorGUILayout.EndScrollView();

			EditorHelper.SeparatorUI();

            GUILayout.BeginHorizontal();
            {
                if (GUILayout.Button(new GUIContent("AssetStore", EditorHelper.Logo_Unity, "Visit the 'Unity AssetStore' website.")))
                {
                    Application.OpenURL(Util.Constants.ASSET_CT_URL);
                }

                if (GUILayout.Button(new GUIContent(Util.Constants.ASSET_AUTHOR, EditorHelper.Logo_CT, "Visit the '" + Util.Constants.ASSET_AUTHOR + "' website.")))
                {
                    Application.OpenURL(Util.Constants.ASSET_AUTHOR_URL);
                }
            }
            GUILayout.EndHorizontal();

            GUILayout.Space(6);
        }

        protected static void save()
        {

            Util.Config.Save();

            if (Util.Config.DEBUG)
                Debug.Log("Config data saved");
        }

        #endregion
    }
}
// © 2016-2017 crosstales LLC (https://www.crosstales.com)