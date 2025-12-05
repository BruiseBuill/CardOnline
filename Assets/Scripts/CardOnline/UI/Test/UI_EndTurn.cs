using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BF;

namespace CardOnline.UI
{
	public class UI_EndTurn : MonoBehaviour
	{
		[SerializeField] Button button;
		[SerializeField] EventChannel ch_OnEndTurn;
		private void Start()
		{
			button.onClick.AddListener(OnClick);
		}
		void OnClick()
		{
			ch_OnEndTurn.Invoke();
		}
        private void OnDestroy()
        {
            button.onClick.RemoveListener(OnClick);
        }
    }
}