using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JellyShiftClone.Managers;
using UnityEngine.UI;
using TMPro;

namespace JellyShiftClone.Canvases
{
    public class EndCanvas : MonoBehaviour
    {

		public GameObject endPanel;
		public Button nextButton;

		public void Initialize()
		{

		}

		private void OnEnable()
		{
			GameManager.OnGameEnd += OnGameEnd;
		}

		private void OnDisable()
		{
			GameManager.OnGameEnd -= OnGameEnd;			
		}


		private void OnGameEnd(bool isSuccessful)
		{
			Debug.Log("111");
			if (isSuccessful)
			{
				Debug.Log("222");
				endPanel.SetActive(true);
				nextButton.onClick.AddListener(NextButtonClick);
			}
		}

		private void NextButtonClick()
		{
			endPanel.SetActive(false);
			GameManager.Instance.ResetGame();
		}

		
	}

}
