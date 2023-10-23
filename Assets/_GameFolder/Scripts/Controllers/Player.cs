using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JellyShiftClone.Managers;

namespace JellyShiftClone.Controllers
{
    public class Player : MonoBehaviour
    {
        private Vector3 _initialScale;

		private void OnEnable()
		{
			GameManager.OnGameStarted += OnGameStart;
		}

		private void OnDisable()
		{
			GameManager.OnGameStarted -= OnGameStart;
			
		}

		private void OnGameStart()
		{
			_initialScale = transform.localScale;
		}

		public void ChangeScale(bool isUp)
		{
			if (isUp)
			{
				transform.localScale = new Vector3(_initialScale.x * 0.8f, _initialScale.y * 1.2f, _initialScale.z);
			}
			else
			{
				transform.localScale = new Vector3(_initialScale.x * 1.2f, _initialScale.y * 0.8f, _initialScale.z);
			}
		}
    }

}
