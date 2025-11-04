using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BF;

namespace CardOnline
{
	public class UI_StartTurn : MonoBehaviour
	{
        [SerializeField] Button button;
        [SerializeField] EventChannel ch_OnStartTurn;
        private void Start()
        {
            button.onClick.AddListener(OnClick);
        }
        void OnClick()
        {
            ch_OnStartTurn.Invoke();
        }
        private void OnDestroy()
        {
            button.onClick.RemoveListener(OnClick);
        }
    }
}