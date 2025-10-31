using BF;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CardOnline.Control
{
	public class EnemySystem : MonoBehaviour
	{
		[Header("UI")]
		[SerializeField] EventChannel ch_OnEndTurn;

        private void Start()
        {
            ch_OnEndTurn.AddListener(EndTurn);
        }
        private void OnDisable()
        {
            ch_OnEndTurn.RemoveListener(EndTurn);
        }
        public void EndTurn()
        {
            ActionSystem.Instance().Execute(new EnemyAction());
        }

    }
}