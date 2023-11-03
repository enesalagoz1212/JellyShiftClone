using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JellyShiftClone.Managers;
using UnityEngine.UI;
using TMPro;
using JellyShiftClone.Controllers;

namespace JellyShiftClone.Canvases
{
    public class GameCanvas : MonoBehaviour
    {
        private LevelManager _levelManager;
        private SettingsCanvas _settingsCanvas;
        private PlayerController _playerController;

        [SerializeField] private Image sliderImage;
        [SerializeField] private Image levelsImage;
        [SerializeField] private TextMeshProUGUI levelText;

        public RectTransform fullImage;
        public Button playButton;
        public Button settingsButton;
        public Button pauseButton;
        public Button resumeButton;
        public Button mainMenuButton;
        public GameObject pausedPanel;
        public float startX = -213f; 
        public float endX = 236f;

        public void Initialize(LevelManager levelManager,SettingsCanvas settingsCanvas,PlayerController playerController)
        {
            _levelManager = levelManager;
            _settingsCanvas = settingsCanvas;
            _playerController = playerController;

            settingsButton.onClick.AddListener(OnSettingsButtonClicked);
            pauseButton.onClick.AddListener(OnPausedButton);
            resumeButton.onClick.AddListener(OnResumeButton);
            mainMenuButton.onClick.AddListener(OnMainMenuButton);
        }

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
	
        private void OnGameStart()
		{
            levelsImage.gameObject.SetActive(true);
            Vector3 startPosition = fullImage.anchoredPosition;
            startPosition.x = startX;
            fullImage.anchoredPosition = startPosition;

            playButton.onClick.AddListener(OnPlayButtonClick);
            sliderImage.gameObject.SetActive(true);

            UpdateLevelText();
            settingsButton.gameObject.SetActive(true);
            pauseButton.gameObject.SetActive(false);

            Time.timeScale = 1;
        }


        private void OnGameReset()
		{
            playButton.gameObject.SetActive(true);
        }

        private void OnGameEnd(bool isSuccessful)
		{
			if (!isSuccessful)
			{
                sliderImage.gameObject.SetActive(false);
                pauseButton.gameObject.SetActive(false);
			}
		}

        private void OnPlayButtonClick()
        {
            Debug.Log("OnPlayButtonClick calisti");
            GameManager.Instance.ChangeState(GameState.Playing);
            playButton.gameObject.SetActive(false);
        }

        private void OnSettingsButtonClicked()
		{
            _settingsCanvas.SettingPanel();
		}

        private void OnPausedButton()
		{
           
            PauseGame();
        }

        private void OnResumeButton()
		{
            
            ResumeGame();
		}

        private void OnMainMenuButton()
		{
            GameManager.Instance.ResetGame();
            pausedPanel.SetActive(false);
		}

        private void PauseGame()
        {
            Time.timeScale = 0;
            pausedPanel.SetActive(true);
            pauseButton.gameObject.SetActive(false);
        }

        private void ResumeGame()
        {
            Time.timeScale = 1;
            pausedPanel.SetActive(false);
            pauseButton.gameObject.SetActive(true);
        }

        private void UpdateLevelText()
        {
            int currentLevel = PlayerPrefsManager.CurrentLevel;
            levelText.text = "LEVEL " + currentLevel.ToString();
        }

        private void Update()
        {
            if (GameManager.Instance.GameState == GameState.Playing)
            {
                float playerProgress = _levelManager.ReturnPlayerProgress();
                float newX = Mathf.Lerp(startX, endX, playerProgress);

                Vector3 newPosition = fullImage.anchoredPosition;
                newPosition.x = newX;
                fullImage.anchoredPosition = newPosition;

                settingsButton.gameObject.SetActive(false);
                pauseButton.gameObject.SetActive(true);
            }
        }
    }
}

