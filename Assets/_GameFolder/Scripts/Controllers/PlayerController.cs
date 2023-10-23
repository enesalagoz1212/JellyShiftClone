using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JellyShiftClone.Managers;

namespace JellyShiftClone.Controllers
{
	public class PlayerController : MonoBehaviour
	{
		public float forwardSpeed;
		private bool _canMove = true;

		public void Initialize()
		{

		}
		void Start()
		{

		}


		void Update()
		{
			switch (GameManager.Instance.GameState)
			{
				case GameState.Start:
					_canMove = true;
					break;
				case GameState.Playing:
					break;
				case GameState.End:
					_canMove = false;
					break;
				case GameState.Reset:
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

