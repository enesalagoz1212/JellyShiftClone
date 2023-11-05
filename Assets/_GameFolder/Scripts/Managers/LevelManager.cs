using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JellyShiftClone.Controllers;

namespace JellyShiftClone.Managers
{
	public class LevelManager : MonoBehaviour
	{
		public static LevelManager Instance { get; private set; }

		private PlayerController _playerController;

		public GameObject levels;
		public GameObject[] levelPrefabs;

		private GameObject currentLevel;
		private int _currentLevelIndex;

		private float _firstPlatformPositionZ = 17.7f;
		private float _lastPlatformPositionZ = 210f;
		public void Initialize(PlayerController playerController)
		{
			_playerController = playerController;
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
			CreateNextLevel();
			levels.gameObject.SetActive(true);
		}

		private void OnGameEnd(bool isSuccessful)
		{

		}

		public void CreateNextLevel()
		{
			if (currentLevel != null)
			{
				Destroy(currentLevel);
			}

			int levelPrefabsLength = levelPrefabs.Length;
			var _currentLevelIndex = (PlayerPrefsManager.CurrentLevel % levelPrefabsLength);

			if (_currentLevelIndex == 0)
			{
				_currentLevelIndex = levelPrefabsLength;
			}

			GameObject nextLevelPrefab = levelPrefabs[_currentLevelIndex - 1];
			currentLevel = Instantiate(nextLevelPrefab, levels.transform);
		}

		public float ReturnPlayerProgress()
		{
			var top = (_playerController.childTransform.position.z - _firstPlatformPositionZ);
			var bottom = (_lastPlatformPositionZ - _firstPlatformPositionZ);
			return top / bottom;
		}
	}

}
