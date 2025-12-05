using BF;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CardOnline.UI
{
	public class UI_CloseRaycast : MonoBehaviour
	{
		[SerializeField] GenericEventChannel<bool> onCloseRaycast;
        GraphicRaycaster graphicRaycaster;

        private void Start()
        {
            graphicRaycaster = GetComponent<GraphicRaycaster>();

            onCloseRaycast.AddListener(SetRaycast);
        }
        void SetRaycast(bool isClose)
        {
            if (isClose)
            {
                graphicRaycaster.enabled = false;
            }
            else
            {
                graphicRaycaster.enabled = true;
            }
        }
    }
}