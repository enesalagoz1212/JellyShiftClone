using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JellyShiftClone.Controllers
{
    public class CameraController : MonoBehaviour
    {
        public Transform target;
        public Vector3 offset;
        public float lerpValue;

		private void LateUpdate()
		{
            Vector3 pos = target.position + offset;
            transform.position = Vector3.Lerp(transform.position, pos, lerpValue);
		}
	
    }

}
