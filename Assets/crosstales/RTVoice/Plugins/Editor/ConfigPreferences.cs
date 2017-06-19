using UnityEditor;
using UnityEngine;

namespace Crosstales.RTVoice.EditorExt
{
    /// <summary>Unity "Preferences" extension.</summary>
    public class ConfigPreferences : ConfigBase
    {

        #region Variables

        private static int tab = 0;
        private static int lastTab = 0;

        #endregion


        #region Static methods

        [PreferenceItem(Util.Constants.ASSET_NAME)]
        private static void PreferencesGUI()
        {

            tab = GUILayout.Toolbar(tab, new string[] { "Configuration", "Help", "About" });

            if (tab != lastTab)
            {
                lastTab = tab;
                GUI.FocusControl(null);
            }

            if (tab == 0)
            {
                showConfiguration();

                EditorHelper.SeparatorUI();

                if (GUILayout.Button(new GUIContent("Reset", EditorHelper.Icon_Reset, "Resets the configuration settings for this project.")))
                {
                    if (EditorUtility.DisplayDialog("Reset configuration?", "Reset the configuration of " + Util.Constants.ASSET_NAME + "?", "Yes", "No"))
                    {
                        Util.Config.Reset();
                        save();
                    }
                }

                GUILayout.Space(6);
            }
            else if (tab == 1)
            {
                showHelp();
            }
            else
            {
                showAbout();
            }

            if (GUI.changed)
            {
                save();
            }
        }

        #endregion
    }
}
// © 2016-2017 crosstales LLC (https://www.crosstales.com)