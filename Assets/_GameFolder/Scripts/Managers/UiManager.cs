using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JellyShiftClone.Canvases;
using JellyShiftClone.Controllers;

namespace JellyShiftClone.Managers
{
    public class UiManager : MonoBehaviour
    {
        public static UiManager Instance { get; private set; }

        [SerializeField] private InputCanvas inputCanvas;
        [SerializeField] private GameCanvas gameCanvas;
        [SerializeField] private SettingsCanvas settingsCanvas;
        [SerializeField] private EndCanvas endCanvas;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        public void Initialize(InputManager inputManager, LevelManager levelManager,PlayerController playerController)
		{
            inputCanvas.Initialize(inputManager);
            gameCanvas.Initialize(levelManager,settingsCanvas,playerController);
            settingsCanvas.Initialize();
            endCanvas.Initialize();
        }
        void Start()
        {

        }

        void Update()
        {

        }
    }
}

