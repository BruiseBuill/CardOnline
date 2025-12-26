using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BF;
using System;
using CardOnline.Bullet;

namespace CardOnline.Character
{
	public enum CharacterIndex
	{
		Player, AI, AI_2, AI_3,AI_4, Player_2, Player_3, Player_4
    }
	public class CharacterData : BaseShareData
	{
		[Header("Component")]
		public CharacterControl characterControl;
		public CharacterInput characterInput;
		public CardLogicSystem characterCardController;
		public CardAllignment cardAllignment;
		public CoolDownAlligement coolDownAlligement;

		[Header("Data")]
		public int characterIndex;
		public bool isPlayer;

		[Header("In_Event")]
        [SerializeField] protected GenericEventChannel<object> ch_ActionStart;
        [SerializeField] protected GenericEventChannel<object> ch_ActionEnd;

		//Inner Event
		public Action<BaseBullet> onBeHit = delegate { };
	}
}