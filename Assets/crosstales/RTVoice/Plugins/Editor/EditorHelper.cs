using UnityEngine;
using UnityEditor;

namespace Crosstales.RTVoice.EditorExt
{
    /// <summary>Editor helper class.</summary>
    public static class EditorHelper
    {
        #region Static variables

        /// <summary>Start index inside the "GameObject"-menu.</summary>
        public const int GO_ID = 20;

        /// <summary>Start index inside the "Tools"-menu.</summary>
        public const int MENU_ID = 2000;

        private static Texture2D logo_asset;
        private static Texture2D logo_asset_small;
        private static Texture2D logo_ct;
        private static Texture2D logo_unity;

        private static Texture2D icon_save;
        private static Texture2D icon_reset;
        private static Texture2D icon_plus;
		private static Texture2D icon_minus;
        private static Texture2D icon_refresh;
        private static Texture2D icon_speak;
        private static Texture2D icon_silence;

        private static Texture2D icon_manual;
        private static Texture2D icon_api;
        private static Texture2D icon_forum;
        private static Texture2D icon_product;

        private static Texture2D icon_check;

        private static Texture2D store_PlayMaker;
        private static Texture2D store_AdventureCreator;
        private static Texture2D store_CinemaDirector;
        private static Texture2D store_DialogueSystem;
        private static Texture2D store_LDC;
        private static Texture2D store_LipSync;
        private static Texture2D store_NPC_Chat;
        private static Texture2D store_QuestSystem;
        private static Texture2D store_SALSA;
        private static Texture2D store_SLATE;
        private static Texture2D store_THE_Dialogue_Engine;
        private static Texture2D store_uSequencer;

        private static Texture2D icon_3p_assets;

        #endregion


        #region Static properties

        public static Texture2D Logo_Asset
        {
            get
            {
                if (Util.Constants.isPro)
                {
                    return loadImage(ref logo_asset, "logo_asset_pro.png");
                }
                else
                {
                    return loadImage(ref logo_asset, "logo_asset.png");
                }
            }
        }

        public static Texture2D Logo_Asset_Small
        {
            get
            {
                if (Util.Constants.isPro)
                {
                    return loadImage(ref logo_asset_small, "logo_asset_small_pro.png");
                }
                else
                {
                    return loadImage(ref logo_asset_small, "logo_asset_small.png");
                }
            }
        }

        public static Texture2D Logo_CT
        {
            get
            {
                return loadImage(ref logo_ct, "logo_ct.png");
            }
        }

        public static Texture2D Logo_Unity
        {
            get
            {
                return loadImage(ref logo_unity, "logo_unity.png");
            }
        }

        public static Texture2D Icon_Save
        {
            get
            {
                return loadImage(ref icon_save, "icon_save.png");
            }
        }

        public static Texture2D Icon_Reset
        {
            get
            {
                return loadImage(ref icon_reset, "icon_reset.png");
            }
        }

        public static Texture2D Icon_Plus
        {
            get
            {
                return loadImage(ref icon_plus, "icon_plus.png");
            }
        }

		public static Texture2D Icon_Minus
		{
			get
			{
				return loadImage(ref icon_minus, "icon_minus.png");
			}
		}

        public static Texture2D Icon_Refresh
        {
            get
            {
                return loadImage(ref icon_refresh, "icon_refresh.png");
            }
        }

        public static Texture2D Icon_Speak
        {
            get
            {
                return loadImage(ref icon_speak, "icon_speak.png");
            }
        }

        public static Texture2D Icon_Silence
        {
            get
            {
                return loadImage(ref icon_silence, "icon_silence.png");
            }
        }

        public static Texture2D Icon_Manual
        {
            get
            {
                return loadImage(ref icon_manual, "icon_manual.png");
            }
        }

        public static Texture2D Icon_API
        {
            get
            {
                return loadImage(ref icon_api, "icon_api.png");
            }
        }

        public static Texture2D Icon_Forum
        {
            get
            {
                return loadImage(ref icon_forum, "icon_forum.png");
            }
        }

        public static Texture2D Icon_Product
        {
            get
            {
                return loadImage(ref icon_product, "icon_product.png");
            }
        }

        public static Texture2D Icon_Check
        {
            get
            {
                return loadImage(ref icon_check, "icon_check.png");
            }
        }

        public static Texture2D Store_PlayMaker
        {
            get
            {
                return loadImage(ref store_PlayMaker, "store_PlayMaker.png");
            }
        }

        public static Texture2D Store_AdventureCreator
        {
            get
            {
                return loadImage(ref store_AdventureCreator, "store_AdventureCreator.png");
            }
        }

        public static Texture2D Store_CinemaDirector
        {
            get
            {
                return loadImage(ref store_CinemaDirector, "store_CinemaDirector.png");
            }
        }

        public static Texture2D Store_DialogueSystem
        {
            get
            {
                return loadImage(ref store_DialogueSystem, "store_DialogueSystem.png");
            }
        }

        public static Texture2D Store_LDC
        {
            get
            {
                return loadImage(ref store_LDC, "store_LDC.png");
            }
        }

