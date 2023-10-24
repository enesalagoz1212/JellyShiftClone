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
            
            playButton.onClick.AddListener(OnPlayButtonClick);

        }

        void Start()
        {

        }

        
        void Update()
        {

        }

        private void OnPlayButtonClick()
        {
            Debug.Log("OnPlayButtonClick calisti");
            GameManager.Instance.ChangeState(GameState.Playing);
            playButton.gameObject.SetActive(false);
        }
    }
}

