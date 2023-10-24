using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JellyShiftClone.Canvases;

namespace JellyShiftClone.Managers
{
    public class UiManager : MonoBehaviour
    {
        public static UiManager Instance { get; private set; }

        [SerializeField] private InputCanvas inputCanvas;
        [SerializeField] private GameCanvas gameCanvas;

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
        public void Initialize(InputManager inputManager)
		{
            inputCanvas.Initialize(inputManager);
            gameCanvas.Initialize();
        }
        void Start()
        {

        }

        void Update()
        {

        }
    }
}

