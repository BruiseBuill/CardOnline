using BF;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CardOnline.Character;

namespace CardOnline.Manager
{
	public class TurnManager : Single<TurnManager>
	{
		List<CharacterControl> characterList;
		CharacterControl currentCharacter;

		[Header("Out")]
		[SerializeField] GenericEventChannel<object> ch_ActionStart;
		[SerializeField] GenericEventChannel<object> ch_ActionEnd;

        void Start()
		{
			StartGame();
        }
		void StartGame()
		{
			currentCharacter = characterList[0];
			ch_ActionStart.Invoke(currentCharacter);
        }
		public void CurrentTurnOver()
		{
            ch_ActionEnd.Invoke(currentCharacter);
        }
		public void StartNextTurn()
		{
            currentCharacter = characterList[(characterList.IndexOf(currentCharacter) + 1) % characterList.Count];
			ch_ActionStart.Invoke(currentCharacter);
        }
    }
}