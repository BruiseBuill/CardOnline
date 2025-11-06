using CardOnline.Card;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CardOnline.Character
{
	public class FightControl : MonoBehaviour
	{
		public bool isNeedResponse => isNeedAttack || isNeedDefense;
		public bool isNeedAttack;
		public bool isNeedDefense;

		public void UseCard(MagicCard magicCard)
		{

		}
	}
}