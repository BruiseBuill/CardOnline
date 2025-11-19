using BF;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CardOnline.UI
{
	public class UI_Accelerate : MonoBehaviour
	{
		[SerializeField] GenericEventChannel<int> ch_Accelerate;
        [SerializeField] Button acc_Btn;

        private void Start()
        {
            acc_Btn.onClick.AddListener(() => ch_Accelerate.Invoke(0));
        }

    }
}