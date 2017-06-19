using UnityEngine;
using UnityEditor;

namespace Crosstales.RTVoice.EditorExt
{
    /// <summary>Loads the configuration of the asset.</summary>
    [InitializeOnLoad]
    public static class ConfigLoader
    {

        #region Constructor

        static ConfigLoader()
        {
            Util.Config.Load();

            if (Util.Config.DEBUG)
                Debug.Log("Config data loaded");
        }

        #endregion
    }
}
// © 2016-2017 crosstales LLC (https://www.crosstales.com)