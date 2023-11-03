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
		public bool CanMove
		{
			get { return _canMove; }
			set { _canMove = value; }
		}

		public Transform childTransform;
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
				transform.position = _initialPosition;
		
		}

		void Update()
		{
			switch (GameManager.Instance.GameState)
			{
				case GameState.Start:
					CanMove = false;
					break;
				case GameState.Playing:
					CanMove = true;
					break;
				case GameState.End:
					CanMove = false;
					break;
				case GameState.Reset:
					CanMove = false;
					break;
				default:
					break;
			}

			if (_canMove)
			{
				MoveForward();
			}

		}

		public void TrueCanMove()
		{
			_canMove = true;
		}

		public void FalseCanMove()
		{
			_canMove = false;
		}

		private void MoveForward()
		{
			transform.position += transform.forward * forwardSpeed * Time.deltaTime;
		}



	}
}

