using UnityEngine;
using UnityEditor;

namespace Crosstales.RTVoice.EditorExt
{
    /// <summary>Checks for updates of the asset.</summary>
    [InitializeOnLoad]
    public static class UpdateCheck
    {

        #region Variables

		public const string TEXT_NOT_CHECKED = "Not checked.";
        public const string TEXT_NO_UPDATE = "No update available - you are using the latest version.";

		public static UpdateStatus Status = UpdateStatus.NOT_CHECKED;

#if !UNITY_WSA || UNITY_EDITOR
        private static char[] splitChar = new char[] { ';' };

        private static System.Threading.Thread worker;
#endif

        #endregion

#if !UNITY_WSA || UNITY_EDITOR

        #region Constructor

        static UpdateCheck()
        {
            if (Util.Config.UPDATE_CHECK)
            {
                if (Tool.InternetCheck.isInternetAvailable)
                {
                    if (Util.Config.DEBUG)
                        Debug.Log("Updater enabled!");

                    string lastDate = EditorPrefs.GetString(Util.Constants.KEY_UPDATE_DATE);
                    string date = System.DateTime.Now.ToString("yyyyMMdd"); // every day
                    //string date = System.DateTime.Now.ToString("yyyyMMddmm"); // every minute (for tests)

                    if (!date.Equals(lastDate))
                    {
                        if (Util.Config.DEBUG)
                            Debug.Log("Checking for update...");

                        EditorPrefs.SetString(Util.Constants.KEY_UPDATE_DATE, date);

                        worker = new System.Threading.Thread(() => updateCheck());
                        worker.Start();
                    }
                    else
                    {
                        if (Util.Config.DEBUG)
                            Debug.Log("No update check needed.");
                    }
                }
                else
                {
                    if (Util.Config.DEBUG)
                        Debug.Log("No Internet available!");
                }
            }
            else
            {
                if (Util.Config.DEBUG)
                    Debug.Log("Updater disabled!");
            }
        }

        #endregion

#endif

        #region Static methods

        public static void UpdateCheckForEditor(out string result)
        {
            string[] data = readData();

			updateStatus (data);

			if (Status == UpdateStatus.UPDATE)
            {
                result = updateTextForEditor(data);
			}
			else if (Status == UpdateStatus.UPDATE_PRO)
			{
				result = updateProTextForEditor(data);
			}
			else if (Status == UpdateStatus.UPDATE_VERSION)
			{
				result = updateVersionTextForEditor(data);
			}
			else if (Status == UpdateStatus.DEPRECATED)
			{
				result = deprecatedTextForEditor(data);
			}
            else
            {
                result = TEXT_NO_UPDATE;
            }
        }

        #endregion


        #region Private methods

#if !UNITY_WSA || UNITY_EDITOR
        private static void updateCheck()
        {
            string[] data = readData();

			updateStatus (data);

			if (Status == UpdateStatus.UPDATE)
            {
                Debug.LogWarning(updateText(data));

                if (Util.Config.UPDATE_OPEN_UAS)
                {
                    Application.OpenURL(Util.Constants.ASSET_URL);
                }
            }
			else if (Status == UpdateStatus.UPDATE_PRO)
			{
				Debug.LogWarning(updateProText(data));

				if (Util.Config.UPDATE_OPEN_UAS)
				{
					Application.OpenURL(Util.Constants.ASSET_PRO_URL);
				}
			}
			else if (Status == UpdateStatus.UPDATE_VERSION)
			{
				Debug.LogWarning(updateVersionText(data));

				if (Util.Config.UPDATE_OPEN_UAS)
				{
					Application.OpenURL(Util.Constants.ASSET_CT_URL);
				}
			}
			else if (Status == UpdateStatus.DEPRECATED)
			{
				Debug.LogWarning(deprecatedText(data));

				if (Util.Config.UPDATE_OPEN_UAS)
				{
					Application.OpenURL(Util.Constants.ASSET_AUTHOR_URL);
				}
			}
            else
            {
                if (Util.Config.DEBUG)
                    Debug.Log("Asset is up-to-date.");
            }
        }

        private static string updateText(string[] data)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            if (data != null)
            {
                sb.Append(Util.Constants.ASSET_NAME);
                sb.Append(" - update found!");
                sb.Append(System.Environment.NewLine);
                sb.Append(System.Environment.NewLine);
                sb.Append("Your version:\t");
                sb.Append(Util.Constants.ASSET_VERSION);
                sb.Append(System.Environment.NewLine);
                sb.Append("New version:\t");
                sb.Append(data[2]);
                sb.Append(System.Environment.NewLine);
                sb.Append(System.Environment.NewLine);
                sb.AppendLine("Please download the new version from the UAS:");
                sb.AppendLine(Util.Constants.ASSET_URL);
            }

            return sb.ToString();
        }

		private static string updateProText(string[] data)
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder();

