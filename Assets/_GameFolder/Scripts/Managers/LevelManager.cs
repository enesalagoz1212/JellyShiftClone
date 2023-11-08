using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JellyShiftClone.Controllers;
using JellyShiftClone.ScriptableObjects;

namespace JellyShiftClone.Managers
{
	public class LevelManager : MonoBehaviour
	{
		public static LevelManager Instance { get; private set; }

		private PlayerMovementController _playerMovementController;
		public LevelScriptableObject[] levels;
		private LevelScriptableObject _currentLevelData;
		private int _currentLevelIndex = 0;

		public GameObject levelContainer;
		private GameObject currentLevel;

		public void Initialize(PlayerMovementController playerMovementController)
		{
			_playerMovementController = playerMovementController;
		}

		private void Awake()
		{
			if (Instance != null && Instance != this)
			{
				Destroy(this);
			}
			else
			{
				Instance = this;
			}
		
		}


		private void OnEnable()
		{
			GameManager.OnGameStarted += OnGameStart;
			GameManager.OnGameEnd += OnGameEnd;
		}

		private void OnDisable()
		{
			GameManager.OnGameStarted -= OnGameStart;
			GameManager.OnGameEnd -= OnGameEnd;
		}



		private void OnGameStart()
		{
			int currentLevelData = PlayerPrefsManager.CurrentLevel;
			int moodCurrentLevel = currentLevelData % levels.Length;
			if (moodCurrentLevel==0)
			{
				moodCurrentLevel = levels.Length;
			}
			_currentLevelData = levels[moodCurrentLevel-1];

			CreateNextLevel();
			levelContainer.gameObject.SetActive(true);
		}

		private void OnGameEnd(bool isSuccessful)
		{

		}

		public LevelScriptableObject GetLevelData()
		{
			return _currentLevelData;
		}

		public void CreateNextLevel()
		{
			if (currentLevel != null)
			{
				Destroy(currentLevel);
			}

			int levelIndex = PlayerPrefsManager.CurrentLevel;
			int mood = levelIndex % levels.Length;

			if (mood==0)
			{
				mood = levels.Length;
			}

			LevelScriptableObject currentLevelData = levels[mood-1];
			GameObject nextLevelPrefab = currentLevelData.levelPrefab;
			currentLevel = Instantiate(nextLevelPrefab, levelContainer.transform);

		}

		public float ReturnPlayerProgress()
		{
			var top = (_playerMovementController.childTransform.position.z -_currentLevelData.playerStartTransform.z);
			var bottom = (_currentLevelData.playerExitTransform.z - _currentLevelData.playerStartTransform.z);
			return top / bottom;
			
		}
	}

}
