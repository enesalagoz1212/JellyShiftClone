using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JellyShiftClone.Managers
{
	public class PlayerPrefsManager
	{
		private const string CurrentLevelKey = "CurrentLevel";
		private const string HapticKey = "IsHapticOn";
		private const string SoundKey = "IsSoundOn";
		public static int CurrentLevel
		{
			get
			{
				return PlayerPrefs.GetInt(CurrentLevelKey, 1);
			}
			set
			{
				PlayerPrefs.SetInt(CurrentLevelKey, value);
			}
		}

		public static bool IsHapticOn
		{
			get
			{
				if (PlayerPrefs.HasKey(HapticKey))
				{
					return bool.Parse(PlayerPrefs.GetString(HapticKey));
				}
				return true;
			}
			set => PlayerPrefs.SetString(HapticKey, value.ToString());
		}


		public static bool IsSoundOn
		{
			get
			{
				if (PlayerPrefs.HasKey(SoundKey))
				{
					return bool.Parse(PlayerPrefs.GetString(SoundKey));
				}
				return true;
			}
			set => PlayerPrefs.SetString(SoundKey, value.ToString());
		}
	}
}

