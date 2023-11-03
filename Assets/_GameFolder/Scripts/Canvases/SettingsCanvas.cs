using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using JellyShiftClone.Managers;

namespace JellyShiftClone.Canvases
{
    public class SettingsCanvas : MonoBehaviour
    {
        public GameObject settingPanel;
        public Button backButton;
        public Button hapticButton;
        public Button soundButton;

        public GameObject onHapticImage;
        public GameObject offHapticImage;

        public GameObject onSoundImage;
        public GameObject offSoundImage;
        public void Initialize()
		{
            backButton.onClick.AddListener(OnBackButtonClicked);
            hapticButton.onClick.AddListener(OnHapticButton);
            soundButton.onClick.AddListener(OnSoundButton);
        }

		private void Start()
		{
            UpdateHapticImages();
            UpdateSoundImages();
		}

		public void SettingPanel()
		{
            settingPanel.SetActive(true);
		}

        public void OnBackButtonClicked()
		{
            settingPanel.SetActive(false);
		}

        public void OnHapticButton()
		{
            PlayerPrefsManager.IsHapticOn = !PlayerPrefsManager.IsHapticOn;
            UpdateHapticImages();
        }


        private void OnSoundButton()
		{
            PlayerPrefsManager.IsSoundOn = !PlayerPrefsManager.IsSoundOn;
            UpdateSoundImages();
        }


        private void UpdateHapticImages()
        {    
            onHapticImage.SetActive(PlayerPrefsManager.IsHapticOn);
            offHapticImage.SetActive(!PlayerPrefsManager.IsHapticOn);
        }

        private void UpdateSoundImages()
        {
            onSoundImage.SetActive(PlayerPrefsManager.IsSoundOn);
            offSoundImage.SetActive(!PlayerPrefsManager.IsSoundOn);
        }
    }
}

