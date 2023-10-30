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
			_camera = Camera.main;

			if (_camera == null)
			{
				Debug.LogError("Camera null");
			}
		}

		private void OnGameStart()
		{
			_initialScale = transform.localScale;
		}

		private void OnGameReset()
		{

		}

		private void OnGameEnd(bool isSuccessful)
		{
			if (!isSuccessful)
			{
				transform.localScale = _initialScale;
				Vector3 targetPosition = new Vector3(0, 0, 230);
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
			if (collision.gameObject.CompareTag("Obstacle"))
			{
				Debug.Log("Obstacle temas oldu");
			}
		}

		private bool isMoving = false; // Hareket kontrol�

		private void FlyAndRotateToTarget(Vector3 targetPosition)
		{
			if (isMoving) // E�er Player zaten hareket halindeyse i�lem yapma
			{
				return;
			}

			float rotationAngleX = 360f; // X ekseni etraf�nda 360 derece d�nme

			// Hedef pozisyonuna do�ru ilerleme mesafesi
			float forwardDistance = Vector3.Distance(transform.position, targetPosition);

			isMoving = true; // Hareket ba�lad���nda kontrol� etkinle�tir

			// �leriye do�ru hareketi ve d�nme animasyonunu ayn� anda ba�lat
			transform.DOMove(targetPosition, 1f)
				.SetEase(Ease.Linear) // �leriye do�ru hareket animasyonu
				.OnStart(() =>
				{
					transform.DORotate(new Vector3(rotationAngleX, 0, 0), 1f, RotateMode.FastBeyond360)
						.SetEase(Ease.Linear) // D�n�� animasyonu
						.OnComplete(() =>
						{
					// �leriye do�ru hareket ve d�n�� tamamland���nda kontrol� devre d��� b�rak
					isMoving = false;
						});
				});
		}




	}

}
