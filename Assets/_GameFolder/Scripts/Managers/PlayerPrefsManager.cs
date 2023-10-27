using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JellyShiftClone.Managers
{
	public class PlayerPrefsManager
	{
		private const string CurrentLevelKey = "CurrentLevel";

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

	}
}

