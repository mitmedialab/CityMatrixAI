using UnityEditor;
using UnityEngine;
using UnityEditor.Callbacks;

namespace Crosstales.RTVoice.EditorExt
{
    /// <summary>BuildPostprocessor for Windows. Adds the TTS-wrapper to the build.</summary>
    public class BuildPostprocessor
    {

        [PostProcessBuildAttribute(1)]
        public static void OnPostprocessBuild(BuildTarget target, string pathToBuiltProject)
        {
            if (target == BuildTarget.StandaloneWindows || target == BuildTarget.StandaloneWindows64)
            {
                string dataPath = pathToBuiltProject.Substring(0, pathToBuiltProject.Length - 4) + "_Data/";

                if (Util.Config.ENFORCE_32BIT_WINDOWS)
                {
                    FileUtil.CopyFileOrDirectory(Application.dataPath + Util.Config.TTS_WINDOWS_EDITOR_x86, dataPath + "RTVoiceTTSWrapper.exe");
                }
                else
                {
                    FileUtil.CopyFileOrDirectory(Application.dataPath + Util.Config.TTS_WINDOWS_EDITOR, dataPath + "RTVoiceTTSWrapper.exe");
                }

                if (Util.Config.DEBUG)
                    Debug.Log("Wrapper copied to: " + dataPath);
            }
        }
    }
}
// © 2015-2017 crosstales LLC (https://www.crosstales.com)