			if (data != null)
			{
				sb.Append(Util.Constants.ASSET_NAME);
				sb.Append(" is deprecated in favour of the PRO-version!");
				sb.Append(System.Environment.NewLine);
				sb.Append(System.Environment.NewLine);
				sb.AppendLine("Please consider an upgrade in the UAS:");
				sb.AppendLine(Util.Constants.ASSET_PRO_URL);
			}

			return sb.ToString();
		}

		private static string updateVersionText(string[] data)
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder();

			if (data != null)
			{
				sb.Append(Util.Constants.ASSET_NAME);
				sb.Append(" is deprecated in favour of an newer version!");
				sb.Append(System.Environment.NewLine);
				sb.Append(System.Environment.NewLine);
				sb.AppendLine("Please consider an upgrade in the UAS:");
				sb.AppendLine(Util.Constants.ASSET_CT_URL);
			}

			return sb.ToString();
		}

		private static string deprecatedText(string[] data)
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder();

			if (data != null)
			{
				sb.Append(Util.Constants.ASSET_NAME);
				sb.Append(" is deprecated!");
				sb.Append(System.Environment.NewLine);
				sb.Append(System.Environment.NewLine);
				sb.AppendLine("Please check the link for more information:");
				sb.AppendLine(Util.Constants.ASSET_AUTHOR_URL);
			}

			return sb.ToString();
		}
#endif

        private static string[] readData()
        {
            string[] data = null;

#if !UNITY_WSA || UNITY_EDITOR

            try
            {
                System.Net.ServicePointManager.ServerCertificateValidationCallback = Util.Helper.RemoteCertificateValidationCallback;

                using (System.Net.WebClient client = new Util.CTWebClient())
                {
                    string content = client.DownloadString(Util.Constants.ASSET_UPDATE_CHECK_URL);

                    foreach (string line in Util.Helper.SplitStringToLines(content))
                    {
                        if (line.StartsWith(Util.Constants.ASSET_UID.ToString()))
                        {
                            data = line.Split(splitChar, System.StringSplitOptions.RemoveEmptyEntries);

                            if (data != null && data.Length >= 3)
                            { //valid record?
                                break;
                            }
                            else
                            {
                                data = null;
                            }
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                Debug.LogError("Could not load update file: " + System.Environment.NewLine + ex);
            }

#endif
            return data;
        }

		private static void updateStatus(string[] data)
        {
            if (data != null)
            {
                int buildNumber;

                if (int.TryParse(data[1], out buildNumber))
                {
					if (buildNumber > Util.Constants.ASSET_BUILD) {
						Status = UpdateStatus.UPDATE;
					} else if (buildNumber == -100) {
						Status = UpdateStatus.UPDATE_PRO;
					} else if (buildNumber == -200) {
						Status = UpdateStatus.UPDATE_VERSION;
					} else if (buildNumber == -900) {
						Status = UpdateStatus.DEPRECATED;
					} else {
						Status = UpdateStatus.NO_UPDATE;
					}
                }
            }
        }

        private static string updateTextForEditor(string[] data)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            if (data != null)
            {
                sb.AppendLine("Update found!");
                sb.Append(System.Environment.NewLine);
                sb.Append("Your version:\t");
                sb.Append(Util.Constants.ASSET_VERSION);
                sb.Append(System.Environment.NewLine);
                sb.Append("New version:\t");
                sb.Append(data[2]);
                sb.Append(System.Environment.NewLine);
				sb.Append(System.Environment.NewLine);
				sb.AppendLine("Please download the new version from the UAS.");
            }

            return sb.ToString();
        }

		private static string updateProTextForEditor(string[] data)
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder();

			if (data != null)
			{
				sb.Append(Util.Constants.ASSET_NAME);
				sb.Append(" is deprecated in favour of the PRO-version!");
				sb.Append(System.Environment.NewLine);
				sb.Append(System.Environment.NewLine);
				sb.AppendLine("Please consider an upgrade in the UAS.");
			}

			return sb.ToString();
		}

		private static string updateVersionTextForEditor(string[] data)
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder();

			if (data != null)
			{
				sb.Append(Util.Constants.ASSET_NAME);
				sb.Append(" is deprecated in favour of an newer version!");
				sb.Append(System.Environment.NewLine);
				sb.Append(System.Environment.NewLine);
				sb.AppendLine("Please consider an upgrade in the UAS.");
			}

			return sb.ToString();
		}

		private static string deprecatedTextForEditor(string[] data)
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder();

			if (data != null)
			{
				sb.Append(Util.Constants.ASSET_NAME);
				sb.Append(" is deprecated!");
				sb.Append(System.Environment.NewLine);
				sb.Append(System.Environment.NewLine);
				sb.AppendLine("Please click below for more information.");
			}

			return sb.ToString();
		}

        #endregion
    }

	/// <summary>All possible update stati.</summary>
	public enum UpdateStatus {
		NOT_CHECKED,
		NO_UPDATE,
		UPDATE,
		UPDATE_PRO,
		UPDATE_VERSION,
		DEPRECATED
	}
}
// © 2016-2017 crosstales LLC (https://www.crosstales.com)