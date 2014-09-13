using UnityEngine;
using System.Collections;
using System.Reflection;

public class LocalizationManager
{
	protected static LocalizationManager mInstance = null;
	public static LocalizationManager Instance 
	{
		get 
		{
			if (Application.isEditor && !Application.isPlaying)
				return null;
			
            if (mInstance == null)
			{
				Debug.Log("LocalizationManager: Init gameStrings...");
				mInstance = new LocalizationManager();
				Localization.instance.currentLanguage = "Localization/English";
				
				CheckLocalization();
				Debug.Log("LocalizationManager: Init gameStrings completed");
            }
            return mInstance;
        }
	}

	public static string GetLocalizedString(string key)
	{
		if (Application.isPlaying)
			return Instance.GetLocalizedString_(key);
		else
			return string.Empty;
	}
	
	protected string GetLocalizedString_(string key)
	{
		string localizedString = Localization.instance.Get(key);
		if (localizedString.Equals(key))
			Debug.LogWarning("WARNING: LocalizationManager: Localized string not found: " + key);

		return localizedString;
	}
	
	public static void CheckLocalization()
	{
		FieldInfo[] fields = Instance.GetType().GetFields(BindingFlags.Static | BindingFlags.Public);
		foreach(FieldInfo field in fields)
		{
			if (field.FieldType == typeof(string))
			{
				string key = (string)field.GetValue(null);
				string localizedString = Localization.instance.Get(key);
				if (string.IsNullOrEmpty( localizedString ) || localizedString.Equals(key))
					Debug.LogWarning("WARNING: Localized string not found for key: \"" + key + "\" in Language: \"" + Localization.instance.currentLanguage + "\"");
			}
		}
	}


	/////////////////////////////////////// 
	/// 								///
	/// 	HISTORY COMMICS		 		///
	///									///
	///////////////////////////////////////
	
	public static string HistoryComics_Page01_SubTitle = "HistoryComics_Page01_SubTitle";
		
}