using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JellyShiftClone.Managers;
using DG.Tweening;

namespace JellyShiftClone.Controllers
{
	public enum PlayerState
	{
		Idle = 0,
		Forward = 1,
		Hit = 2,
		Jumping = 3,
		Falling = 4,
	}

	public class PlayerMovementController : MonoBehaviour
	{
		public PlayerState PlayerState { get; set; }

		public Ease rotationEaseType;
		public float forwardSpeed;
		private Vector3 _initialPosition;
		private Vector3 _initialScale;

		private bool _canMove = false;
		public float lerpValue;
		public float minX;
		public float maxX;
		public float minY;
		public float maxY;

		private bool isBouncing = false;
		private bool isJump = true;
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
			isJump = true;
			_canMove = true;

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
				ChangeState(PlayerState.Jumping);
			}
		}

		public void ChangeState(PlayerState playerState)
		{
			PlayerState = playerState;
			Debug.Log($"PlayerState: {playerState} ");

			switch (playerState)
			{
				case PlayerState.Idle:
					break;
				case PlayerState.Forward:
					MoveForward();
					break;
				case PlayerState.Hit:
					// obstacle leri carpma animasyonu ekle
					break;
				case PlayerState.Jumping:
					// oyun sonu zýplama animasyonu
					Rotate();
					break;
				case PlayerState.Falling:
					// oyun esnasýnda harekt ederken yere düsme icin gerekli kisim
					break;
				default:
					break;
			}
		}

		private void Update()
		{
			if (GameManager.Instance.GameState == GameState.Playing && _canMove == true)
			{
				ChangeState(PlayerState.Forward);
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
		private void OnTriggerEnter(Collider other)
		{

			if (other.gameObject.CompareTag("Obstacle") && !isBouncing)
			{
				ChangeState(PlayerState.Hit);
				_canMove = false;
				isBouncing = true;
				Vector3 originalPosition = transform.position;
				Vector3 bounceBackPosition = new Vector3(originalPosition.x, originalPosition.y, originalPosition.z - 6f);
				float bounceBackDuration = 0.2f;

				transform.DOMove(bounceBackPosition, bounceBackDuration).SetEase(Ease.InOutQuad).OnComplete(() =>
				{
					DOVirtual.DelayedCall(0.2f, () =>
					{
						isBouncing = false;
						_canMove = true;
						ChangeState(PlayerState.Forward);
					});
				});
			}
		}

		private void Rotate()
		{

			if (isJump)
			{

				Vector3 originalPosition = transform.position;
				float duration = 1.5f;
				float forwardDistance = 10f;
				float rotationAmount = 360f;

				transform.DOMoveZ(originalPosition.z + forwardDistance, duration).SetEase(rotationEaseType);

				
				float customRotationAmount = 180f;
				transform.DORotate(new Vector3(customRotationAmount, 0f, 0f), duration, RotateMode.FastBeyond360).SetEase(Ease.Linear);

				isJump = false;
		

			}
		}
	}
}
