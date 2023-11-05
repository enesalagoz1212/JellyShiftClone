using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JellyShiftClone.Managers;
using JellyShiftClone.Controllers;
using DG.Tweening;

namespace JellyShiftClone.Controllers
{
	public class Player : MonoBehaviour
	{
		private Camera _camera;
		private Vector3 _initialScale;

		public float lerpValue;
		public float minX;
		public float maxX;
		public float minY;
		public float maxY;


		private bool isMoving = false;
		private bool isBouncing = false;
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

		private void Start()
		{
			
		}

		private void OnGameStart()
		{
			_initialScale = transform.localScale;
		}

		private void OnGameReset()
		{
			transform.localScale = _initialScale;
		}

		private void OnGameEnd(bool isSuccessful)
		{
			if (!isSuccessful)
			{
				transform.localScale = _initialScale;
				Vector3 targetPosition = new Vector3(0, 0, 220);
				FlyAndRotateToTarget(targetPosition);
			}
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
					isBouncing = false;
				});
			}
		}


		private void FlyAndRotateToTarget(Vector3 targetPosition)
		{
			if (isMoving)
			{
				return;
			}

			float rotationAngleX = 360f;

			float forwardDistance = Vector3.Distance(transform.position, targetPosition);

			isMoving = true;

			transform.DOMove(targetPosition, 1f)
				.SetEase(Ease.Linear)
				.OnStart(() =>
				{
					transform.DORotate(new Vector3(rotationAngleX, 0, 0), 1f, RotateMode.FastBeyond360)
						.SetEase(Ease.Linear)
						.OnComplete(() =>
						{

							isMoving = false;
						});
				});
		}




	}

}
