using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JellyShiftClone.Managers;
using JellyShiftClone.Controllers;

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
		}

		private void OnDisable()
		{
			GameManager.OnGameStarted -= OnGameStart;

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

	}

}
