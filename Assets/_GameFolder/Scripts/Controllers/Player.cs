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
		private Rigidbody _rigitbody;
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
			_rigitbody = GetComponent<Rigidbody>();
		}

		private void OnGameStart()
		{
			_initialScale = transform.localScale;
		}

		public void ChangeScale()
		{
			if (InputManager.Instance.isInputEnabled)
			{
				Vector3 mousePos = Input.mousePosition;
				mousePos.z = 10;

				Vector3 moveVec = _camera.ScreenToWorldPoint(mousePos);
				float x = transform.localScale.x;
				moveVec.z = transform.localScale.z;
				moveVec.y *= 2f;
				moveVec.y = Mathf.Clamp(moveVec.y, minY, maxY);
				x = 10 / moveVec.y;

				moveVec.x = Mathf.Clamp(x, minX, maxX);

				transform.localScale = Vector3.Lerp(transform.localScale, moveVec, lerpValue);
			}

		}
	}

}
