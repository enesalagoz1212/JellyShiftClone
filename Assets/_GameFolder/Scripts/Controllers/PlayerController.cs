using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JellyShiftClone.Managers;
using DG.Tweening;

namespace JellyShiftClone.Controllers
{
	public class PlayerController : MonoBehaviour
	{
		public float forwardSpeed;
		private Vector3 _initialPosition;
		private bool _canMove;

		private void OnEnable()
		{
			GameManager.OnGameStarted += OnGameStart;
			GameManager.OnGameReset += OnGameReset;
		}

		private void OnDisable()
		{
			GameManager.OnGameStarted -= OnGameStart;
			GameManager.OnGameReset -= OnGameReset;

		}
		public void Initialize()
		{

		}
		void Start()
		{

		}

		private void OnGameStart()
		{
			_initialPosition = transform.position;
		}

		private void OnGameReset()
		{
			DOVirtual.DelayedCall(1f, () =>
			{
				transform.position = _initialPosition;
			});
		}

		void Update()
		{
			switch (GameManager.Instance.GameState)
			{
				case GameState.Start:

					break;
				case GameState.Playing:
					_canMove = true;
					break;
				case GameState.End:
					_canMove = false;
					break;
				case GameState.Reset:
					_canMove = false;
					break;
				default:
					break;
			}

			if (_canMove)
			{
				MoveForward();
			}

		}

		private void MoveForward()
		{
			transform.position += transform.forward * forwardSpeed * Time.deltaTime;
		}



	}
}

