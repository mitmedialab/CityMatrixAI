using UnityEngine;
using UnityEditor;

namespace Crosstales.RTVoice.EditorExt
{
    /// <summary>Editor component for the "Hierarchy"-menu.</summary>
	public class RTVoiceGameObject : MonoBehaviour
    {

		[MenuItem("GameObject/" + Util.Constants.ASSET_NAME + "/" + Util.Constants.RTVOICE_SCENE_OBJECT_NAME, false, EditorHelper.GO_ID)]
        private static void AddRTVoice()
        {
			EditorHelper.InstantiatePrefab(Util.Constants.RTVOICE_SCENE_OBJECT_NAME);
        }

		[MenuItem("GameObject/" + Util.Constants.ASSET_NAME + "/" + Util.Constants.RTVOICE_SCENE_OBJECT_NAME, true)]
        private static bool AddRTVoiceValidator()
        {
            return !EditorHelper.isRTVoiceInScene;
        }

		[MenuItem("GameObject/" + Util.Constants.ASSET_NAME + "/SpeechText", false, EditorHelper.GO_ID + 1)]
        private static void AddSpeechText()
        {
            EditorHelper.InstantiatePrefab("SpeechText");
        }

		[MenuItem("GameObject/" + Util.Constants.ASSET_NAME + "/Sequencer", false, EditorHelper.GO_ID + 2)]
        private static void AddSequencer()
        {
            EditorHelper.InstantiatePrefab("Sequencer");
        }

		[MenuItem("GameObject/" + Util.Constants.ASSET_NAME + "/TextFileSpeaker", false, EditorHelper.GO_ID + 3)]
        private static void AddTextFileSpeaker()
        {
            EditorHelper.InstantiatePrefab("TextFileSpeaker");
        }

		[MenuItem("GameObject/" + Util.Constants.ASSET_NAME + "/Loudspeaker", false, EditorHelper.GO_ID + 4)]
        private static void AddLoudspeaker()
        {
            EditorHelper.InstantiatePrefab("Loudspeaker");
        }

		[MenuItem("GameObject/" + Util.Constants.ASSET_NAME + "/" + Util.Constants.INTERNETCHECK_SCENE_OBJECT_NAME, false, EditorHelper.GO_ID + 5)]
		private static void AddInternetCheck()
		{
			EditorHelper.InstantiatePrefab(Util.Constants.INTERNETCHECK_SCENE_OBJECT_NAME);
		}

		[MenuItem("GameObject/" + Util.Constants.ASSET_NAME + "/" + Util.Constants.INTERNETCHECK_SCENE_OBJECT_NAME, true)]
		private static bool AddInternetCheckValidator()
		{
			return !EditorHelper.isInternetCheckInScene;
		}

		[MenuItem("GameObject/" + Util.Constants.ASSET_NAME + "/" + Util.Constants.PROXY_SCENE_OBJECT_NAME, false, EditorHelper.GO_ID + 6)]
        private static void AddProxy()
        {
			EditorHelper.InstantiatePrefab(Util.Constants.PROXY_SCENE_OBJECT_NAME);
        }

		[MenuItem("GameObject/" + Util.Constants.ASSET_NAME + "/" + Util.Constants.PROXY_SCENE_OBJECT_NAME, true)]
        private static bool AddProxyValidator()
        {
            return !EditorHelper.isProxyInScene;
        }
    }
}
// © 2017 crosstales LLC (https://www.crosstales.com)