        public static Texture2D Store_LipSync
        {
            get
            {
                return loadImage(ref store_LipSync, "store_LipSync.png");
            }
        }

        public static Texture2D Store_NPC_Chat
        {
            get
            {
                return loadImage(ref store_NPC_Chat, "store_NPC_Chat.png");
            }
        }

        public static Texture2D Store_QuestSystem
        {
            get
            {
                return loadImage(ref store_QuestSystem, "store_QuestSystem.png");
            }
        }

        public static Texture2D Store_SALSA
        {
            get
            {
                return loadImage(ref store_SALSA, "store_SALSA.png");
            }
        }

        public static Texture2D Store_SLATE
        {
            get
            {
                return loadImage(ref store_SLATE, "store_SLATE.png");
            }
        }

        public static Texture2D Store_THE_Dialogue_Engine
        {
            get
            {
                return loadImage(ref store_THE_Dialogue_Engine, "store_THE_Dialogue_Engine.png");
            }
        }

        public static Texture2D Store_uSequencer
        {
            get
            {
                return loadImage(ref store_uSequencer, "store_uSequencer.png");
            }
        }

        public static Texture2D Icon_3p_Assets
        {
            get
            {
                return loadImage(ref icon_3p_assets, "icon_3p_assets.png");
            }
        }

        #endregion


        #region Static methods

        /// <summary>Shows the "no voices found"-UI.</summary>
        public static void NoVoicesUI()
        {
            Color guiColor = GUI.color;

            GUILayout.Label("Could Not Load Voices!", EditorStyles.boldLabel);

            if (isRTVoiceInScene)
            {
                GUI.color = Color.red;
                GUILayout.Label("TTS on your machine is not possible.");
                GUI.color = guiColor;
            }
            else
            {
                GUI.color = Color.yellow;
				GUILayout.Label("Did you add the '" + Util.Constants.RTVOICE_SCENE_OBJECT_NAME + "'-prefab to the scene?");
                GUI.color = guiColor;

                GUILayout.Space(8);

				if (GUILayout.Button(new GUIContent("Add RTVoice", Icon_Plus, "Add the '" + Util.Constants.RTVOICE_SCENE_OBJECT_NAME + "'-prefab to the current scene.")))
                {
					InstantiatePrefab(Util.Constants.RTVOICE_SCENE_OBJECT_NAME);
                }
            }
        }

        /// <summary>Shows a separator-UI.</summary>
		/// <param name="space">Space in pixels between the component and the seperator line (default: 12, optional).</param>
        public static void SeparatorUI(int space = 12)
        {
            GUILayout.Space(space);
            GUILayout.Box(string.Empty, new GUILayoutOption[] { GUILayout.ExpandWidth(true), GUILayout.Height(1) });
        }

        /// <summary>Instantiates a prefab.</summary>
        /// <param name="prefabName">Name of the prefab.</param>
        public static void InstantiatePrefab(string prefabName)
        {
            PrefabUtility.InstantiatePrefab(AssetDatabase.LoadAssetAtPath("Assets" + Util.Config.PREFAB_PATH + prefabName + ".prefab", typeof(GameObject)));
        }

        /// <summary>Checks if the 'RTVoice'-prefab is in the scene.</summary>
        /// <returns>True if the 'RTVoice'-prefab is in the scene.</returns>
        public static bool isRTVoiceInScene
        {
            get
            {
                return GameObject.Find(Util.Constants.RTVOICE_SCENE_OBJECT_NAME) != null;
            }
        }


		/// <summary>Checks if the 'InternetCheck'-prefab is in the scene.</summary>
		/// <returns>True if the 'InternetCheck'-prefab is in the scene.</returns>
		public static bool isInternetCheckInScene
		{
			get
			{
				return GameObject.Find(Util.Constants.INTERNETCHECK_SCENE_OBJECT_NAME) != null;
			}
		}

        /// <summary>Checks if the 'Proxy'-prefab is in the scene.</summary>
        /// <returns>True if the 'Proxy'-prefab is in the scene.</returns>
        public static bool isProxyInScene
        {
            get
            {
				return GameObject.Find(Util.Constants.PROXY_SCENE_OBJECT_NAME) != null;
            }
        }

        /// <summary>Loads an image as Texture2D from 'Editor Default Resources'.</summary>
        /// <param name="logo">Logo to load.</param>
        /// <param name="fileName">Name of the image.</param>
        /// <returns>Image as Texture2D from 'Editor Default Resources'.</returns>
        private static Texture2D loadImage(ref Texture2D logo, string fileName)
        {
            if (logo == null)
            {
#if rtv_ignore_setup
                logo = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets" + Util.Config.ASSET_PATH + "Icons/" + fileName, typeof(Texture2D));
#else
                logo = (Texture2D)EditorGUIUtility.Load("RTVoice/" + fileName);
#endif

                if (logo == null)
                {
                    Debug.LogWarning("Image not found: " + fileName);
                }
            }

            return logo;
        }
        #endregion
    }
}
// © 2016-2017 crosstales LLC (https://www.crosstales.com)