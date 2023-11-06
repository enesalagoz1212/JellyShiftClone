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
		public LevelScriptableObject levelData;
		private LevelScriptableObject _currentLevelData;

		public GameObject levels;
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
			_currentLevelData= levelData;
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
			CreateNextLevel();
			levels.gameObject.SetActive(true);
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

			int levelPrefabsLength = _currentLevelData.levelPrefabs.Length;
			var _currentLevelIndex = (PlayerPrefsManager.CurrentLevel % levelPrefabsLength);

			if (_currentLevelIndex == 0)
			{
				_currentLevelIndex = levelPrefabsLength;
			}

			GameObject nextLevelPrefab = _currentLevelData.levelPrefabs[_currentLevelIndex - 1];
			currentLevel = Instantiate(nextLevelPrefab, levels.transform);
		}

		public float ReturnPlayerProgress()
		{
			var top = (_playerMovementController.childTransform.position.z -_currentLevelData.playerStartTransform.z);
			var bottom = (_currentLevelData.playerExitTransform.z - _currentLevelData.playerStartTransform.z);
			return top / bottom;
		}
	}

}
