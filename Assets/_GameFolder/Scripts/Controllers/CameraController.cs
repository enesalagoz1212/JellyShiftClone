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
        private bool _isFollowingPlayer = true;



        private void OnEnable()
        {
            GameManager.OnGameStarted += OnGameStart;
            GameManager.OnGameEnd += OnGameEnd;
        }
        private void OnDisable()
        {
            GameManager.OnGameStarted -= OnGameStart;
            GameManager.OnGameEnd -= OnGameEnd;
        }

        private void OnGameStart()
		{
            _isFollowingPlayer = true;
		}

        private void OnGameEnd(bool isSuccessful)
        {
			if (!isSuccessful)
			{
                _isFollowingPlayer = false;
			}
            
        }
        private void LateUpdate()
		{
            if (_isFollowingPlayer)
            {
                Vector3 pos = target.position + offset;
                transform.position = Vector3.Lerp(transform.position, pos, lerpValue);
            }
        }
	
    }

}
