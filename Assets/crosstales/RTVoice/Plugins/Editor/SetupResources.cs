using UnityEngine;
using UnityEditor;

namespace Crosstales.RTVoice.EditorExt
{
    /// <summary>Copies all resources to 'Editor Default Resources'.</summary>
    [InitializeOnLoad]
    public static class SetupResources
    {

        #region Constructor

        static SetupResources()
        {

#if !rtv_ignore_setup

            string path = Application.dataPath + "/";
            string assetpath = path + "crosstales/RTVoice/";
            string source = assetpath + "Icons/";
            string target = path + "Editor Default Resources/RTVoice/";
            string metafile = assetpath + "Icons.meta";

            try
            {
                if (System.IO.Directory.Exists(source))
                {
                    if (!System.IO.Directory.Exists(target))
                    {
                        System.IO.Directory.CreateDirectory(target);
                    }

                    var dirSource = new System.IO.DirectoryInfo(source);

                    foreach (var file in dirSource.GetFiles("*"))
                    {
                        if (Util.Config.DEBUG)
                            Debug.Log("File moved: " + file);

                        //file.CopyTo(target + file.Name);
                        file.MoveTo(target + file.Name);
                    }

                    dirSource.Delete();

                    if (System.IO.File.Exists(metafile))
                    {
                        System.IO.File.Delete(metafile);
                    }

                    AssetDatabase.Refresh();
                }
            }
            catch (System.Exception ex)
            {
                Debug.LogError("Could not move all files: " + ex);
            }
#endif
        }

        #endregion
    }
}
// © 2016-2017 crosstales LLC (https://www.crosstales.com)