using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JellyShiftClone.Controllers
{
    public class HologramController : MonoBehaviour
    {
        private RaycastHit _raycastHit;

        private GameObject playerCopy;
        private GameObject playerShadow;

		private void Update()
		{
            if (Physics.Raycast(transform.position, transform.forward, out _raycastHit, 20.0f))
            {
                playerCopy.transform.position = _raycastHit.transform.position;
                playerCopy.transform.localScale = transform.localScale;

                playerShadow.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + (playerCopy.transform.position.z - transform.position.z) / 2);
                playerShadow.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, (playerCopy.transform.position.z - transform.position.z) * 2f);

            }
        }
	}
}

