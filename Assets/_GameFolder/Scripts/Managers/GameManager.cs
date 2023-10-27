using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using JellyShiftClone.Controllers;

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

		[SerializeField] private InputManager inputManager;
		[SerializeField] private PlayerController playerController;
		[SerializeField] private LevelManager levelManager;
		[SerializeField] private Player player;
		[SerializeField] private UiManager uiManager;

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
			levelManager.Initialize();
			inputManager.Initialize(player);
			uiManager.Initialize(inputManager);
			playerController.Initialize();
			OnGameStart();
		}


		private void OnGameStart()
		{
			ChangeState(GameState.Start);
		}

		public void ResetGame()
		{

		}

		public void EndGame(bool isSuccessful)
		{
			ChangeState(GameState.End);
			
		}

		public void ChangeState(GameState gameState, bool isSuccessful = false)
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
					if (isSuccessful)
					{
						PlayerPrefsManager.CurrentLevel++;
					}
					else
					{

					}
					break;
				case GameState.Reset:
					break;
				default:
					break;
			}
		}
	}
}

