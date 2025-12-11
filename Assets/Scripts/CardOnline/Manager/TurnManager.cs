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
		[SerializeField] GenericEventChannel<object> ch_onChangeCharacter;

		void Start()
		{
			StartGame();
        }
		void StartGame()
		{
			currentCharacter = characterList[0];
			ch_onChangeCharacter.Invoke(currentCharacter);
        }
		public void CurrentTurnOver()
		{
			currentCharacter = characterList[(characterList.IndexOf(currentCharacter) + 1) % characterList.Count];
			ch_onChangeCharacter.Invoke(currentCharacter);
        }
    }
}