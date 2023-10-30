using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JellyShiftClone.Managers;

namespace JellyShiftClone.Controllers
{
    public class CameraController : MonoBehaviour
    {
        public Transform target;
        public Vector3 offset;
        public float lerpValue;
        private bool _isGameSuccessful = false;


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
            _isGameSuccessful = isSuccessful;
        }
        private void LateUpdate()
		{
            if (!_isGameSuccessful)
            {
                Vector3 pos = target.position + offset;
                transform.position = Vector3.Lerp(transform.position, pos, lerpValue);
            }
        }
	
    }

}
