using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JellyShiftClone.Managers;

namespace JellyShiftClone.Controllers
{
    public class FinishController : MonoBehaviour
    {
		private void Start()
		{
			Debug.Log("start calisti");
		}

		private void OnTriggerEnter(Collider other)
		{
			if (other.CompareTag("Player"))
			{
				GameManager.Instance.EndGame(true);
				Debug.Log("EndGame true");
			}
		}
		
	}
}

