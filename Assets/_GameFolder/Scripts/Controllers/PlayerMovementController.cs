using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JellyShiftClone.Managers;
using DG.Tweening;

namespace JellyShiftClone.Controllers
{
	public enum PlayerState
	{
		Idle,
		forward,
		hit,
		Jumping,
		Falling,
	}

	public class PlayerMovementController : MonoBehaviour
	{
		public float forwardSpeed;
		private Vector3 _initialPosition;

		private bool _canMove;
		public bool CanMove
		{
			get { return _canMove; }
			set { _canMove = value; }
		}

		private Vector3 _initialScale;

		public float lerpValue;
		public float minX;
		public float maxX;
		public float minY;
		public float maxY;


		private bool isMoving = false;
		private bool isBouncing = false;
		public Transform childTransform;
		private void OnEnable()
		{
			GameManager.OnGameStarted += OnGameStart;
			GameManager.OnGameReset += OnGameReset;
			GameManager.OnGameEnd += OnGameEnd;

		}

		private void OnDisable()
		{
			GameManager.OnGameStarted -= OnGameStart;
			GameManager.OnGameReset -= OnGameReset;
			GameManager.OnGameEnd -= OnGameEnd;
		}
		public void Initialize()
		{

		}

		private void OnGameStart()
		{
			_initialPosition = transform.position;
			_initialScale = transform.localScale;
		}

		private void OnGameReset()
		{
			transform.position = _initialPosition;
			
		}

		private void OnGameEnd(bool isSuccessful)
		{
			if (isSuccessful)
			{
				transform.localScale = _initialScale;
			}
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

		private void MoveForward()
		{
			transform.position += transform.forward * forwardSpeed * Time.deltaTime;
		}

		public void ChangeScale(float deltaY)
		{
			if (InputManager.Instance.isInputEnabled)
			{
				float newYScale = transform.localScale.y + deltaY * 0.01f;
				float newXScale = transform.localScale.x - deltaY * 0.01f;

				newXScale = transform.localScale.x + deltaY * 0.01f;
				newYScale = transform.localScale.y - deltaY * 0.01f;

				newYScale = Mathf.Clamp(newYScale, minY, maxY);
				newXScale = Mathf.Clamp(newXScale, minX, maxX);

				transform.localScale = new Vector3(newXScale, newYScale, transform.localScale.z);
			}
		}
		
		private void OnCollisionEnter(Collision collision)
		{
			if (collision.gameObject.CompareTag("Obstacle") && !isBouncing)
			{
				isBouncing = true;
				Vector3 originalPosition = transform.position;
				Vector3 bounceBackPosition = new Vector3(originalPosition.x, originalPosition.y, originalPosition.z - 6f);
				float bounceBackDuration = 0.2f;

				transform.DOMove(bounceBackPosition, bounceBackDuration).SetEase(Ease.InOutQuad).OnComplete(() =>
				{
					DOVirtual.DelayedCall(0.2f, () =>
					{
						isBouncing = false;
					});
				});
			}
		}
	}
}