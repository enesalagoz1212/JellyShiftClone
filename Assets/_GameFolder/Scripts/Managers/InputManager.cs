using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using JellyShiftClone.Controllers;
using System;

namespace JellyShiftClone.Managers
{
	public class InputManager : MonoBehaviour
	{
		public static InputManager Instance { get; private set; }
		public bool isInputEnabled { get; private set; } = true;

		private Player _player;

		private float _firstTouchY;
		private bool _isDragging;
		private void Awake()
		{
			if (Instance != null && Instance != this)
			{
				Destroy(this);
			}
			else
			{
				Instance = this;
			}

		}

		public void Initialize(Player player)
		{
			_player = player;
			_isDragging = false;
		}
		public void OnScreenTouch(PointerEventData eventData)
		{
			Debug.Log("OnScreenTouch 1");
			if (!isInputEnabled)
			{
				return;
			}
			_firstTouchY = Input.mousePosition.y;
			_isDragging = true;

		}

		public void OnScreenDrag(PointerEventData eventData)
		{
			Debug.Log("OnScreenDrag 1");
			if (!isInputEnabled || !_isDragging)
			{
				return;
			}
			if (GameManager.Instance.GameState != GameState.Playing)
			{
				return;
			}
			float _lastTouchY = Input.mousePosition.y;
			float deltaY = _firstTouchY - _lastTouchY;

			_firstTouchY = _lastTouchY;
			_player.ChangeScale(deltaY);
		}


		public void OnScreenUp(PointerEventData eventData)
		{
			Debug.Log("OnScreenUp");
			_isDragging = false;
		}

	

		



	}
}

