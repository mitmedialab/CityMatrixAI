using UnityEngine;
using UnityEditor;

namespace Crosstales.RTVoice.EditorExt
{
    /// <summary>Custom editor for the 'Proxy'-class.</summary>
    [InitializeOnLoad]
    [CustomEditor(typeof(Tool.Proxy))]
    public class ProxyEditor : Editor
    {

        #region Variables

        private Tool.Proxy script;

        #endregion


        #region Editor methods

        public void OnEnable()
        {
            script = (Tool.Proxy)target;
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            EditorHelper.SeparatorUI();

            if (script.isActiveAndEnabled)
            {
                if (Util.Helper.isEditorMode)
                {
                    GUILayout.Label("HTTP-Proxy:", EditorStyles.boldLabel);

                    if (Tool.Proxy.hasHTTPProxy)
                    {
                        if (GUILayout.Button(new GUIContent("Disable", EditorHelper.Icon_Minus, "Disable HTTP-Proxy.")))
                        {
                            script.DisableHTTPProxy();
                        }
                    }
                    else
                    {
                        if (GUILayout.Button(new GUIContent("Enable", EditorHelper.Icon_Plus, "Enable HTTP-Proxy.")))
                        {
                            script.EnableHTTPProxy();
                        }
                    }

                    GUILayout.Space(8);

                    GUILayout.Label("HTTPS-Proxy:", EditorStyles.boldLabel);

                    if (Tool.Proxy.hasHTTPSProxy)
                    {
                        if (GUILayout.Button(new GUIContent("Disable", EditorHelper.Icon_Minus, "Disable HTTPS-Proxy.")))
                        {
                            script.DisableHTTPSProxy();
                        }
                    }
                    else
                    {
                        if (GUILayout.Button(new GUIContent("Enable", EditorHelper.Icon_Plus, "Enable HTTPS-Proxy.")))
                        {
                            script.EnableHTTPSProxy();
                        }
                    }
                }
                else
                {
                    GUILayout.Label("Disabled in Play-mode!");
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