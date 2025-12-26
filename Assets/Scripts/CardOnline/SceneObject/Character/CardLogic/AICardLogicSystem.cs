using CardOnline.Card;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CardOnline.Character
{
	public class AICardLogicSystem : CardLogicSystem
    {
		public MagicCard GetRandomHandCard()
		{
			if (handCardsList.Count == 0)
				return null;
			int index = Random.Range(0, handCardsList.Count);
			return handCardsList[index];
        }

    }
}