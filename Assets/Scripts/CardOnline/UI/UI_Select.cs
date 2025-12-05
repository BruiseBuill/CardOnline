using BF;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CardOnline.UI
{
	public class UI_Select : MonoBehaviour
	{
		[SerializeField] GenericEventChannel<int> ch_Accelerate;
		[SerializeField] EventChannel ch_EndAccelerate;
        [SerializeField] GameObject selectPanel;

        private void Start()
        {
            ch_Accelerate.AddListener(Accelerate);
            ch_EndAccelerate.AddListener(EndSelect);
        }
        private void OnDestroy()
        {
            ch_Accelerate.RemoveListener(Accelerate);
            ch_EndAccelerate.RemoveListener(EndSelect);
        }
        void Accelerate(int playerIndex)
        {
            if(playerIndex != 0){
                return;
            }
            selectPanel.SetActive(true);
        }
        void EndSelect() 
        {
            selectPanel.SetActive(false);
        }
    }
}