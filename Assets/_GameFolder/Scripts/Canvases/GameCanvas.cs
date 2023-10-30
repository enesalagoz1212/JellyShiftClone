using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JellyShiftClone.Managers;
using UnityEngine.UI;

namespace JellyShiftClone.Canvases
{
    public class GameCanvas : MonoBehaviour
    {
        public Button playButton;

        public void Initialize()
        {
            
           

        }

		private void OnEnable()
		{
            GameManager.OnGameStarted += OnGameStart;
            GameManager.OnGameReset += OnGameReset;
		}

		private void OnDisable()
		{
            GameManager.OnGameStarted -= OnGameStart;
            GameManager.OnGameReset -= OnGameReset;
			
		}
	
        private void OnGameStart()
		{
            playButton.onClick.AddListener(OnPlayButtonClick);
        }


        private void OnGameReset()
		{
            playButton.gameObject.SetActive(true);
        }

        private void OnPlayButtonClick()
        {
            Debug.Log("OnPlayButtonClick calisti");
            GameManager.Instance.ChangeState(GameState.Playing);
            playButton.gameObject.SetActive(false);
        }
    }
}

