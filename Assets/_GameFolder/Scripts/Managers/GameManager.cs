using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using JellyShiftClone.Controllers;
using DG.Tweening;

namespace JellyShiftClone.Managers
{
	public enum GameState
	{
		Start = 0,
		Playing = 1,
		End = 2,
		Reset = 3,
	}
	public class GameManager : MonoBehaviour
	{
		public static GameManager Instance { get; private set; }
		public GameState GameState { get; set; }

		public static Action OnGameStarted;
		public static Action<bool> OnGameEnd;
		public static Action OnGameReset;
		public static Action<int> OnDiamondScored;

		[SerializeField] private InputManager inputManager;
		[SerializeField] private PlayerMovementController playerMovementController;
		[SerializeField] private LevelManager levelManager;
		[SerializeField] private Player player;
		[SerializeField] private UiManager uiManager;

		private bool _isGameSuccessful;

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

		void Start()
		{
			GameInitialize();
		}


		private void GameInitialize()
		{
			levelManager.Initialize(playerMovementController);
			inputManager.Initialize(playerMovementController);
			uiManager.Initialize(inputManager, levelManager, playerMovementController);
			playerMovementController.Initialize();
			OnGameStart();
		}


		private void OnGameStart()
		{
			ChangeState(GameState.Start);
		}

		public void ResetGame()
		{
			ChangeState(GameState.Reset);
			OnGameStart();
		}

		public void EndGame(bool isSuccessful)
		{
			_isGameSuccessful = isSuccessful;
			ChangeState(GameState.End);
		}

		public void ChangeState(GameState gameState)
		{
			GameState = gameState;
			Debug.Log($"GameState:{gameState}");

			switch (gameState)
			{
				case GameState.Start:
					OnGameStarted?.Invoke();
					break;
				
				case GameState.Playing:
					break;
				
				case GameState.End:
					OnGameEnd?.Invoke(_isGameSuccessful);
					if (_isGameSuccessful)
					{
						IncreaseDiamondScore(3);
						PlayerPrefsManager.CurrentLevel++;
					}
					break;
				
				case GameState.Reset:
					OnGameReset?.Invoke();
					break;
			}
		}

		public void IncreaseDiamondScore(int score)
		{
			PlayerPrefsManager.DiamondScore += score;
			OnDiamondScored?.Invoke(PlayerPrefsManager.DiamondScore);
		}
	}
}

